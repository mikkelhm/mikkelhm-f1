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