FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 50731
EXPOSE 44328

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY IdentityServer4.Study.ServerDB/IdentityServer4.Study.ServerDB.csproj IdentityServer4.Study.ServerDB/
RUN dotnet restore IdentityServer4.Study.ServerDB/IdentityServer4.Study.ServerDB.csproj
COPY . .
WORKDIR /src/IdentityServer4.Study.ServerDB
RUN dotnet build IdentityServer4.Study.ServerDB.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish IdentityServer4.Study.ServerDB.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityServer4.Study.ServerDB.dll"]
