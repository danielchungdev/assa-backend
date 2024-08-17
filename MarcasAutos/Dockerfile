FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY ["MarcasAutos.csproj", "./"]

RUN dotnet restore "MarcasAutos.csproj"

COPY . .

RUN dotnet build "MarcasAutos.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "MarcasAutos.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MarcasAutos.dll"]