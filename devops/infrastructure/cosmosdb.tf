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

# Store Cosmosdb connection details
resource "github_actions_secret" "cosmosdb_connectionstring" {
  repository      = "mikkelhm-f1"
  secret_name     = "COSMOSDB_PRIMARY_MASTER_KEY"
  plaintext_value = azurerm_cosmosdb_account.cosmosdb-mikkelhm-f1.primary_key
}

# Store Cosmosdb connection details
resource "github_actions_secret" "cosmosdb_endpoint" {
  repository      = "mikkelhm-f1"
  secret_name     = "COSMOSDB_ENDPOINT"
  plaintext_value = azurerm_cosmosdb_account.cosmosdb-mikkelhm-f1.endpoint
}