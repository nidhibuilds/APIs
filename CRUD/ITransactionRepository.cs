using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction> GetByIdAsync(int id);
        Task<Transaction> AddAsync(Transaction transaction);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(int id);
    }
} 