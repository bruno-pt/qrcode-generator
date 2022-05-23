# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
RUN apt-get install -y libfreetype6
RUN apt-get install -y libfontconfig1
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

#ENTRYPOINT ["dotnet", "aspnetapp.dll"]
CMD ASPNETCORE_URLS="http://*:$PORT" dotnet APIQRCode.dll