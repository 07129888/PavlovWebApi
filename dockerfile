FROM microsoft/dotnet:2.1-sdk as compiler
COPY . /root/src
RUN dotnet publish -c Release -v Detailed -o /root/PavlovWebApi /root/src

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 as base
COPY --from=compiler /root/PavlovWebApi /root/PavlovWebApi
WORKDIR /root/PavlovWebApi
CMD ["dotnet", "PavlovWebApi.dll"]