resource "azurerm_cosmosdb_account" "cosmosdb-mikkelhm-f1" {
  name                      = "cosdb-${var.main_identifier}"
  resource_group_name       = azurerm_resource_group.rg-mikkelhm-f1.name
  location                  = azurerm_resource_group.rg-mikkelhm-f1.location
  offer_type                = "Standard"
  kind                      = "GlobalDocumentDB"
  enable_automatic_failover = false
  enable_free_tier          = true

  consistency_policy {
    consistency_level       = "Session"
    max_interval_in_seconds = 5
    max_staleness_prefix    = 100
  }

  geo_location {
    location          = azurerm_resource_group.rg-mikkelhm-f1.location
    failover_priority = 0
  }
}

resource "azurerm_key_vault_secret" "kv-cosmosdb-primary-key" {
  key_vault_id = azurerm_key_vault.kv.id

  name  = "CosmosDbPrimaryKey"
  value = azurerm_cosmosdb_account.cosmosdb-mikkelhm-f1.primary_key
}

resource "azurerm_app_configuration_key" "appcfg-cosmosdb-primary-key" {
  configuration_store_id = azurerm_app_configuration.appcfg.id
  key                    = "CosmosDbPrimaryKey"
  type                   = "vault"
  vault_key_reference    = azurerm_key_vault_secret.kv-cosmosdb-primary-key.id
}

resource "azurerm_app_configuration_key" "appcfg-cosmosdb-endpoint" {
  configuration_store_id = azurerm_app_configuration.appcfg.id
  key                    = "CosmosDbEndpoint"
  value                  = azurerm_cosmosdb_account.cosmosdb-mikkelhm-f1.endpoint
}