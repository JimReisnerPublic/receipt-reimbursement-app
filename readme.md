# Receipt Reimbursement App
This is a minimal web application that allows university employees to submit receipts for reimbursement. The app includes:

A frontend built with Angular

A backend API built with .NET

A MySQL database for storing receipt and employee data

## Description

Receipt file upload

Backend API to handle receipt submissions and persist data in MySQL.

Basic validation for required fields and file size limits (max 5MB).

## Prerequisites
Ensure you have the following installed on your machine:

Node.js (v18 or later) and npm

Angular CLI (v15 or later)

.NET SDK (v8 or later)

MySQL Server (v8.0 or later)

Visual Studio 2022 or later

VS Code 1.99.2 or later

Setup Instructions
1. Clone the Repository
bash
git clone <repository-url>
cd receipt-reimbursement-app
2. Backend Setup
Navigate to the API project directory:

bash
`cd Reimburse-api/ReceiptReimbursementApi`
Install dependencies:
Use visual Studio to restore packages, or run the following command in the solution directory:

`dotnet restore ReceiptReimbursementApi.sln`

## Create the MySQL Database:
You can choose to use Entity Framework Migrations or use generated SQL scripts to create the database and tables.
In either case, you will manual create a databse user for programatic connection to the database.
The database user created will need to match the user defined in the ReceiptReimbursementApi
appsettngs.json file under ConnectionStrings:DefaultConnection

### Entity Framwork Migration database and table creation:

Open a terminal in the project directory and run:
`dotnet ef database update --project ReceiptReimbursement.Data --startup-project ReceiptReimbursement.Data`

Note: if you prefer to generate scripts create the database, run the following:
`dotnet ef migrations script --project ReceiptReimbursement.Data --startup-project ReceiptReimbursement.Data`

### Create Application User
-- Create a dedicated user for the application with restricted permissions

`CREATE USER 'receiptuser'@'localhost' IDENTIFIED BY 'StrongPassword123!';`

-- Grant the necessary permissions to the user

`GRANT SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, INDEX, DROP, REFERENCES ON ReceiptReimbursementDb.* TO 'receiptuser'@'localhost';`

-- Apply the changes
`FLUSH PRIVILEGES;`
### Create employee record
In MySQL Workbench, Add an employee to the database, whose email you will use the application
(change Name, Email and Department fields as desired)

`INSERT INTO employees (Name, Email, Department) VALUES ('Jim Reisner', 'jim.reisner@gmail.com', 'Finance');`

Start the Backend API:
Debug in Visual Studo, or from terminal:
`dotnet run`

3. Frontend Setup
Open the Reimburse-frontend folder in VS Code, navigate to receipt-reimbursement-app\src\app\services\receiptservice.ts, ensuring that 'apiUrl' matches the url defined in the backend's ReceiptReimbursementApi project's launchsettings.json file.
Open a terminal in VS Code
Navigate to the Angular project directory:

`cd ../../receipt-reimbursement-app`
Install dependencies:

`npm install`
Run the Angular Development Server:

`ng serve --open`
This will open your browser at http://localhost:4200.

Usage

Fill out the receipt form with:

Employee email (e.g., johndoe@example.com, matching the mySQL employee record created earlier)

Date (defaults to today)

Amount, description, and category of purchase.

Upload a receipt file (PDF/JPG/PNG).

Submit the form to save the receipt.

Estimated vs Actual Time Spent
Estimated Time: 5 hours

Actual Time: 10 hours

Additional time was spent debugging JSON serialization issues with NativeAOT and ensuring proper error handling for file uploads.

Tech Stack Choices
Angular & .NET Core:

Preferred stack as per requirements.

Strong integration between Angular frontend and .NET backend.

MySQL:

Chosen for its ease of use and familiarity.

Assumptions
Employees are pre-registered in the database. 

Receipts are submitted by providing an employee email.

File uploads are limited to 5MB.

Highlights
Validation for required fields and file size limits.

Clear error messages for invalid inputs.

Clean separation of concerns between frontend, backend, and database.
