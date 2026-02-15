FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copier tous les csproj de tous les projets
COPY CheckMyStar.Apis/*.csproj CheckMyStar.Apis/
COPY CheckMyStar.Apis.Services/*.csproj CheckMyStar.Apis.Services/
COPY CheckMyStar.Apis.Services.Abstractions/*.csproj CheckMyStar.Apis.Services.Abstractions/
COPY CheckMyStar.Apis.Services.Models/*.csproj CheckMyStar.Apis.Services.Models/
COPY CheckMyStar.Bll/*.csproj CheckMyStar.Bll/
COPY CheckMyStar.Bll.Abstractions/*.csproj CheckMyStar.Bll.Abstractions/
COPY CheckMyStar.Bll.Mappings/*.csproj CheckMyStar.Bll.Mappings/
COPY CheckMyStar.Bll.Responses/*.csproj CheckMyStar.Bll.Responses/
COPY CheckMyStar.Bll.Requests/*.csproj CheckMyStar.Bll.Requests/
COPY CheckMyStar.Dal/*.csproj CheckMyStar.Dal/
COPY CheckMyStar.Dal.Abstractions/*.csproj CheckMyStar.Dal.Abstractions/
COPY CheckMyStar.Dal.Models/*.csproj CheckMyStar.Dal.Models/
COPY CheckMyStar.Data/*.csproj CheckMyStar.Data/
COPY CheckMyStar.Data.Abstraction/*.csproj CheckMyStar.Data.Abstraction/
COPY CheckMyStar.Enumerations/*.csproj CheckMyStar.Enumerations/
COPY CheckMyStar.Logger/*.csproj CheckMyStar.Logger/
COPY CheckMyStar.Security/*.csproj CheckMyStar.Security/

# Restaurer
RUN dotnet restore CheckMyStar.Apis/CheckMyStar.Apis.csproj

# Copier tout le code
COPY . .

# Publier
RUN dotnet publish CheckMyStar.Apis/CheckMyStar.Apis.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "CheckMyStar.Apis.dll"]
