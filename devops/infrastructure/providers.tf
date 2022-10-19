terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.2.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "= 2.15.0"
    }
    cloudflare = {
      source  = "cloudflare/cloudflare"
      version = "~> 3.0"
    }
    github = {
      source  = "integrations/github"
      version = "~> 4.0"
    }
  }
  backend "azurerm" {}

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  tenant_id       = var.azure_tenant_id
  subscription_id = var.azure_subscription_id
  client_id       = var.azure_client_id
  client_secret   = var.
  features {}
}

provider "azuread" {
  tenant_id     = var.azure_tenant_id
  client_id     = var.azure_client_id
  client_secret = var.azure_client_secret
}

provider "cloudflare" {
  api_token = var.cloudflare_api_token
}

provider "github" {
  token = var.github_token
}