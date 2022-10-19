## Azure
variable "azure_subscription_id" {
  description = "Azure Subscription Id"
}

variable "azure_tenant_id" {
  description = "Azure Tenant Id"
}

variable "azure_client_id" {
  description = "Client Id for preforming the Terraform against Azure"
}

variable "azure_client_secret" {
  description = "Client secret to go with the Client Id for preforming the Terraform against Azure"
}

## Cloudflare
variable "cloudflare_zone_id" {
  description = "The zone Id defined in Cloudflare"
}

variable "cloudflare_api_token" {
  description = "Cloudflare api token to access the zone"
}

variable "github_token" {
  description = "Token that can edit in github"
}

variable "main_identifier" {
  description = "Main identifier, for easy access - mikkelhm-f1"
}

variable "main_resourcegroup" {
  description = "The main resource group"
}

variable "default_location" {
  description = "the default location"
}