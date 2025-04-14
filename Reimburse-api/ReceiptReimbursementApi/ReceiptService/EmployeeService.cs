using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ReceiptReimbursement.Data;
using ReceiptReimbursement.Models;

namespace ReceiptReimbursement.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            ValidateEmployee(employee);
            return await _employeeRepository.AddAsync(employee);
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            ValidateEmployee(employee);
            return await _employeeRepository.UpdateAsync(employee);
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            try
            {
                return await _employeeRepository.DeleteAsync(id);
            }
            catch (InvalidOperationException ex)
            {
                throw new ValidationException(ex.Message);
            }
        }

        public async Task<IEnumerable<Receipt>> GetEmployeeReceiptsAsync(int employeeId)
        {
            var employeeExists = await _employeeRepository.EmployeeExistsAsync(employeeId);
            if (!employeeExists)
                throw new ValidationException($"Employee with ID {employeeId} does not exist");

            return await _employeeRepository.GetEmployeeReceiptsAsync(employeeId);
        }

        private void ValidateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.Name))
                throw new ValidationException("Employee name is required");

            if (string.IsNullOrWhiteSpace(employee.Email))
                throw new ValidationException("Employee email is required");

            if (!IsValidEmail(employee.Email))
                throw new ValidationException("Invalid email format");

            if (string.IsNullOrWhiteSpace(employee.Department))
                throw new ValidationException("Department is required");
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ValidationException("Employee email is required");
            }

            return await _employeeRepository.GetByEmailAsync(email);
        }
    }
}
