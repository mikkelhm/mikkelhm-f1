terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.20.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "= 2.29.0"
    }
    cloudflare = {
      source  = "cloudflare/cloudflare"
      version = "~> 3.0"
    }
    github = {
      source  = "integrations/github"
      version = "~> 4.0"
    }
    pkcs12 = {
      source  = "chilicat/pkcs12"
      version = "0.0.7"
    }
  }
  backend "azurerm" {}

  required_version = ">= 1.1.0"
}

provider "azurerm" {
  tenant_id       = var.azure_tenant_id
  subscription_id = var.azure_subscription_id
  client_id       = var.azure_client_id
  client_secret   = var.azure_client_secret
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

provider "pkcs12" {
}