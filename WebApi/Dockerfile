FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ../ .
RUN apk add --update nodejs npm
WORKDIR "/src/Web"
RUN npm i
WORKDIR "/src/WebApi"
RUN dotnet publish "WebApi.csproj" -c $BUILD_CONFIGURATION -o /publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /publish .
RUN apk add curl
ENTRYPOINT ["dotnet", "WebApi.dll"]
