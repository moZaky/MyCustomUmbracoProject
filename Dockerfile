	#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

	FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
	WORKDIR /app
	EXPOSE 80
	EXPOSE 443

	# Build image
	FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
	WORKDIR /src
	COPY ["UmbracoBaseCore.csproj", "."]
	RUN dotnet restore "./UmbracoBaseCore.csproj"
	COPY . .
	WORKDIR "/src/."
	RUN dotnet build "UmbracoBaseCore.csproj" -c Release -o /app/build

	FROM build AS publish
	RUN echo "publish command start"

	#npm
	RUN apt-get update && apt-get install -y curl
	RUN curl -sL https://deb.nodesource.com/setup_16.x | bash -
	RUN apt-get install -y nodejs
	WORKDIR "/src/MyCustomUmbracoProject/ClientApp"
	RUN echo "publish workdir"
	RUN echo "running commands"

	RUN npm install
	RUN echo "running npm install"
	RUN npm run start-dev
	RUN echo "running npm  startdev"
	#end npm
	RUN dotnet publish "MyCustomUmbracoProject.csproj" -c Release -o /app/publish /p:UseAppHost=false
	RUN echo "dotnet publish"

	FROM base AS final
	WORKDIR /app

	# Copy the built dotnet app to the container
	COPY --from=publish /app/publish .

	# Copy the umbraco-app folder from the build stage to the final stage
	COPY --from=build /src/MyCustomUmbracoProject/ClientApp/dist/umbraco-app /app/ClientApp/dist/umbraco-app

	ENTRYPOINT ["dotnet", "MyCustomUmbracoProject.dll"]

