FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 50138
EXPOSE 44330

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY IdentityServer4.Study.Server/IdentityServer4.Study.Server.csproj IdentityServer4.Study.Server/
RUN dotnet restore IdentityServer4.Study.Server/IdentityServer4.Study.Server.csproj
COPY . .
WORKDIR /src/IdentityServer4.Study.Server
RUN dotnet build IdentityServer4.Study.Server.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish IdentityServer4.Study.Server.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityServer4.Study.Server.dll"]
