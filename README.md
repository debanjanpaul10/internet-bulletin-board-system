# Internet Bulletin Board Service (IBBS)

This project is an Internet Bulletin Board Service (IBBS) that allows users to:
- Create and manage user accounts
- Create and publish posts
- Control post visibility (public/private)
- Interact with other users' content

# Getting Started
## Pre-requisites
- .NET SDK (latest)
- Node.js and npm
- SQL Server Management Studio 18 or latest
- Visual Studio 2022+ IDE
- Visual Studio Code or any text editor of choice

## Installation process
- Install the above mentioned software as the first step.
- For the .NET backend:
    - Navigate to the `InternetBulletin.API` folder (API solution root)
    - Open the terminal and run `dotnet build` to install packages and build the solution
    - Once the build is successful, run the project using `dotnet run`
- For the UI:
    - Navigate to the `InternetBulletin.Web` folder (web solution root)
    - In the file `src/app/helpers/config.constants.ts`, update the `ApiBaseUrl` to match your local API URL
    - Open the terminal and run `npm install` to install node dependencies
    - Once complete, run `npm start` to host the application on the development server
- Access the application through the localhost URL provided in the terminal


# Infrastructure
## Build and Deploy
- The application is deployed to Azure services using Github actions.
- The pipeline information is present in `.github/workflows` folder.
- Deployment is handled through GitHub Actions using the `Build and Deploy InternetBulletin` pipeline
- Currently, only `main` and `dev` branch commits should be deployed.
- The pipeline is configured with workflow triggers with selectable stages.
- Deployment consists of two stages:
    - .NET stage: Builds the .NET app using .NET 9.0.x SDK and deploys to Azure App Service.
    - React stage: Builds the frontend and deploys to Azure Static Web App.
    - Database: Build and deploys the SQL Database project to Azure SQL server.

## Branching Strategy
- `main` branch: Contains stable, production-ready code
- `dev` branch: Development branch with latest features (may be unstable)

## Deployment Status
### Main
[![Build and Deploy InternetBulletin](https://github.com/debanjanpaul10/InternetBulletinService/actions/workflows/deploy-to-azure.yml/badge.svg?branch=main)](https://github.com/debanjanpaul10/InternetBulletinService/actions/workflows/deploy-to-azure.yml)

### Development
[![Build and Deploy InternetBulletin](https://github.com/debanjanpaul10/InternetBulletinService/actions/workflows/deploy-to-azure.yml/badge.svg?branch=dev)](https://github.com/debanjanpaul10/InternetBulletinService/actions/workflows/deploy-to-azure.yml)

# Contributing
We welcome contributions! Feel free to:
- Clone the repository
- Create your own branches
- Add new features
- Experiment with new technologies
- Submit pull requests

The only rule is: HAVE FUN!

Happy coding!

