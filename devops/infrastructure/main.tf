# Create a Resource Group
resource "azurerm_resource_group" "rg-mikkelhm-f1" {
  name     = var.main_resourcegroup
  location = var.default_location
}

# TODO: Create Service Principal that can modify resources in this resource group
# TODO: Store its output as json in a GITHUB secret like: 
# {
#   "clientId": "<GUID>",
#   "clientSecret": "<STRING>",
#   "subscriptionId": "<GUID>",
#   "tenantId": "<GUID>",
#   "resourceManagerEndpointUrl": "https://management.azure.com/"
# }
# TODO: use that secret when deploying (in deploy.yml)

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