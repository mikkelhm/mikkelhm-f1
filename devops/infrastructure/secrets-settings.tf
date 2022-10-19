# KeyVault and AppConfiguration

resource "azurerm_app_configuration" "appcfg" {
  name                = "appcfg-${var.main_identifier}"
  resource_group_name = var.main_resourcegroup
  location            = var.default_location
  sku                 = "free"
  identity {
    type = "SystemAssigned"
  }
}

// Keyvault resource for services within the bounded context
// NOTE - Where do we populate actual values from?
resource "azurerm_key_vault" "kv" {
  name                = "kv-${var.main_identifier}" // max 24 alphanumeric chars & dashes
  location            = var.default_location
  resource_group_name = var.main_resourcegroup
  tenant_id           = var.azure_tenant_id

  enable_rbac_authorization  = true
  soft_delete_retention_days = 7
  purge_protection_enabled   = false
  sku_name                   = "standard"
}