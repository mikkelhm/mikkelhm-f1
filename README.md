# mikkelhm-f1

Using the API at https://ergast.com to get data about formula 1. 

We will try to sync the data up into our own store, so we dont have to hammer their APIs all the time

Goal:

Show the current season standings and race results

# Infrastructure
Setup via Terraform

Backend
`terraform init --backend-config=backend.tfvars`

Plan
`terraform plan -var-file="variables.tfvars"`
