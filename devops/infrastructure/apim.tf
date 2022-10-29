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
