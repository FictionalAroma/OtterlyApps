#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Otterly.API/Otterly.API.csproj", "Otterly.API/"]
COPY ["Lib/Otterly.AWS/LDSoft.AWS.csproj", "Lib/Otterly.AWS/"]
COPY ["Lib/LDSoft.APIClient/LDSoft.APIClient.csproj", "Lib/LDSoft.APIClient/"]
COPY ["Lib/Otterly.ClientLib/Otterly.API.ClientLib.csproj", "Lib/Otterly.ClientLib/"]
COPY ["Lib/Otterly.API.DataObjects/Otterly.API.DataObjects.csproj", "Lib/Otterly.API.DataObjects/"]
COPY ["Lib/Otterly.Database.Mongo/Otterly.Database.ActivityData.csproj", "Lib/Otterly.Database.Mongo/"]
COPY ["Lib/Otterly.Database/Otterly.Database.UserData.csproj", "Lib/Otterly.Database/"]
RUN dotnet restore "Otterly.API/Otterly.API.csproj"
COPY . .
WORKDIR "/src/Otterly.API"
RUN dotnet build "Otterly.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Otterly.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Otterly.API.dll"]