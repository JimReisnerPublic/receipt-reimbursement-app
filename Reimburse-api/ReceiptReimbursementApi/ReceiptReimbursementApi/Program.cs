using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.OpenApi.Models;
using ReceiptReimbursement.Data;
using ReceiptReimbursement.Models;
using ReceiptReimbursement.Services;
using ReceiptReimbursementApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container

// Configure database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register repositories and services
builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

// Add CORS policy for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Add Swagger/OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Receipt Reimbursement API",
        Version = "v1",
        Description = "API for managing employee receipt reimbursements",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com"
        }
    });
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.TypeInfoResolver = AppJsonSerializerContext.Default;
});

var app = builder.Build();
//app.UsePathBase("/api");

// Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Receipt Reimbursement API v1"));
}

app.UseHttpsRedirection();
app.UseCors("AllowAngularApp");

// Define API endpoints using Minimal API style

// Receipts endpoints
app.MapGet("/api/receipts", async (IReceiptService service) =>
{
    var receipts = await service.GetReceiptsAsync();
    return Results.Ok(receipts);
})
.WithName("GetAllReceipts")
.WithOpenApi(operation => {
    operation.Summary = "Gets all receipts";
    return operation;
});

app.MapGet("/api/receipts/{id}", async (int id, IReceiptService service) =>
{
    var receipt = await service.GetReceiptByIdAsync(id);
    return receipt != null ? Results.Ok(receipt) : Results.NotFound();
})
.WithName("GetReceiptById")
.WithOpenApi(operation => {
    operation.Summary = "Gets a receipt by ID";
    return operation;
});

app.MapPost("/api/receipts", async (Receipt receipt, IReceiptService service) =>
{
    try
    {
        var newReceipt = await service.CreateReceiptAsync(receipt);
        return Results.Created($"/api/receipts/{newReceipt.Id}", newReceipt);
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(new { Message = "Receipt validation error: " + ex.Message });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Message = ex.Message });
    }
})
.WithName("CreateReceipt")
.WithOpenApi(operation => {
    operation.Summary = "Creates a new receipt";
    return operation;
});

app.MapPut("/api/receipts/{id}", async (int id, Receipt receipt, IReceiptService service) =>
{
    if (id != receipt.Id)
        return Results.BadRequest(new { Message = "ID mismatch" });

    try
    {
        var success = await service.UpdateReceiptAsync(receipt);
        return success ? Results.NoContent() : Results.NotFound();
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(new { Message = ex.Message });
    }
})
.WithName("UpdateReceipt")
.WithOpenApi(operation => {
    operation.Summary = "Updates an existing receipt";
    return operation;
});

app.MapDelete("/api/receipts/{id}", async (int id, IReceiptService service) =>
{
    var success = await service.DeleteReceiptAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteReceipt")
.WithOpenApi(operation => {
    operation.Summary = "Deletes a receipt";
    return operation;
});

app.MapPatch("/api/receipts/{id}/status", async (int id, string status, IReceiptService service) =>
{
    try
    {
        var success = await service.UpdateReceiptStatusAsync(id, status);
        return success ? Results.NoContent() : Results.NotFound();
    }
    catch (ValidationException ex)
    {
        return Results.BadRequest(new { Message = ex.Message });
    }
})
.WithName("UpdateReceiptStatus")
.WithOpenApi(operation => {
    operation.Summary = "Updates the status of a receipt";
    return operation;
});

// Employees endpoints
app.MapGet("/api/employees", async (IEmployeeService service) =>
{
    var employees = await service.GetEmployeesAsync();
    return Results.Ok(employees);
})
.WithName("GetAllEmployees")
.WithOpenApi(operation => {
    operation.Summary = "Gets all employees";
    return operation;
});

app.MapGet("/api/employees/{id}", async (int id, IEmployeeService service) =>
{
    var employee = await service.GetEmployeeByIdAsync(id);
    return employee != null ? Results.Ok(employee) : Results.NotFound();
})
.WithName("GetEmployeeById")
.WithOpenApi(operation => {
    operation.Summary = "Gets an employee by ID";
    return operation;
});

app.MapPost("/api/employees", async (Employee employee, IEmployeeService service) =>
{
    var newEmployee = await service.CreateEmployeeAsync(employee);
    return Results.Created($"/api/employees/{newEmployee.Id}", newEmployee);
})
.WithName("CreateEmployee")
.WithOpenApi(operation => {
    operation.Summary = "Creates a new employee";
    return operation;
});

app.MapPut("/api/employees/{id}", async (int id, Employee employee, IEmployeeService service) =>
{
    if (id != employee.Id)
        return Results.BadRequest(new { Message = "ID mismatch" });

    var success = await service.UpdateEmployeeAsync(employee);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("UpdateEmployee")
.WithOpenApi(operation => {
    operation.Summary = "Updates an existing employee";
    return operation;
});

app.MapDelete("/api/employees/{id}", async (int id, IEmployeeService service) =>
{
    var success = await service.DeleteEmployeeAsync(id);
    return success ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteEmployee")
.WithOpenApi(operation => {
    operation.Summary = "Deletes an employee";
    return operation;
});

// Run the application
app.Run();
