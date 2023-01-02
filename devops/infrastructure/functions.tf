# Create a app plan that the functions app can run on
resource "azurerm_service_plan" "ap-functions-mikkelhm-f1" {
  name                = "asp-func-${var.main_identifier}"
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  location            = azurerm_resource_group.rg-mikkelhm-f1.location
  os_type             = "Linux"
  sku_name            = "Y1"
}

## SYNC FUNCTIONS APP
# Create a Storage account for the functions app to tuse
resource "azurerm_storage_account" "sa-functions-sync-mikkelhm-f1" {
  name                     = "safuncsyncmikkelhmf1"
  resource_group_name      = azurerm_resource_group.rg-mikkelhm-f1.name
  location                 = azurerm_resource_group.rg-mikkelhm-f1.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

# Create the functions app
resource "azurerm_linux_function_app" "fa-functions-sync-mikkelhm-f1" {
  name                = "azfunc-${var.main_identifier}-sync"
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  location            = azurerm_resource_group.rg-mikkelhm-f1.location

  storage_account_name       = azurerm_storage_account.sa-functions-sync-mikkelhm-f1.name
  storage_account_access_key = azurerm_storage_account.sa-functions-sync-mikkelhm-f1.primary_access_key
  service_plan_id            = azurerm_service_plan.ap-functions-mikkelhm-f1.id

  site_config {
    application_insights_key = azurerm_application_insights.ai-mikkelhm-f1.instrumentation_key
  }

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet"
    "AZURE_CLIENT_ID"          = azuread_service_principal.sp.application_id
    "AZURE_CLIENT_SECRET"      = azuread_service_principal_password.sp_pass.value
    "AZURE_TENANT_ID"          = var.azure_tenant_id
    "AZURE_SUBSCRIPTION_ID"    = var.azure_subscription_id
  }
}

# Store a Static WebApp deployment token
resource "github_actions_secret" "functions_sync_app_resourcegroup" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_FUNCTIONS_SYNC_RESOURCE_GROUP_NAME"
  plaintext_value = azurerm_linux_function_app.fa-functions-sync-mikkelhm-f1.resource_group_name
}

# Store a Static WebApp deployment token
resource "github_actions_secret" "function_sync_app_name" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_FUNCTIONS_SYNC_APP_NAME"
  plaintext_value = azurerm_linux_function_app.fa-functions-sync-mikkelhm-f1.name
}


## API FUNCTIONS APP
# Create a Storage account for the functions app to tuse
resource "azurerm_storage_account" "sa-functions-api-mikkelhm-f1" {
  name                     = "safuncapimikkelhmf1"
  resource_group_name      = azurerm_resource_group.rg-mikkelhm-f1.name
  location                 = azurerm_resource_group.rg-mikkelhm-f1.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

# Create the functions app
resource "azurerm_linux_function_app" "fa-functions-api-mikkelhm-f1" {
  name                = "azfunc-${var.main_identifier}-api"
  resource_group_name = azurerm_resource_group.rg-mikkelhm-f1.name
  location            = azurerm_resource_group.rg-mikkelhm-f1.location

  storage_account_name       = azurerm_storage_account.sa-functions-api-mikkelhm-f1.name
  storage_account_access_key = azurerm_storage_account.sa-functions-api-mikkelhm-f1.primary_access_key
  service_plan_id            = azurerm_service_plan.ap-functions-mikkelhm-f1.id

  site_config {
    application_insights_key = azurerm_application_insights.ai-mikkelhm-f1.instrumentation_key
  }

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet"
    "AZURE_CLIENT_ID"          = azuread_service_principal.sp.application_id
    "AZURE_CLIENT_SECRET"      = azuread_service_principal_password.sp_pass.value
    "AZURE_TENANT_ID"          = var.azure_tenant_id
    "AZURE_SUBSCRIPTION_ID"    = var.azure_subscription_id
  }
}

# Store a Static WebApp deployment token
resource "github_actions_secret" "functions_api_app_resourcegroup" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_FUNCTIONS_API_RESOURCE_GROUP_NAME"
  plaintext_value = azurerm_linux_function_app.fa-functions-api-mikkelhm-f1.resource_group_name
}

# Store a Static WebApp deployment token
resource "github_actions_secret" "function_api_app_name" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_FUNCTIONS_API_APP_NAME"
  plaintext_value = azurerm_linux_function_app.fa-functions-api-mikkelhm-f1.name
}
