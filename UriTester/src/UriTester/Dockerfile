FROM microsoft/dotnet


WORKDIR /dotnetapp

# copy project.json and restore as distinct layers
COPY project.json .
RUN dotnet restore

# copy and build everything else
COPY . .
ENV ASPNETCORE_URLS http://+:80
ENV STATSDSERVER localhost
ENV STATSDPREFIX myApp.prod
RUN dotnet publish -c Release -o out
ENTRYPOINT ["dotnet", "out/dotnetapp.dll"]