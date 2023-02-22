# E-Procurement
The mini Rest-Api app that can do payment of products from Customer(Store) to Vendor.

## How To Try this app ?
- Setting your localhost database in appsettings.Development.json & appsettings.json into your localhost Sql Server database.
- You can directly to Update database from the migrations file (migration has been done in this project). your database will be made automatically.
- Content of Category name have been made. 
- Recommended using Swagger to test.

## Features
### Register
- #### Register for Customer (Store) 
[post] https://localhost:9000/api/auth/register-customer
```json
{
  "username": "string",
  "address": "string",
  "phoneNumber": "string",
  "email": "string",
  "password": "string"
}
```

- #### Register for Vendor
[post] https://localhost:9000/api/auth/register-vendor
```json
{
  "username": "string",
  "address": "string",
  "phoneNumber": "string",
  "email": "string",
  "password": "string"
}
```

### Login
[post] https://localhost:9000/api/auth/login
```json
{
    "email": "indra@email.com",
    "password": "password123"
}
```

### Public Features
- #### See all categories
[get] https://localhost:9000/api/categories
- #### See all vendor profiles
[get] https://localhost:9000/api/users/vendor
- #### See all vendors with products for sale
[get] https://localhost:9000/api/users/products
- #### See all products for sale
[get] https://localhost:9000/api/products/{name(opsional)}

### Vendor features
- #### Create Product
[post] https://localhost:9000/api/products
```json
{
  "name": "Bolpoin",
  "categoryId": "19ce01d3-23a0-46cb-85c4-cef8206ddf11",
  "productCode": "PSS002",
  "stock": 10,
  "price": 2000
}
```
- #### Update Product
[put] https://localhost:9000/api/products/product-update
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "price": 0,
  "stock": 0,
  "productCode": "string"
}
```

- #### Update Product Name / Category only
[put] https://localhost:9000/api/products/
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "productName": "string",
  "productCategoryId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```

- #### Delete Product
[delete] https://localhost:9000/api/products/api/products/{id}

### Customer (Store) Features
- #### Transaction features
[post] https://localhost:9000/api/transactions/
```json
[
  {
    "qty": 0,
    "productPriceId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  }
]
```

- #### Today transaction reports
[get] https://localhost:9000/api/transactions/daily-report

- #### Monthly transaction reports
[get] https://localhost:9000/api/transactions/monthly-report/{numberOfMonth}



