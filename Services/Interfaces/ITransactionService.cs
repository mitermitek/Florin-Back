using Florin_Back.Models;

namespace Florin_Back.Services.Interfaces;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetUserTransactionsAsync(long userId);
    public Task<Transaction?> GetUserTransactionByIdAsync(long userId, long transactionId);
    public Task<Transaction> CreateUserTransactionAsync(long userId, Transaction transaction);
    public Task<Transaction> UpdateUserTransactionAsync(long userId, long transactionId, Transaction transaction);
    public Task DeleteUserTransactionAsync(long userId, long transactionId);
}
