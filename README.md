# ASP.NET Core JWT Token Authentication

There are two ASP.NET Core Web API apps. The user app accepts a valid username and password to generate a JWT Token. The token is returned to the user and can be used for authorization in the user app or the membership app.

# How?
ASP.NET Core supports JWT Authentication out of the box and can be configured in `Startup.cs`. There is also some library support for generating the JWT token, which is found in `JWTTokenManager`.
