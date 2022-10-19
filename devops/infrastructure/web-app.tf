# Create the Static Webapp
resource "azurerm_static_site" "ss-mikkelhm-f1" {
  name                = "ss-${var.main_identifier}"
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  location            = "West Europe"
}

# Store a Static WebApp deployment token
resource "github_actions_secret" "deployment_token" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_STATIC_WEB_APPS_API_TOKEN"
  plaintext_value = azurerm_static_site.ss-mikkelhm-f1.api_key
}

# Create a custom domain in the Static Webapp
resource "azurerm_static_site_custom_domain" "cd-f1-madsn-dk" {
  static_site_id  = azurerm_static_site.ss-mikkelhm-f1.id
  domain_name     = "f1.madsn.dk"
  validation_type = "dns-txt-token"
}

# Create a TXT record to verify the custom domain in Cloudflare
resource "cloudflare_record" "cf-txt-f1-mikkelhm-f1" {
  zone_id = var.cloudflare_zone_id
  name    = "f1.madsn.dk"
  value   = azurerm_static_site_custom_domain.cd-f1-madsn-dk.validation_token
  type    = "TXT"
  ttl     = 1
}

# Create the CNAME record for the domain that will point to the webapp default hostname
resource "cloudflare_record" "cf-cname-f1-mikkelhm-f1" {
  zone_id = var.cloudflare_zone_id
  name    = "f1"
  value   = azurerm_static_site.ss-mikkelhm-f1.default_host_name
  type    = "CNAME"
  ttl     = 1
}