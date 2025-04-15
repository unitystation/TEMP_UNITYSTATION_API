# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
WORKDIR /
WORKDIR /app


# Copy and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application source code
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy compiled application from build stage
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "TEMP_UNITYSTATION_API.dll"]

#docker build -t hub_api_unitystation .