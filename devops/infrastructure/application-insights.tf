# Application Insights
resource "azurerm_log_analytics_workspace" "law-mikkelhm-f1" {
  name                = "law-${var.main_identifier}"
  location            = azurerm_resource_group.rg-mikkelhm-f1.location
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}

resource "azurerm_application_insights" "ai-mikkelhm-f1" {
  name                = "ai-${var.main_identifier}"
  location            = azurerm_resource_group.rg-mikkelhm-f1.location
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  workspace_id        = azurerm_log_analytics_workspace.law-mikkelhm-f1.id
  application_type    = "web"
}

# 8760 hours in a year, expires in 5 years
locals {
  expiration_date = timeadd(time_static.now.rfc3339, "${tostring(8760 * 5)}h")
}

resource "azurerm_key_vault_secret" "kv-ai-instrumentation-key" {
  key_vault_id = azurerm_key_vault.kv.id

  name            = "ApplicationInsightsInstrumentationKey"
  value           = azurerm_application_insights.ai-mikkelhm-f1.instrumentation_key
  expiration_date = local.expiration_date
}

resource "azurerm_app_configuration_key" "appcfg-ai-instrumentation-key" {
  configuration_store_id = azurerm_app_configuration.appcfg.id
  key                    = "ApplicationInsightsInstrumentationKey"
  type                   = "vault"
  vault_key_reference    = azurerm_key_vault_secret.kv-ai-instrumentation-key.id
}
