# A-cant

[![Build status](https://ci.appveyor.com/api/projects/status/j5cjvxo0eou297e3/branch/master?svg=true)](https://ci.appveyor.com/project/JohnKennedy/a-cant/branch/master)

This is a [.NET Core](https://dotnet.microsoft.com/) app with an [Aurelia](https://aurelia.io/) backed client side. These two apps run on different ports, the API needs to be started first.

## Instructions
### Server
+ Ensure you have installed the .NET Core 3.1 SDK
+ In your terminal, navigate to the project root and run the following commands:
    + `dotnet restore ./Hahn.ApplicatonProcess.May2020.Data/Hahn.ApplicatonProcess.May2020.Data.csproj`.
    + `dotnet restore ./Hahn.ApplicatonProcess.May2020.Domain/Hahn.ApplicatonProcess.May2020.Domain.csproj`.
    + `dotnet restore ./Hahn.ApplicatonProcess.May2020.Web/Hahn.ApplicatonProcess.May2020.Web.csproj`.
    > + Visual Studio automatically does this for you.
    > + Visual Studio Code requires you manually do this.
    + `cd Hahn.ApplicatonProcess.May2020.Web`
    + `dotnet run`
+ When the API is started you can navigate to https://localhost:6131/swagger to view the API documentation.

### Client
As mentioned previously, the client app is built on Aurelia, so Aurelia needs to be installed.
+ In your terminal, run the command: `npm i -g aurelia-cli` or `yarn global add aurelia-cli`, this installs the Aurelia CLI thereby making the `au` command available system wide.
+ Now in your terminal, navigate to the project's root directory, then into the `Hahn.ApplicatonProcess.May2020.Web` directory and finally into the `ClientApp` directory. This is where the client facing app sits.
+ While in that directory, run `npm install` to install app's dependencies then run `au run --open`
+ The app is then available at http://localhost:8080.

## Tests
On the terminal, in the project the dependencies needs to be installed if not previously done.
+ Run `dotnet restore ./Hahn.ApplicatonProcess.Application.Tests/Hahn.ApplicatonProcess.Application.Tests.csproj` to restore the test project's dependencies.
+ `cd Hahn.ApplicatonProcess.Application.Tests`
+ `dotnet run`

