FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Tig.Crebo.Etl/Tig.Crebo.Etl.csproj", "Tig.Crebo.Etl/"]
RUN dotnet restore "src/Tig.Crebo.Etl/Tig.Crebo.Etl.csproj"
COPY . .
WORKDIR "/src/Tig.Crebo.Etl"
RUN dotnet build "Tig.Crebo.Etl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tig.Crebo.Etl.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tig.Crebo.Etl.dll"]
