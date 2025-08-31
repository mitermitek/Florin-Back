using Florin_Back.Models;

namespace Florin_Back.Repositories.Interfaces;

public interface ITransactionRepository
{
    public Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(long userId);
    public Task<Pagination<Transaction>> GetTransactionsByUserIdAsync(long userId, int page, int size);
    public Task<Transaction> CreateTransactionAsync(Transaction transaction);
    public Task<Transaction?> GetTransactionByIdAndUserIdAsync(long id, long userId);
    public Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    public Task DeleteTransactionAsync(Transaction transaction);
}
