# Make sure the workspace is writable
# chmod -R 777 ./workspaces

# Setting up the .NET Core CLI tools
#dotnet workload update


# Setting up certificates
mkdir -p /https
chmod 700 /https
dotnet dev-certs https -ep /https/aspnetapp.pfx -p password
dotnet dev-certs https --trust