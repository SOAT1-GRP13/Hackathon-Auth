# Construindo a aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiando os arquivos de projeto e restaurando as dependências
COPY ./*.sln ./
COPY ./HackathonAuth/*.csproj ./HackathonAuth/
COPY ./Application/*.csproj ./Application/
COPY ./Domain/*.csproj ./Domain/
COPY ./Infra/*.csproj ./Infra/
#COPY ./Tests/HackathonAuth.Tests/*.csproj ./Tests/HackathonAuth.Tests/
#COPY ./Tests/Application.Tests/*.csproj ./Tests/Application.Tests/
#COPY ./Tests/Domain.Tests/*.csproj ./Tests/Domain.Tests/
#COPY ./Tests/Infra.Tests/*.csproj ./Tests/Infra.Tests/

RUN dotnet restore

# Definindo a variável de ambiente para o modo de compilação, padrão é Release
ARG BUILD_CONFIGURATION=Release

# Copiando o código-fonte e compilando a aplicação
COPY . ./
RUN dotnet publish -c $BUILD_CONFIGURATION -o out

# Executando a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

# Expondo a porta da aplicação
EXPOSE 8080

# Iniciar a aplicação quando o contêiner for iniciado
ENTRYPOINT ["dotnet", "HackathonAuth.dll"]