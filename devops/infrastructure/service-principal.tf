resource "azuread_application" "ad_app" {
  display_name = "sp-mikkelhm-f1"
  web {
    homepage_url  = "http://f1.madsn.dk"
    redirect_uris = ["http://f1.madsn.dk/mikkelhm-f1"]
    implicit_grant {
      access_token_issuance_enabled = true
      id_token_issuance_enabled     = true
    }
  }
}

resource "azuread_service_principal" "sp" {
  application_id               = azuread_application.ad_app.application_id
  app_role_assignment_required = false
}

resource "time_static" "now" {
}

# 8760 hours in a year, expires in 5 years
locals {
  expiration_date = timeadd(time_static.now.rfc3339, "${tostring(8760 * 5)}h")
}

resource "azuread_service_principal_password" "sp_pass" {
  service_principal_id = azuread_service_principal.sp.id
  end_date             = local.expiration_date
}

resource "azurerm_key_vault_secret" "sp_client_secret" {
  key_vault_id = azurerm_key_vault.kv.id

  name            = "RuntimeServicePrincipalClientSecret"
  value           = azuread_service_principal_password.sp_pass.value
  expiration_date = local.expiration_date
}

locals {
  role_assignments = {
    "kv_access" = {
      scope                = azurerm_key_vault.kv.id
      role_definition_name = "Key Vault Secrets User"
    },
    "kv_certificate_access" = {
      scope                = azurerm_key_vault.kv.id
      role_definition_name = "Key Vault Certificates Officer"
    },
    "appcfg_access" = {
      scope                = azurerm_app_configuration.appcfg.id
      role_definition_name = "App Configuration Data Reader"
    },
  }
}

resource "azurerm_role_assignment" "role_assignments" {
  for_each             = local.role_assignments
  scope                = each.value.scope
  role_definition_name = each.value.role_definition_name
  principal_id         = azuread_service_principal.sp.object_id
}

resource "github_actions_secret" "runtime_sp_app_id" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_SP_RUNTIME_CLIENT_ID"
  plaintext_value = azuread_service_principal.sp.application_id
}

resource "github_actions_secret" "runtime_sp_secret" {
  repository      = "mikkelhm-f1"
  secret_name     = "AZURE_SP_RUNTIME_CLIENT_SECRET"
  plaintext_value = azuread_service_principal_password.sp_pass.value
}