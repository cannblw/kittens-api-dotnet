FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KittensApi/KittensApi.csproj", "KittensApi/"]
RUN dotnet restore "KittensApi/KittensApi.csproj"
COPY . .
WORKDIR "/src/KittensApi"
RUN dotnet build "KittensApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KittensApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KittensApi.dll"]
