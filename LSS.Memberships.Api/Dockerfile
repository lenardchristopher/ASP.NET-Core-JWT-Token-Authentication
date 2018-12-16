FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["LSS.Memberships.Api/LSS.Memberships.Api.csproj", "LSS.Memberships.Api/"]

RUN dotnet restore "LSS.Memberships.Api/LSS.Memberships.Api.csproj"
COPY . .
WORKDIR "/src/LSS.Memberships.Api"
RUN dotnet build "LSS.Memberships.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LSS.Memberships.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LSS.Memberships.Api.dll"]