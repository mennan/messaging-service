FROM mcr.microsoft.com/dotnet/core/sdk:3.1-bionic AS builder
WORKDIR /source

COPY . .

# Change the Directory
WORKDIR /source/

# aspnet-core
RUN dotnet restore src/Api/MessagingService.Api/MessagingService.Api.csproj
RUN dotnet publish src/Api/MessagingService.Api/MessagingService.Api.csproj --output /messagingservice/ --configuration Release

## Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic

# Change the Directory
WORKDIR /messagingservice

COPY --from=builder /messagingservice .
ENTRYPOINT ["dotnet", "MessagingService.Api.dll"]
