FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["LSS.Users.Api/LSS.Users.Api.csproj", "LSS.Users.Api/"]

RUN dotnet restore "LSS.Users.Api/LSS.Users.Api.csproj"
COPY . .
WORKDIR "/src/LSS.Users.Api"
RUN dotnet build "LSS.Users.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "LSS.Users.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "LSS.Users.Api.dll"]