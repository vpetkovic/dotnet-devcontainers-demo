FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

# Install SQL Tools: SQLPackage and sqlcmd
COPY microservices/mssql/installSQLtools.sh installSQLtools.sh
RUN bash ./installSQLtools.sh \
     && apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

# Install EF Core CLI globally
RUN su vscode -c "/usr/share/dotnet/dotnet tool install --ignore-failed-sources --add-source https://api.nuget.org/v3/index.json --global dotnet-ef"
# Install dotnet user-secrets globally
RUN su vscode -c "/usr/share/dotnet/dotnet tool install --ignore-failed-sources --add-source https://api.nuget.org/v3/index.json --global dotnet-user-secrets"
RUN su vscode -c "/usr/share/dotnet/dotnet tool install --ignore-failed-sources --add-source https://api.nuget.org/v3/index.json --global nbgv"
# Ensure PATH includes the global tools directory
ENV PATH="$PATH:/root/.dotnet/tools"
ENV DOTNET_NO_PACKAGE_FALLBACK=true

# [Optional] Uncomment this section to install additional OS packages.
# RUN apt-get update && export DEBIAN_FRONTEND=noninteractive \
#     && apt-get -y install --no-install-recommends <your-package-list-here>

# [Optional] - Node.js - Uncomment this line to install global node packages.
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && nvm install --lts && nvm use --lts && npm install -g typescript" 2>&1
# RUN su vscode -c "source /usr/local/share/nvm/nvm.sh && npm install -g <your-package-here>" 2>&1
