#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Otterly.Site/Otterly.Site.csproj", "Otterly.Site/"]
COPY ["Lib/Otterly.AWS/LDSoft.AWS.csproj", "Lib/Otterly.AWS/"]
COPY ["Lib/LDSoft.APIClient/LDSoft.APIClient.csproj", "Lib/LDSoft.APIClient/"]
COPY ["Lib/Otterly.ClientLib/Otterly.API.ClientLib.csproj", "Lib/Otterly.ClientLib/"]
COPY ["Lib/Otterly.API.DataObjects/Otterly.API.DataObjects.csproj", "Lib/Otterly.API.DataObjects/"]
RUN dotnet restore "Otterly.Site/Otterly.Site.csproj"
COPY . .
WORKDIR "/src/Otterly.Site"
RUN dotnet build "Otterly.Site.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Otterly.Site.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otterly.Site.dll"]