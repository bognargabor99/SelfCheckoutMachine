# SelfCheckoutMachine

The application needs .NET 8 to run and a local mysql database which can be hosted with docker by running `docker-compose up` in the root folder of the project

The application can be started by running 'dotnet run' in cmd. The API can be accessed through http://localhost:5202.

You can also launch it inside Visual Studio. In this case, the API can have an HTTPS binding through port no. 7134

The above settings can be changed in `Properties/lauchSettings.json`

Endpoints:
GET  /api/v1/Stock
POST /api/v1/Stock
POST /api/v1/Checkout