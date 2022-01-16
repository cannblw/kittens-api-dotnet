# Kittens API

API to return images of kittens upside down. Requires authentication.

## How to run

1. Set secrets required by appsettings.json. It's very important to set "Settings.Authentication.JwtSecret" to be able to generate JWT tokens.

2. Run using an IDE or with the command `dotnet run`.

It will run by default in http://localhost:5177 and the Swagger documentation will be available in http://localhost:5177/swagger/index.html

## What is missing

Everything should be working except for the refresh-token endpoint. This has been left out intentionally because rolling out your own authentication using .NET Core+ is not very well documented and takes some time to figure out, especially if it wants to be done in a clean way. I did provide the endpoints to generate JWT tokens, and the refresh-token process should be similar to what is already done.