#!/bin/bash
SApassword=$1
dacpath=$2
sqlpath=$3
projpath=$4

set -e

echo "🚀 Setting up development environment..."

# Run initial DB setup
bash /workspaces/.devcontainer/advanced-microservices/microservices/mssql/SQLReadiness.sh $SApassword $dacpath $sqlpath

# Install .NET workload
sudo dotnet workload update

# Restore .NET project dependencies
cd $projpath && dotnet restore

# List installed .NET tools && certificates
dotnet tool list --global
ls -l /home/vscode/.dotnet/corefx/cryptography/x509stores

echo "🎉 Development container setup complete!"
