# SelfCheckoutMachine

The application needs .NET 8 to run and a local mysql database which can be hosted with docker by running `docker-compose up` in the root folder of the project.

### Running the application
- Install .NET 8 runtime on your machine
- Install Docker
- Run `docker-compose up` in the root folder of the project
- If the container is running, execute `dotnet ef database update`
- Execute `dotnet run` in the root  folder of the project
	- The API can be accessed through http://localhost:5202.
	- You can also launch it inside Visual Studio. In this case, the API can have an HTTPS binding through port no. 7134

The port and database settings can be changed in `Properties/lauchSettings.json` and `appsettings.json`

Endpoints:

GET  /api/v1/Stock

POST /api/v1/Stock

POST /api/v1/Checkout
