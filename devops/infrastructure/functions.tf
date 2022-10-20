# Create a Storage account for the functions app to tuse
resource "azurerm_storage_account" "sa-functions-mikkelhm-f1" {
  name                     = "safuncmikkelhmf1"
  resource_group_name      = azurerm_resource_group.rg-mikkelhm-f1.name
  location                 = azurerm_resource_group.rg-mikkelhm-f1.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

# Create a app plan that the functions app can run on
resource "azurerm_service_plan" "ap-functions-mikkelhm-f1" {
  name                = "asp-func-${var.main_identifier}"
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  location            = azurerm_resource_group.rg-mikkelhm-f1.location
  os_type             = "Linux"
  sku_name            = "Y1"
}

# Create the functions app
resource "azurerm_linux_function_app" "fa-functions-mikkelhm-f1" {
  name                = "azfunc-${var.main_identifier}"
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  location            = azurerm_resource_group.rg-mikkelhm-f1.location

  storage_account_name       = azurerm_storage_account.sa-functions-mikkelhm-f1.name
  storage_account_access_key = azurerm_storage_account.sa-functions-mikkelhm-f1.primary_access_key
  service_plan_id            = azurerm_service_plan.ap-functions-mikkelhm-f1.id

  site_config {
    application_insights_key = azurerm_application_insights.ai-mikkelhm-f1.instrumentation_key
  }

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet"
  }
}

# Store a Static WebApp deployment token
resource "github_actions_secret" "functions_app_resourcegroup" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_FUNCTIONS_RESOURCE_GROUP_NAME"
  plaintext_value = azurerm_linux_function_app.fa-functions-mikkelhm-f1.resource_group_name
}

# Store a Static WebApp deployment token
resource "github_actions_secret" "function_app_name" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_FUNCTIONS_APP_NAME"
  plaintext_value = azurerm_linux_function_app.fa-functions-mikkelhm-f1.name
}