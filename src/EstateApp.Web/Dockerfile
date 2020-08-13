# Run Application within a Dotnet Image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101 AS build

# Tell Docker which folder to work in
WORKDIR /app

# Expose the port of app
EXPOSE 5001
EXPOSE 5000

# Switch WorkDirectory because there are two Work files
WORKDIR /app/EstateApp.Data
COPY EstateApp.Data/*csproj .

# Copy everything else
COPY EstateApp.Data/ .

# Switch into Web Project
WORKDIR /app/EstateApp.Web
COPY EstateApp.Web/*csproj .
RUN dotnet restore

COPY EstateApp.Web/ .

# Publish the App
RUN dotnet publish -c Release -o out


# Build runtime image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.101
WORKDIR /app
COPY --from=build /app/EstateApp.Web/out .
ENTRYPOINT ["dotnet", "EstateApp.Web.dll"]


