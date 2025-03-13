#!/bin/bash

# Setup .NET dev certificates
bash .devcontainer/scripts/setup-dotnet-dev-cert.sh

# Instal Aspire Project Templates
dotnet new install Aspire.ProjectTemplates::9.1.0 --force