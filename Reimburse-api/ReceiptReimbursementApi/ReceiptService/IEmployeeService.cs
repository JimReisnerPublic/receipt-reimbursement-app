using System.Collections.Generic;
using System.Threading.Tasks;
using ReceiptReimbursement.Models;

namespace ReceiptReimbursement.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> CreateEmployeeAsync(Employee employee);
        Task<bool> UpdateEmployeeAsync(Employee employee);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<IEnumerable<Receipt>> GetEmployeeReceiptsAsync(int employeeId);
        Task<Employee> GetEmployeeByEmailAsync(string email);
    }
}
