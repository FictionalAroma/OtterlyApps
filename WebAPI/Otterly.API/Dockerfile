FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Otterly.API/Otterly.API.csproj", "Otterly.API/"]
COPY ["Lib/Otterly.Database/Otterly.Database.UserData.csproj", "Lib/Otterly.Database/"]
COPY ["Lib/Otterly.ClientLib/Otterly.API.ClientLib.csproj", "Lib/Otterly.ClientLib/"]
COPY ["Otterly.Site/Otterly.Site.csproj", "Otterly.Site/"]
COPY ["Lib/Otterly.Database.Mongo/Otterly.Database.ActivityData.csproj", "Lib/Otterly.Database.Mongo/"]
COPY ["Lib/Otterly.API.DataObjects/Otterly.API.DataObjects.csproj", "Lib/Otterly.API.DataObjects/"]
COPY ["Lib/Otterly.AWS/LDSoft.AWS.csproj", "Lib/Otterly.AWS/"]
COPY ["Lib/LDSoft.APIClient/LDSoft.APIClient.csproj", "Lib/LDSoft.APIClient/"]
RUN dotnet restore "Otterly.API/Otterly.API.csproj"
COPY . .
WORKDIR "/src/Otterly.API"
RUN dotnet build "Otterly.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_14.x | bash \
    && apt-get install nodejs -yq
RUN dotnet publish "Otterly.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otterly.API.dll"]
