FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ["src/RedditTopMemes.Web.Host/RedditTopMemes.Web.Host.csproj", "src/RedditTopMemes.Web.Host/"]
COPY ["src/RedditTopMemes.Web.Core/RedditTopMemes.Web.Core.csproj", "src/RedditTopMemes.Web.Core/"]
COPY ["src/RedditTopMemes.Application/RedditTopMemes.Application.csproj", "src/RedditTopMemes.Application/"]
COPY ["src/RedditTopMemes.Core/RedditTopMemes.Core.csproj", "src/RedditTopMemes.Core/"]
COPY ["src/RedditTopMemes.EntityFrameworkCore/RedditTopMemes.EntityFrameworkCore.csproj", "src/RedditTopMemes.EntityFrameworkCore/"]
WORKDIR "/src/src/RedditTopMemes.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/RedditTopMemes.Web.Host", "src/RedditTopMemes.Web.Host"]
COPY ["src/RedditTopMemes.Web.Core", "src/RedditTopMemes.Web.Core"]
COPY ["src/RedditTopMemes.Application", "src/RedditTopMemes.Application"]
COPY ["src/RedditTopMemes.Core", "src/RedditTopMemes.Core"]
COPY ["src/RedditTopMemes.EntityFrameworkCore", "src/RedditTopMemes.EntityFrameworkCore"]
WORKDIR "/src/src/RedditTopMemes.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "RedditTopMemes.Web.Host.dll"]
