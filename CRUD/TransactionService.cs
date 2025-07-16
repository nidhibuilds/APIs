using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
        {
            return await _repository.AddAsync(transaction);
        }

        public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
        {
            return await _repository.UpdateAsync(transaction);
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
} 