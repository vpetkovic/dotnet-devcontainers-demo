FROM mcr.microsoft.com/devcontainers/dotnet:8.0-bookworm

ARG USERNAME=vscode

# Change ownership of the /home/vscode directory to the vscode user (to avoid permission errors)
RUN chown -R $USERNAME:$USERNAME /home/$USERNAME \
    && apt-get update \
    # Ensure all files are converted to LF to avoid issues with CRLF by using dos2unix
    && apt-get install -y dos2unix

# Install SQL Tools: SQLPackage and sqlcmd
COPY microservices/mssql/installSQLtools.sh installSQLtools.sh
RUN bash installSQLtools.sh \
    && rm ./installSQLtools.sh
    
# Install required .NET tools globally
RUN su vscode -c "/usr/share/dotnet/dotnet tool install --ignore-failed-sources --add-source https://api.nuget.org/v3/index.json --global dotnet-ef"
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

# Cleanup
USER root
RUN apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

USER $USERNAME