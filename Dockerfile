FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copier tous les csproj nécessaires
COPY CheckMyStar.Apis/*.csproj CheckMyStar.Apis/
COPY CheckMyStar.Apis.Services/*.csproj CheckMyStar.Apis.Services/
COPY CheckMyStar.Apis.Services.Abstractions/*.csproj CheckMyStar.Apis.Services.Abstractions/
COPY CheckMyStar.Apis.Services.Models/*.csproj CheckMyStar.Apis.Services.Models/
COPY CheckMyStar.Bll/*.csproj CheckMyStar.Bll/
COPY CheckMyStar.Bll.Abstractions/*.csproj CheckMyStar.Bll.Abstractions/
COPY CheckMyStar.Bll.Filters/*.csproj CheckMyStar.Bll.Filters/
COPY CheckMyStar.Bll.Mappings/*.csproj CheckMyStar.Bll.Mappings/
COPY CheckMyStar.Bll.Models/*.csproj CheckMyStar.Bll.Models/
COPY CheckMyStar.Bll.Requests/*.csproj CheckMyStar.Bll.Requests/
COPY CheckMyStar.Bll.Responses/*.csproj CheckMyStar.Bll.Responses/
COPY CheckMyStar.Dal/*.csproj CheckMyStar.Dal/
COPY CheckMyStar.Dal.Abstractions/*.csproj CheckMyStar.Dal.Abstractions/
COPY CheckMyStar.Dal.Models/*.csproj CheckMyStar.Dal.Models/
COPY CheckMyStar.Dal.Results/*.csproj CheckMyStar.Dal.Results/
COPY CheckMyStar.Data/*.csproj CheckMyStar.Data/
COPY CheckMyStar.Data.Abstraction/*.csproj CheckMyStar.Data.Abstraction/
COPY CheckMyStar.Database/*.csproj CheckMyStar.Database/
COPY CheckMyStar.Enumerations/*.csproj CheckMyStar.Enumerations/
COPY CheckMyStar.Logger/*.csproj CheckMyStar.Logger/
COPY CheckMyStar.Security/*.csproj CheckMyStar.Security/

# Restaurer les dépendances
RUN dotnet restore CheckMyStar.Apis/CheckMyStar.Apis.csproj

# Copier tout le code
COPY . .

# Publier l'API
RUN dotnet publish CheckMyStar.Apis/CheckMyStar.Apis.csproj -c Release -o /app/publish

# Image finale
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80

ENTRYPOINT ["dotnet", "CheckMyStar.Apis.dll"]
