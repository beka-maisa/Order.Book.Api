# Order Book Api

An implementation of Order Book where the user can add/edit/delete orders.

## Requirements

- .NET 8 SDK
- Asp.net Web Api
- ORM: EF Core
- Database: PostgreSQL;
- Websocket Library: SignalR - ()


## Setup

1. Clone the repository.
2. Update the connection string in `appsettings.json`.
3. Run migrations to create the database schema: `dotnet ef database update`.
4. Run the application: `dotnet run`.

## API Endpoints

- `POST /api/v1/create-order`: Create order endpoint.
- `GET /api/v1/get-order-by-id`: Get order by id endpoint.
- `GET /api/v1/get-all-orders`: Get all orders list endpoint.
- `POST /api/v1/update-by-id`: Update order by id endpoint.
- `POST /api/v1/delete-by-id`: Delete order by id endpoint.