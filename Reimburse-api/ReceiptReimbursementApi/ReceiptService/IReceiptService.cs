using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReceiptReimbursement.Models;

namespace ReceiptReimbursement.Services
{
    public interface IReceiptService
    {
        Task<IEnumerable<Receipt>> GetReceiptsAsync();
        Task<Receipt?> GetReceiptByIdAsync(int id);
        Task<Receipt> CreateReceiptAsync(Receipt receipt);
        Task<bool> UpdateReceiptAsync(Receipt receipt);
        Task<bool> DeleteReceiptAsync(int id);
        Task<bool> UpdateReceiptStatusAsync(int id, string status);
    }

}
