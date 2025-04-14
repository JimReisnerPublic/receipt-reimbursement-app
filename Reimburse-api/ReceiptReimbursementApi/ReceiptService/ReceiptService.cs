using System.ComponentModel.DataAnnotations;
using ReceiptReimbursement.Data;
using ReceiptReimbursement.Models;

namespace ReceiptReimbursement.Services
{

    public class ReceiptService : IReceiptService
    {
        private readonly IReceiptRepository _repository;

        public ReceiptService(IReceiptRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Receipt>> GetReceiptsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Receipt> GetReceiptByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Receipt> CreateReceiptAsync(Receipt receipt)
        {
            ValidateReceipt(receipt);

            // Set default values
            receipt.Status = "Pending";
            receipt.SubmissionDate = DateTime.Now;

            return await _repository.AddAsync(receipt);
        }

        public async Task<bool> UpdateReceiptAsync(Receipt receipt)
        {
            ValidateReceipt(receipt);
            return await _repository.UpdateAsync(receipt);
        }

        public async Task<bool> DeleteReceiptAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<bool> UpdateReceiptStatusAsync(int id, string status)
        {
            // Get the receipt
            var receipt = await _repository.GetByIdAsync(id);
            if (receipt == null)
                return false;

            // Validate status
            if (!new[] { "Pending", "Approved", "Rejected" }.Contains(status))
                throw new ValidationException("Invalid status value");

            receipt.Status = status;
            return await _repository.UpdateAsync(receipt);
        }

        private void ValidateReceipt(Receipt receipt)
        {
            if (receipt.EmployeeId <= 0)
                throw new ValidationException("Employee ID is required");

            if (receipt.Amount <= 0)
                throw new ValidationException("Amount must be greater than zero");

            if (string.IsNullOrWhiteSpace(receipt.Description))
                throw new ValidationException("Description is required");

            if (string.IsNullOrWhiteSpace(receipt.Category))
                throw new ValidationException("Category is required");
        }
    }
}
