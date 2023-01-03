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

resource "azurerm_key_vault_certificate" "apim_cname_certificate" {
  name         = "api-f1-certificate"
  key_vault_id = azurerm_key_vault.kv.id

  certificate_policy {
    issuer_parameters {
      name = "Self"
    }

    key_properties {
      exportable = true
      key_size   = 2048
      key_type   = "RSA"
      reuse_key  = true
    }

    lifetime_action {
      action {
        action_type = "AutoRenew"
      }

      trigger {
        days_before_expiry = 30
      }
    }

    secret_properties {
      content_type = "application/x-pkcs12"
    }

    x509_certificate_properties {
      key_usage = [
        "cRLSign",
        "dataEncipherment",
        "digitalSignature",
        "keyAgreement",
        "keyCertSign",
        "keyEncipherment",
      ]

      subject            = "CN=api-f1.madsn.dk"
      validity_in_months = 12

      subject_alternative_names {
        dns_names = [
          "api-f1.madsn.dk",
        ]
      }
    }
  }
}

resource "azurerm_api_management_custom_domain" "apim_internal" {
  api_management_id = azurerm_api_management.apim.id
  gateway {
    host_name    = "api-f1.madsn.dk"
    key_vault_id = azurerm_key_vault_certificate.apim_cname_certificate.secret_id
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
