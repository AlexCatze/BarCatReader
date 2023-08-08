FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /BarCatReader

# Copy everything
COPY ./BarCatReader ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /BarCatReader
COPY --from=build-env /BarCatReader/out .
ENTRYPOINT ["dotnet", "BarCatReader.dll"]
