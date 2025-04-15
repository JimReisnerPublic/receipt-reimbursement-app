# Receipt Reimbursement App
A minimal web application that allows university employees to submit receipts for reimbursement.

The app includes:

A frontend built with Angular

A backend API built with .NET

A MySQL database for storing receipt and employee data

✨ Features
Submit a receipt with:

Date

Amount

Description

Category

File upload (PDF/JPG/PNG, max 5MB)

Backend API to handle submissions and persist data in MySQL

Basic validation for required fields and file size

🛠️ Prerequisites
Ensure you have the following installed:

Node.js (v18 or later) and npm

Angular CLI (v15 or later)

bash

`npm install -g @angular/cli`

.NET SDK (v8 or later)

MySQL Server (v8.0 or later)

🚀 Setup Instructions
1. Clone the Repository
bash

`git clone <repository-url>`
cd receipt-reimbursement-app
2. Backend Setup
Navigate to the API project directory:

bash
`cd Reimburse-api/ReceiptReimbursementApi`
Install dependencies:

bash

`dotnet restore`
Create the MySQL database:

sql
Copy
Edit
CREATE DATABASE ReceiptReimbursementDb;

USE ReceiptReimbursementDb;

CREATE TABLE Employees (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Department VARCHAR(100) NOT NULL
);

CREATE TABLE Receipts (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeId INT NOT NULL,
    Date DATE NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    Description VARCHAR(500) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Status VARCHAR(50) DEFAULT 'Pending',
    ImageLocation VARCHAR(2000),
    SubmissionDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (EmployeeId) REFERENCES Employees(Id)
);

INSERT INTO Employees (Name, Email, Department)
VALUES ('John Doe', 'johndoe@example.com', 'Finance');
Update the connection string in appsettings.json:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=ReceiptReimbursementDb;User=root;Password=yourpassword;"
}
Run Entity Framework migrations (if using EF Core):

bash
Copy
Edit
dotnet ef database update
Start the backend API:

bash
Copy
Edit
dotnet run
3. Frontend Setup
Navigate to the Angular project directory:

bash
Copy
Edit
cd ../../receipt-reimbursement-app
Install dependencies:

bash
Copy
Edit
npm install
Run the Angular development server:

bash
Copy
Edit
ng serve --open
The app should open in your browser at http://localhost:4200

💡 Usage
Open the app in your browser.

Fill out the receipt form with:

Employee email (e.g., johndoe@example.com)

Date (defaults to today)

Amount, description, and category

Upload a receipt file

Submit the form to save the receipt.

🕒 Estimated vs Actual Time Spent
Estimated Time: 6 hours

Actual Time: 7 hours

Additional time was spent debugging JSON serialization issues with NativeAOT and improving file upload error handling.

🧱 Tech Stack Choices
Angular & .NET Core: Preferred stack; strong integration and modern tooling.

MySQL: Chosen for its ease of use and familiarity.

✅ Assumptions
Employees are pre-registered in the database.

Receipts are submitted using the employee’s email.

File uploads are limited to 5MB.

🌟 Highlights
Validation for required fields and file size.

Clear error messages for invalid inputs.

Clean separation of frontend, backend, and database concerns.