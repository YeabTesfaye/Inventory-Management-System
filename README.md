# Inventory Management System

The Inventory Management .NET Web API provides a robust backend solution for managing inventory. It includes a wide range of features designed to support modern web applications with efficient, secure, and scalable functionality.

## Table of Contents

## Table of Contents

- [Key Features](#key-features)
  - [CORS (Cross-Origin Resource Sharing)](#1-cors-cross-origin-resource-sharing)
  - [Logging Service](#2-logging-service)
  - [Repository Pattern](#3-repository-pattern)
  - [DTO (Data Transfer Object) Classes](#4-dto-data-transfer-object-classes)
  - [Global Error Handling](#5-global-error-handling)
  - [Model Validation](#6-model-validation)
  - [Asynchronous Code](#7-asynchronous-code)
  - [Modular Design](#8-modular-design)
  - [Advanced Querying Capabilities](#9-advanced-querying-capabilities)
    - Paging
    - Filtering
    - Searching
    - Sorting
  - [Rate Limiting](#10-rate-limiting)
  - [JWT and Identity for Authentication and Authorization](#11-jwt-and-identity-for-authentication-and-authorization)
  - [API Documentation with Swagger](#12-api-documentation-with-swagger)
- [API Routes](#api-routes)
  - [Authentication Controller](#authentication-controller)
  - [Customer Controller](#customer-controller)
  - [Item Controller](#item-controller)
  - [Order Controller](#order-controller)
  - [Product Controller](#product-controller)
  - [Supplier Controller](#supplier-controller)
  - [Token](#token)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Usage](#usage)
- [Contributing](#contributing)
- [Conclusion](#conclusion)
- [License](#license)

## Key Features

### 1. CORS (Cross-Origin Resource Sharing)

- Configured CORS to allow cross-origin requests from any origin, method, and header.
- Exposed pagination metadata in the headers.

### 2. Logging Service

- Implemented a centralized logging service to capture and manage logs, aiding in monitoring and debugging.

### 3. Repository Pattern

- Employed the repository pattern to abstract data access logic, making the code more modular and maintainable.

### 4. DTO (Data Transfer Object) Classes

- Used DTOs to ensure a clear separation between the data models and API responses, improving data integrity and security.

### 5. Global Error Handling

- Integrated global error handling to manage exceptions consistently across the entire application.

### 6. Model Validation

- Implemented model validation to ensure data integrity before processing requests.

### 7. Asynchronous Code

- Utilized asynchronous programming to improve performance and scalability.

### 8. Modular Design

- Structured the codebase into different classes and layers to promote modularity and reusability.

### 9. Advanced Querying Capabilities

- **Paging**: Implemented pagination to handle large datasets efficiently.
- **Filtering**: Added filtering capabilities to retrieve data based on specific criteria.
- **Searching**: Integrated search functionality to allow quick data retrieval.
- **Sorting**: Enabled sorting to organize data based on user-defined parameters.

### 10. Rate Limiting

- Implemented rate limiting to control the number of requests a client can make to the API, ensuring fair use and preventing abuse.

### 11. JWT and Identity for Authentication and Authorization

- Utilized JWT and ASP.NET Core Identity for secure authentication and role-based authorization.
- Implemented refresh tokens for maintaining session security.

### 12. API Documentation with Swagger

- Documented the API endpoints and models using Swagger for easy exploration and testing.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or any other compatible database)

### Installation

1. Clone the repository:

   ```bash
   https://github.com/YeabTesfaye/Inventory-Management-System
   cd Ultimate-Dotnet-Web-API
   ```

2. Set up the database:

   - Update the connection string in `appsettings.json`.
   - Apply migrations:

     ```bash
     dotnet ef database update
     ```

3. Build the project:

   ```bash
   dotnet build
   ```

4. Run the project:

   ```bash
   dotnet run
   ```

### Usage

- Access the Swagger documentation at `http://localhost:3000/swagger` to explore and test the API endpoints.

## API Routes

### Authentication Controller

**I Register a new user**

- **POST** `http://localhost:3000/api/auth`
- **Body:**

  ```json
  {
    "firstName": "string",
    "lastName": "string",
    "userName": "string",
    "password": "string",
    "email": "string",
    "phoneNumber": "string",
    "roles": ["string"]
  }
  ```

  **I Login a user**

- **POST** `http://localhost:3000/api/auth/login`
- **Body:**

```json
{
  "username": "string",
  "password": "string"
}
```

## Customer Controller

The Customer Controller provides endpoints to manage customer data in the API. It supports CRUD operations, allowing you to create, retrieve, update, and delete customer information.

## API Routes

### I Retrieve a Customer by Their Unique Identifier

- **GET** `http://localhost:3000/api/customer/{customerId:guid}`
- **Description**: Retrieves customer details based on the unique identifier.
- **Parameters**:
  - `customerId` (guid): The unique identifier of the customer.
- **Responses**:
  - **200 OK**: Returns the customer details if found.
  - **404 Not Found**: If the customer is not found.

## Order Controller

### I Retrieve All Items Associated with a Specific Order

- **GET** `http://localhost:3000/api/orders/{orderId}/items`
- **Description**: Retrieves all items associated with a specific order.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
  - `itemParameters` (query parameters): Parameters for pagination and filtering.
- **Responses**:
  - **200 OK**: Returns a paginated list of items associated with the order.
- **Headers**:
  - `X-Pagination`: Contains pagination metadata.

### II Retrieve a Specific Item by Its Unique Identifier

- **GET** `http://localhost:3000/api/orders/{orderId}/items/{itemId:guid}`
- **Description**: Retrieves a specific item by its unique identifier.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
  - `itemId` (guid): The unique identifier of the item.
- **Responses**:
  - **200 OK**: Returns the item details if found.
  - **404 Not Found**: If the item is not found.

### III Retrieve Items Associated with a Specific Product within an Order

- **GET** `http://localhost:3000/api/orders/{orderId}/items/product/{productId:guid}`
- **Description**: Retrieves items associated with a specific product within an order.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
  - `productId` (guid): The unique identifier of the product.
- **Responses**:
  - **200 OK**: Returns a list of items associated with the product within the order.

### IV Create a New Item Within a Specific Order

- **POST** `http://localhost:3000/api/orders/{orderId}/items`
- **Description**: Creates a new item within a specific order.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
- **Body**:
  ```json
  {
    "productId": "guid",
    "quantity": "integer",
    "price": "decimal"
  }
  ```

### Delete a Customer by Their Unique Identifier

- **DELETE** `http://localhost:3000/api/customer/{customerId:guid}`

### IV Update an Existing Customer by Their Unique Identifier

- **PUT** `http://localhost:3000/api/customer/{customerId:guid}`
  -- **Body**

```json
{
  "firstName": "string",
  "lastName": "string",
  "email": "string",
  "phoneNumber": "string",
  "address": "string"
}
```

# Item Controller

The Item Controller provides endpoints to manage items within orders. It supports operations for retrieving, creating, updating, and deleting items.

## API Routes

### I Retrieve All Items Associated with a Specific Order

- **GET** `http://localhost:3000/api/orders/{orderId}/items`
- **Description**: Retrieves all items associated with a specific order.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
  - `itemParameters` (query parameters): Includes options for pagination and filtering.
- **Responses**:
  - **200 OK**: Returns a paginated list of items associated with the order.

### II Retrieve a Specific Item by Its Unique Identifier

- **GET** `http://localhost:3000/api/orders/{orderId}/items/{itemId:guid}`
- **Description**: Retrieves details of a specific item within an order.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
  - `itemId` (guid): The unique identifier of the item.
- **Responses**:
  - **200 OK**: Returns the item details if found.
  - **404 Not Found**: If the item is not found.

### III Retrieve Items Associated with a Specific Product within an Order

- **GET** `http://localhost:3000/api/orders/{orderId}/items/product/{productId:guid}`
- **Description**: Retrieves items associated with a specific product within an order.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
  - `productId` (guid): The unique identifier of the product.
- **Responses**:
  - **200 OK**: Returns the list of items for the specified product.

### IV Create a New Item Within a Specific Order

- **POST** `http://localhost:3000/api/orders/{orderId}/items`
- **Description**: Creates a new item within a specific order.
- **Body**:
  ```json
  {
    "productId": "guid",
    "quantity": "integer",
    "price": "decimal"
  }
  ```

### I Retrieve All Orders Associated with a Specific Customer

- **GET** `http://localhost:3000/api/customers/{customerId}/orders`
- **Description**: Retrieves all orders associated with a specific customer.
- **Parameters**:
  - `customerId` (guid): The unique identifier of the customer.
  - `orderParameters` (query parameters): Parameters for pagination and filtering.
- **Responses**:
  - **200 OK**: Returns a paginated list of orders for the specified customer.
- **Headers**:
  - `X-Pagination`: Contains pagination metadata.

### II Retrieve a Specific Order by Its Unique Identifier

- **GET** `http://localhost:3000/api/customers/{customerId}/orders/{orderId:guid}`
- **Description**: Retrieves a specific order by its unique identifier within a customer’s orders.
- **Parameters**:
  - `orderId` (guid): The unique identifier of the order.
  - `customerId` (guid): The unique identifier of the customer.
- **Responses**:
  - **200 OK**: Returns the details of the specified order.
  - **404 Not Found**: If the order or customer is not found.

### III Create a New Order for a Specific Customer

- **POST** `http://localhost:3000/api/customers/{customerId}/orders`
- **Description**: Creates a new order for a specific customer.
- **Parameters**:
  - `customerId` (guid): The unique identifier of the customer.
- **Body**:
  ```json
  {
    "productId": "guid",
    "quantity": "integer",
    "price": "decimal"
  }
  ```

## Product Controller

### I Retrieve All Products Associated with a Specific Supplier

- **GET** `http://localhost:3000/api/suppliers/{supplierId}/products`
- **Description**: Retrieves all products associated with a specific supplier.
- **Parameters**:
  - `supplierId` (guid): The unique identifier of the supplier.
  - `productParameters` (query parameters): Parameters for pagination and filtering.
- **Responses**:
  - **200 OK**: Returns a paginated list of products for the specified supplier.
- **Headers**:
  - `X-Pagination`: Contains pagination metadata.

### II Retrieve a Specific Product by Its Unique Identifier

- **GET** `http://localhost:3000/api/suppliers/{supplierId}/products/{productId:guid}`
- **Description**: Retrieves a specific product by its unique identifier within a supplier’s products.
- **Parameters**:
  - `productId` (guid): The unique identifier of the product.
  - `supplierId` (guid): The unique identifier of the supplier.
- **Responses**:
  - **200 OK**: Returns the details of the specified product.
  - **404 Not Found**: If the product or supplier is not found.

### III Create a New Product for a Specific Supplier

- **POST** `http://localhost:3000/api/suppliers/{supplierId}/products`
- **Description**: Creates a new product for a specific supplier.
- **Parameters**:
  - `supplierId` (guid): The unique identifier of the supplier.
- **Body**:
  ```json
  {
    "name": "string",
    "description": "string",
    "price": "decimal",
    "stockQuantity": "integer"
  }
  ```

## Supplier Controller

The `SupplierController` provides endpoints for managing suppliers in the API. It includes operations for retrieving, creating, updating, and deleting suppliers. All endpoints are secured and require appropriate authorization.

## Endpoints

### GetSuppliers

- **URL:** `/api/suppliers`
- **Method:** `GET`
- **Description:** Retrieves all suppliers with optional filtering and pagination.
- **Query Parameters:**
  - `supplierParameters` - Query parameters for pagination and filtering.
- **Response:**
  - `200 OK` - Returns a paginated list of suppliers with pagination metadata in the `X-Pagination` header.
  - `401 Unauthorized` - If the user is not authorized.

### GetSupplierById

- **URL:** `/api/suppliers/{id:guid}`
- **Method:** `GET`
- **Description:** Retrieves a specific supplier by its unique identifier.
- **URL Parameters:**
  - `id` - The unique identifier of the supplier.
- **Response:**
  - `200 OK` - Returns the details of the specified supplier.
  - `404 Not Found` - If the supplier with the specified ID is not found.
  - `401 Unauthorized` - If the user is not authorized.

### CreateSupplier

- **URL:** `/api/suppliers`
- **Method:** `POST`
- **Description:** Creates a new supplier.
- **Request Body:**
  - `supplier` - The supplier data to create, in the form of `SupplierForCreationDto`.
- **Response:**
  - `201 Created` - Returns the created supplier with a location header pointing to the `GetSupplierById` action.
  - `400 Bad Request` - If the supplier data is null.
  - `422 Unprocessable Entity` - If the model state is invalid.
  - `401 Unauthorized` - If the user is not authorized.

### DeleteSupplier

- **URL:** `/api/suppliers/{supplierId:guid}`
- **Method:** `DELETE`
- **Description:** Deletes a supplier by its unique identifier.
- **URL Parameters:**
  - `supplierId` - The unique identifier of the supplier to delete.
- **Response:**
  - `204 No Content` - If the supplier is successfully deleted.
  - `404 Not Found` - If the supplier with the specified ID is not found.
  - `401 Unauthorized` - If the user is not authorized.
  - **Role Required:** `Administrator`

### UpdateSupplier

- **URL:** `/api/suppliers/{supplierId:guid}`
- **Method:** `PUT`
- **Description:** Updates an existing supplier by its unique identifier.
- **Request Body:**
  - `supplier` - The updated supplier data, in the form of `SupplierForUpdateDto`.
  - **URL Parameters:**
  - `supplierId` - The unique identifier of the supplier to update.
- **Response:**
  - `204 No Content` - If the supplier is successfully updated.
  - `400 Bad Request` - If the supplier data is null.
  - `422 Unprocessable Entity` - If the model state is invalid.
  - `401 Unauthorized` - If the user is not authorized.
  - **Role Required:** `Manager`

## Token

**I Retrieve a refersh token**

- **POST** `http://localhost:3000/api/token/refresh`

-**body**

```json
{
  "token": "string",
  "refreshToken": "string"
}
```

## Contributing

We welcome contributions to improve this project. Please follow these guidelines:

1. Fork the repository.

2. Create a new branch:

```bash
   git checkout -b feature/your-feature
```

3. Commit your changes:

```bash
git commit -am 'Add new feature'
```

4. Push to the branch:

```bash
git push origin feature/your-feature




## Conclusion

The Inventory Management System is designed to be scalable, secure, and easy to use. We encourage contributions and feedback to continually enhance the project.

## License

[MIT](https://choosealicense.com/licenses/mit/)
```
