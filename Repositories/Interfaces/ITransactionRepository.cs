using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;

namespace Florin_Back.Repositories.Interfaces;

public interface ITransactionRepository
{
    public Task<Pagination<Transaction>> GetTransactionsByUserIdAsync(long userId, PaginationFilters pagination, TransactionFilters filters);
    public Task<Transaction> CreateTransactionAsync(Transaction transaction);
    public Task<Transaction?> GetTransactionByUserIdAsync(long id, long userId);
    public Task<Transaction> UpdateTransactionAsync(Transaction transaction);
    public Task DeleteTransactionAsync(Transaction transaction);
}
