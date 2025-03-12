# Make sure the workspace is writable
# chmod -R 777 ./workspaces

# Setting up the .NET Core CLI tools
sudo dotnet workload update

# List installed .NET tools && certificates
dotnet tool list --global
ls -l /home/vscode/.dotnet/corefx/cryptography/x509stores