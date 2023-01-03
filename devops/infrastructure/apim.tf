locals {
  organization       = "EllAsta"
  one_year_in_hours  = 8760
  one_month_in_hours = 720
  key_size           = 4096
  key_type           = "RSA"
}


resource "azurerm_api_management" "apim" {
  location            = azurerm_resource_group.rg-mikkelhm-f1.location
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name

  name            = "apim-mikkelhm-f1"
  publisher_name  = "EllAsta"
  publisher_email = "mikkel@h-madsen.dk"
  sku_name        = "Consumption_0"

  identity {
    type = "SystemAssigned"
  }
}

resource "cloudflare_record" "apim_cname_record" {
  zone_id = var.cloudflare_zone_id
  name    = "api-f1"
  type    = "CNAME"
  value   = trimprefix(azurerm_api_management.apim.gateway_url, "https://")
  proxied = true
  ttl     = 1 # Cloudflare will terminate TLS
}


# api-internal.cloudflare-zone-name certificate
resource "tls_private_key" "rsa_4096_key" {
  algorithm = local.key_type
  rsa_bits  = local.key_size
}

# CA certificate
resource "tls_self_signed_cert" "apim_hostname_ca" {
  private_key_pem = tls_private_key.rsa_4096_key.private_key_pem

  subject {
    common_name  = cloudflare_record.apim_cname_record.hostname
    organization = local.organization
  }

  validity_period_hours = local.one_year_in_hours
  early_renewal_hours   = local.one_month_in_hours
  is_ca_certificate     = true

  allowed_uses = [
    "key_encipherment",
    "digital_signature",
    "server_auth"
  ]
}

// Certificate conversion
resource "random_password" "pfx_password" {
  length  = 16
  special = false
}
resource "pkcs12_from_pem" "ca_pkcs12" {
  password        = random_password.pfx_password.result
  cert_pem        = tls_self_signed_cert.apim_hostname_ca.cert_pem
  private_key_pem = tls_private_key.rsa_4096_key.private_key_pem
}

resource "azurerm_api_management_custom_domain" "apim_internal" {
  api_management_id = azurerm_api_management.apim.id

  # Bugs
  ## default_ssl_binding is set to true by default even though the documentation says otherwise and trying to updating trough Terraform does not work...
  ## ... due to the issue, we need to set it to true else the CI/CD pipeline will take a ~ 6 min build time hit per stamp
  proxy {
    host_name            = cloudflare_record.apim_cname_record.hostname
    certificate          = pkcs12_from_pem.ca_pkcs12.result
    certificate_password = random_password.pfx_password.result
    default_ssl_binding  = true
  }
}

resource "azurerm_api_management_api" "f1_api" {
  name                = "f1"
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  api_management_name = azurerm_api_management.apim.name
  revision            = "1"
  display_name        = "F1 API"
  path                = "f1"
  protocols           = ["https"]
  service_url         = "https://${azurerm_linux_function_app.fa-functions-api-mikkelhm-f1.default_hostname}/api/"

  import {
    content_format = "swagger-link-json"
    content_value  = "https://${azurerm_linux_function_app.fa-functions-api-mikkelhm-f1.default_hostname}/api/swagger.json"
  }
}

resource "azurerm_api_management_api_policy" "f1_api_policy" {
  api_name            = azurerm_api_management_api.f1_api.name
  api_management_name = azurerm_api_management.apim.name
  resource_group_name = azurerm_api_management.apim.resource_group_name
  xml_content         = <<XML
<policies>
    <inbound>
        <base />
        <cors allow-credentials="false">
            <allowed-origins>
                <origin>*</origin>
            </allowed-origins>
            <allowed-methods>
                <method>GET</method>
                <method>POST</method>
            </allowed-methods>
        </cors>
    </inbound>
    <backend>
        <base />
    </backend>
    <outbound>
        <base />
    </outbound>
    <on-error>
        <base />
    </on-error>
</policies>
  XML
}

resource "azurerm_api_management_subscription" "f1_api_subscription" {
  api_management_name = azurerm_api_management.apim.name
  resource_group_name = azurerm_api_management.apim.resource_group_name
  display_name        = "f1_api_subscription"
  api_id              = azurerm_api_management_api.f1_api.id
  state               = "active"
}
