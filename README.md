Testing remote development with dev containers

## Introduction
This repository is a template for demonstrating the use of development containers for .NET projects. Development containers provide a consistent and isolated development environment, making it easier to develop, build, and test applications. You can use this template as a starting point for your own .NET projects.

## Repository Structure

- `.devcontainer/`: Directory containing the configuration for the development container.
  - `devcontainer.json`: Configuration file for setting up the development container.
  - `Dockerfile`: Dockerfile used to create the development container.
  - `docker-compose.yml`: Docker Compose file for orchestrating multiple containers.
- `src/`: Directory containing the source code of the .NET project.

### .devcontainer Folder

The `.devcontainer` folder is highly flexible and can be adjusted to suit various development needs:

- **Multiple Microservices**: You can include additional microservices within the Docker Compose setup by modifying the `docker-compose.yml` file. This allows for seamless integration and interaction between different services.
- **Customizing Base Image**: The `Dockerfile` can be customized to change the base image or to use a prebuilt image for the application container. This provides flexibility in choosing the development environment.
- **Post-Create Commands**: The `devcontainer.json` file supports post-create commands that can be executed after the container is created. This is useful for setting up the environment, installing dependencies, or running initial setup scripts.
- **Installed Features and Extensions**: The development container installs several useful features and extensions for .NET development, including:
  - .NET SDK
  - Node.js (for frontend development)
  - Docker CLI
  - Git
  - Visual Studio Code extensions:
    - C# extension
    - Docker extension
    - Remote - Containers extension
    - Any other relevant extensions specified in `devcontainer.json`

## Getting Started

### Prerequisites

- Docker: Ensure that Docker is installed and running on your machine.
- Visual Studio Code: Install Visual Studio Code along with the Remote - Containers extension.

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/vpetkovic/dotnet-devcontainers-demo.git
   cd dotnet-devcontainers-demo
   ```

2. Open the repository in Visual Studio Code:
   ```sh
   code .
   ```

3. When prompted, reopen the repository in the container. Visual Studio Code will use the `devcontainer.json` and `Dockerfile` to build and start the development container.

### Remote Development

- **Using SSH**: You can use SSH for remote development with Visual Studio Code or JetBrains Rider. This allows you to develop on a remote server with the same development environment.
- **Using Browser**: If you are using this in a browser and do not have access to proprietary Microsoft extensions like DevKit, you can use third-party debugging extensions for .NET.

### Usage

- Open a terminal within Visual Studio Code.
- Run the .NET application using the following command:
  ```sh
  dotnet run
  ```

### Features and Testing

- **Container Interaction**: Demonstrate configurable container interaction by passing down the connection string from the MSSQL container to the Weather API container.
- **Docker-in-Docker**: Comment out the part in `docker-compose.yml` that spins up the Redis container. Run the Redis container manually within the development container environment to showcase Docker-in-Docker functionality. Use the following command to spin up the Redis container, which will be used by the Backend API:
  ```sh
  docker run -d --name redis-container -p 6379:6379 redis
  ```
- **Backend and Frontend Development**: The backend service utilizes Redis and Weather API services that are spun up in containers. The Redis container is used for caching and fast data retrieval, while the Weather API container provides weather data for the application. The frontend service interacts with the backend service to display the weather data and other information retrieved from the backend.
- **Debugging**: The development container is set up to support debugging of the .NET application within Visual Studio Code. For browser-based development, use third-party debugging extensions for .NET if proprietary Microsoft extensions are not available.
- **Unit Testing**: The setup includes support for running unit tests within the development container to ensure code quality and correctness.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
