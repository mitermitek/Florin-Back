using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;

namespace Florin_Back.Services.Interfaces;

public interface ITransactionService
{
    public Task<Pagination<Transaction>> GetUserTransactionsAsync(PaginationFilters pagination, TransactionFilters filters);
    public Task<Transaction?> GetUserTransactionAsync(long transactionId);
    public Task<Transaction> CreateUserTransactionAsync(Transaction transaction);
    public Task<Transaction> UpdateUserTransactionAsync(long transactionId, Transaction transaction);
    public Task DeleteUserTransactionAsync(long transactionId);
}
