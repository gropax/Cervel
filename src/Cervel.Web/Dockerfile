#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y build-essential nodejs
WORKDIR /src
COPY ["src/Cervel.Web/Cervel.Web.csproj", "src/Cervel.Web/"]
COPY ["src/Cervel.DateParser/Cervel.TimeParser.csproj", "src/Cervel.DateParser/"]
RUN dotnet restore "src/Cervel.Web/Cervel.Web.csproj"
COPY . .
WORKDIR "/src/src/Cervel.Web"
RUN dotnet build "Cervel.Web.csproj" -c Release -o /app/build # -p:Version=`cat version`

FROM build AS publish
RUN dotnet publish "Cervel.Web.csproj" -c Release -o /app/publish # -p:Version=`cat version`

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cervel.Web.dll"]