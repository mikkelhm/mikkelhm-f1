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
        <cross-domain>
          <cross-domain-policy>
            <allow-http-request-headers-from domain='*' headers='*' />
          </cross-domain-policy>
        </cross-domain>
      </inbound>
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
