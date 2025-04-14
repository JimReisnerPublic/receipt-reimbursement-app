using Microsoft.EntityFrameworkCore;
using ReceiptReimbursement.Models;

namespace ReceiptReimbursement.Data
{


    public interface IReceiptRepository
    {
        Task<IEnumerable<Receipt>> GetAllAsync();
        Task<Receipt> GetByIdAsync(int id);
        Task<Receipt> AddAsync(Receipt receipt);
        Task<bool> UpdateAsync(Receipt receipt);
        Task<bool> DeleteAsync(int id);
    }

    public class ReceiptRepository : IReceiptRepository
    {
        private readonly ApplicationDbContext _context;

        public ReceiptRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Receipt>> GetAllAsync()
        {
            return await _context.Receipts.ToListAsync();
        }

        public async Task<Receipt> GetByIdAsync(int id)
        {
            return await _context.Receipts.FindAsync(id);
        }

        public async Task<Receipt> AddAsync(Receipt receipt)
        {
            _context.Receipts.Add(receipt);
            await _context.SaveChangesAsync();
            return receipt;
        }

        public async Task<bool> UpdateAsync(Receipt receipt)
        {
            _context.Entry(receipt).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var receipt = await _context.Receipts.FindAsync(id);
            if (receipt == null)
                return false;

            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
