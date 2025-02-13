# Personal Library Tracking API

This is a C# Web API for tracking a personal library, using SQLite as the database provider and Google Single Sign-On (SSO) for authentication.

## Features
- User authentication via Google SSO
- CRUD operations for books
- SQLite database for lightweight storage
- Secure API endpoints with authentication

## Technologies Used
- C#
- .NET Core Web API
- SQLite
- Entity Framework Core
- Google Authentication (OAuth 2.0)

## Prerequisites
- .NET SDK installed ([Download here](https://dotnet.microsoft.com/download))
- SQLite installed ([Download here](https://www.sqlite.org/download.html))
- Google Developer Console setup for OAuth 2.0 ([Guide](https://developers.google.com/identity/sign-in/web/sign-in))

## Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/personal-library-tracker.git
cd personal-library-tracker
```

### 2. Configure Google SSO
1. Register your application in the [Google Developer Console](https://console.developers.google.com/).
2. Create OAuth 2.0 credentials and obtain the Client ID and Client Secret.
3. Update `appsettings.json`:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "YOUR_CLIENT_ID",
      "ClientSecret": "YOUR_CLIENT_SECRET"
    }
  }
}
```

### 3. Setup the Database
Run the following command to apply migrations and create the database:
```bash
dotnet ef database update
```

### 4. Run the API
```bash
dotnet run
```
The API will be available at `http://localhost:5000` or `https://localhost:5001`.

## API Endpoints

### Authentication
- `POST /auth/google-login` - Login with Google SSO

### Books
- `GET /books` - Get all books
- `GET /books/{id}` - Get book by ID
- `POST /books` - Add a new book
- `PUT /books/{id}` - Update book details
- `DELETE /books/{id}` - Delete a book

## Deployment
For deployment, consider hosting on:
- Azure App Services
- AWS Lambda with API Gateway
- DigitalOcean or other cloud providers

## License
This project is licensed under the MIT License. See `LICENSE` for details.

## Author
Seth Rearick - [Your Contact/Portfolio]

## Contributions
Contributions are welcome! Feel free to fork and submit a pull request.

