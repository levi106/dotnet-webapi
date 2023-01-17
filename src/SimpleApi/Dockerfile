FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
RUN apt-get update && \
    apt-get install curl gnupg2 software-properties-common -y && \
    curl -sSL https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    apt-add-repository https://packages.microsoft.com/debian/11/prod && \
    apt-get update && \
    apt-get install libmsquic -y && \
    apt-get clean
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SimpleApi.dll"]