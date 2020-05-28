# A-cant

This is a [.NET Core](https://dotnet.microsoft.com/) app with an [Aurelia](https://aurelia.io/) backed client side. These two apps run on different ports, the API needs to be started first.

## Instructions
### Server
+ Ensure you have installed the .NET Core 3.1 SDK
+ In your terminal, navigate to the project root and run `dotnet restore`. This commands will download all the project dependencies.
    + Visual Studio automatically does this for you.
    + Visual Studio Code requires you manually do this.
+ When the API is started you can navigate to https://localhost:6131/swagger to view the API documentation.

### Client
As mentioned previously, the client app is built on Aurelia, so Aurelia needs to be installed.
+ In your terminal, run the command: `npm i -g aurelia-cli` or `yarn global add aurelia-cli`, this installs the Aurelia CLI thereby making the `au` command available system wide.
+ Now in your terminal, navigate to the project's root directory, then into the `Hahn.ApplicatonProcess.May2020.Web` directory and finally into the `ClientApp` directory. This is where the client facing app sits.
+ While in that directory, run `npm install` to install app's dependencies then run `au run --open`
+ The app is then available at http://localhost:8080.

