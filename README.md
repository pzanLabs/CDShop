# CDShop E-Commerce Application

## Overview

CDShop is an e-commerce application designed for managing and purchasing various types of products, offering a streamlined experience for both administrators and customers. The application supports content management, product management, order management, and a secure payment gateway through Stripe. It is developed using ASP.NET Core and integrates with a SQL Server database.

The application is structured to cater to two primary user roles: 
1. **Admin/Employee** – responsible for managing products, genres, packages, companies, and orders.
2. **Company/Individual Customers** – able to place and manage orders, with special company privileges for post-shipment payments.

## Table of Contents
- [Directory Structure](#directory-structure)
- [Technologies Used](#technologies-used)
- [Setup and Configuration](#setup-and-configuration)
- [Application Flow](#application-flow)
- [Testing](#testing)

## Directory Structure

The project is structured as follows:

```plaintext
CDShop (root)
├── CDShop.DataAccess        # Database access layer (Repositories, DbContext, Migrations)
├── CDShop.Models            # Data models and view models
├── CDShop.Utility           # Utility classes (Email sender, Stripe configurations)
├── CDShopWeb                # Main web application (Controllers, Views, Areas)
│   ├── wwwroot              # Static files (CSS, JS, Images)
│   ├── Areas
│   │   ├── Admin            # Admin and Employee views and controllers
│   │   ├── Customer         # Customer views and controllers
│   │   └── Identity         # Identity management views and configuration
│   ├── Views                # Shared views for the application
│   ├── appsettings.json     # Application settings (connection strings, Stripe keys)
│   └── Program.cs           # Application entry point
├── CDShop.IntegrationTests  # Integration tests
└── CDShop.Tests             # Unit tests
```

## Technologies Used

- **ASP.NET Core 7.0**: Main framework for the web application.
- **Entity Framework Core**: ORM for database operations.
- **SQL Server**: Database engine for data storage.
- **Stripe**: Payment platform for secure transactions.
- **Identity**: Built-in ASP.NET Core Identity for user authentication and role management.
- **Moq**: Used for mocking services in unit tests.
- **xUnit**: Unit testing framework.
- **MSTest**: Test framework used for integration testing.
- **Bootswatch**: Frontend framework for responsive UI design.
- **jQuery & AJAX**: Used for client-side interactions.

## Setup and Configuration

To set up the application locally, follow these steps:

### 1. Clone the Repository

```bash
git clone https://github.com/your-repo/CDShop.git
cd CDShop
```

### 2. Configure Database Connection

Open the `CDShopWeb/appsettings.json` file and replace the `"DefaultConnection"` string with your database connection string.

### 3. Database Migration

Run the following commands in the **Package Manager Console** (under `Tools -> NuGet Package Manager -> Package Manager Console`) to create and apply the database migrations:

```bash
add-migration Initial
update-database
```

### 4. Run the Application

You can now run the application by executing the following command:

```bash
dotnet run --project CDShopWeb
```

Alternatively, open the solution in Visual Studio and run the `CDShopWeb` project.

### 5. Stripe Configuration

To test Stripe payments, use the following test card:

- **Card Number**: 4242 4242 4242 4242
- **Expiry Date**: Any future date
- **CVC**: Any 3-digit number

## Application Flow

### Admin and Employee Roles

- **Content Management**: Admin and Employees can manage product types, genres, packages, and companies via the "Content Management" tab.
- **Product Management**: After adding a product, it becomes available for purchase on the main site.
- **Order Management**: Admins and Employees can manage orders by approving, canceling, shipping, or editing them.

### Company and Individual Customers

- **Placing Orders**: Both company and individual customers can place orders. 
- **Order Status**: Customers can view the status of their orders in the "Manage Order" section.
- **Deferred Payment for Companies**: Company customers can place orders and pay 30 days after shipping, within the "Manage Order" section.

### Payment Handling via Stripe

- The Stripe payment platform is used to process payments. If there is an issue during payment, the order status will be marked as `Pending`.
  
**Test Credentials**:
- **Card**: 4242 4242 4242 4242
- **Expiry Date**: Any future date
- **CVC**: Any 3-digit number
- **Additional Information**: Any data can be used for other fields.

### Admin and Employee Testing Accounts

For testing purposes, the creation of Admin and Employee accounts is implemented. These accounts have access to all the content and order management features.

## Testing

### Unit and Integration Tests

The project includes unit and integration tests using **xUnit**, **MSTest**, and **Moq**. The tests are located in the `CDShop.Tests` directory and cover the following areas:

- **Controllers**: Tests for `Admin`, `Customer`, and `Identity` controllers.
- **Repositories**: Mocking database operations using **Moq**.
- **Services**: Verifying the behavior of utilities like the `EmailSender` and Stripe integration.

To run the tests:

```bash
dotnet test
```

### Example Tests:
- [**ProductControllerTests**](CDShop.Tests/Controllers/ProductControllerTests.cs): Ensures correct behavior for product creation, updating, deletion, and retrieval.
- [**ProductRepositoryTests**](CDShop.Tests/Repositories/ProductRepositoryTests.cs): Tests for handling product addition to the database.
