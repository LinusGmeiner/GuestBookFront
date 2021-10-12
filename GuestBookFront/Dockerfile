#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN mkdir -p /app/release
WORKDIR /app
EXPOSE 80
COPY release /app/release
ENTRYPOINT ["dotnet", "release/GuestBookFront.dll"]