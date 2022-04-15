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