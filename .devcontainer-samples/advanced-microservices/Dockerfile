FROM mcr.microsoft.com/devcontainers/dotnet:8.0-bookworm

ARG USERNAME=vscode
ARG INSTALL_SQL_TOOLS=true
ARG INSTALL_DOTNET_TOOLS=true
ARG INSTALL_NODE=false

# Change ownership of the /home/vscode directory to the vscode user (to avoid permission errors)
RUN chown -R $USERNAME:$USERNAME /home/$USERNAME \
    && apt-get update \
    # Ensure all files are converted to LF to avoid issues with CRLF by using dos2unix
    && apt-get install -y dos2unix

# Install SQL Tools: SQLPackage and sqlcmd
RUN if [ "$INSTALL_SQL_TOOLS" = "true" ]; then \
    echo "🚀 Installing mssql-tools"; \
    curl -sSL https://packages.microsoft.com/keys/microsoft.asc | (OUT=$(apt-key add - 2>&1) || echo $OUT); \
    DISTRO=$(lsb_release -is | tr '[:upper:]' '[:lower:]'); \
    CODENAME=$(lsb_release -cs); \
    echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-${DISTRO}-${CODENAME}-prod ${CODENAME} main" > /etc/apt/sources.list.d/microsoft.list; \
    apt-get update; \
    ACCEPT_EULA=Y apt-get -y install unixodbc-dev msodbcsql17 libunwind8 mssql-tools; \
    echo "Installing sqlpackage"; \
    curl -sSL -o sqlpackage.zip "https://aka.ms/sqlpackage-linux"; \
    mkdir /opt/sqlpackage; \
    unzip sqlpackage.zip -d /opt/sqlpackage; \
    rm sqlpackage.zip; \
    chmod a+x /opt/sqlpackage/sqlpackage; \
  fi
    
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
RUN if [ "$INSTALL_NODE" = "true" ]; then \
    curl -sL https://deb.nodesource.com/setup_18.x | bash -; \
    apt-get install -y nodejs; \
    npm install -g typescript eslint eslint-config-prettier eslint-plugin-prettier prettier eslint-plugin-import eslint-plugin-jsx-a11y eslint-plugin-react eslint-plugin-react-hooks eslint-plugin-node eslint-plugin-promise; \
  fi

# Cleanup
USER root
RUN apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

USER $USERNAME