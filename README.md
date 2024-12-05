Microservice_RatingService
Overview
Microservice_RatingService is a simple microservice built with .NET 9 using a Domain-Driven Design (DDD) approach. It manages user ratings for purchases and provides CRUD functionality for ratings, as well as statistical analysis for sellers' ratings.

The project demonstrates best practices in microservice architecture, including integration with tools like SonarQube for code analysis, database migrations using Entity Framework Core, and detailed documentation with Swagger.

Features
CRUD operations for managing ratings.
Statistical analysis of seller ratings (average rating, rating distribution).
Input validation using DataAnnotations.
Logging with Serilog.
Database management using Entity Framework Core and MSSQL.
Automated code analysis with SonarQube.
API documentation using Swagger.

Technologies Used:
.NET 9
Entity Framework Core (Code-First with migrations)
MSSQL (SQL Server) for database
Swagger/OpenAPI for API documentation
SonarQube for code quality and analysis
Serilog for logging
Docker (optional, for containerization)

Getting Started
Prerequisites:
- .NET SDK 9 (Download here)
- MSSQL Server (Download here)
- SonarQube (optional, for code analysis)
- Git (for cloning the repository)

Installation
Clone the Repository:
git clone https://github.com/srdjanbozic/RatingRepository.git
cd RatingRepository

Restore Dependencies: Run the following command to restore NuGet packages:
dotnet restore

Update the Database: Apply migrations to ensure the database is up to date:
dotnet ef database update

Run the Application: Start the project using:
dotnet run --project Microservice_RatingService/Microservice_RatingService.csproj

Access the API:
Swagger UI: https://localhost:7109/swagger
Base API URL: https://localhost:7109

API Endpoints
Ratings
- GET /api/ratings: Get all ratings.
- GET /api/ratings/{id}: Get a specific rating by ID.
- POST /api/ratings: Create a new rating.
- PUT /api/ratings/{id}: Update an existing rating.
- DELETE /api/ratings/{id}: Delete a rating.
- 
Seller Statistics
- GET /api/sellers/{id}/stats: Get statistical data for a seller.
  
Authentication (For Testing)
- POST /api/auth/login: Simulate login and generate a test JWT token.

Project Structure
The project is structured following a Domain-Driven Design (DDD) approach:


├── Application

│   ├── Controllers    
│   ├── Dtos              
│   ├── Interfaces       
├── Domain

│   ├── Entities           
│   ├── ValueObjects        
├── Infrastructure

│   ├── Persistence        
│   ├── Repositories       
│   ├── Middleware         
├── Tests                   

Configuration
Database:
The connection string can be found in appsettings.json:
- "ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=UrisDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}

Logging:
Logging is configured using Serilog. By default, logs are stored in the console and a file:
- Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
    
Running SonarQube Locally
Start the SonarQube Server:
- cd C:\sonarqube\bin\wind
ows-x86-64
- StartSonar.bat

Analyze the Code:
- dotnet sonarscanner begin /k:"RatingService" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="<your_token>"
- dotnet build
- dotnet sonarscanner end /d:sonar.login="<your_token>"

License
- This project is licensed under the MIT License. See LICENSE for details.
