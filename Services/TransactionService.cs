using Florin_Back.Exceptions.Transaction;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services.Interfaces;

namespace Florin_Back.Services;

public class TransactionService(ITransactionRepository transactionRepository, ICategoryService categoryService) : ITransactionService
{
    public Task<IEnumerable<Transaction>> GetUserTransactionsAsync(long userId)
    {
        return transactionRepository.GetTransactionsByUserIdAsync(userId);
    }

    public async Task<Transaction?> GetUserTransactionByIdAsync(long userId, long transactionId)
    {
        var transaction = await transactionRepository.GetTransactionByIdAndUserIdAsync(transactionId, userId) ?? throw new TransactionNotFoundException();
        return transaction;
    }

    public async Task<Transaction> CreateUserTransactionAsync(long userId, Transaction transaction)
    {
        // check if transaction type exists
        CheckTransactionTypeExists(transaction);

        // get category to ensure it belongs to the user
        var existingCategory = await categoryService.GetUserCategoryByIdAsync(userId, transaction.CategoryId);

        transaction.UserId = userId;
        transaction.Category = existingCategory;
        return await transactionRepository.CreateTransactionAsync(transaction);
    }

    public async Task<Transaction> UpdateUserTransactionAsync(long userId, long transactionId, Transaction transaction)
    {
        // check if transaction type exists
        CheckTransactionTypeExists(transaction);

        // get category to ensure it belongs to the user
        var existingCategory = await categoryService.GetUserCategoryByIdAsync(userId, transaction.CategoryId);

        // check if transaction exists
        var existingTransaction = await transactionRepository.GetTransactionByIdAndUserIdAsync(transactionId, userId) ?? throw new TransactionNotFoundException();

        existingTransaction.CategoryId = transaction.CategoryId;
        existingTransaction.Category = existingCategory;
        existingTransaction.Type = transaction.Type;
        existingTransaction.Date = transaction.Date;
        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Description = transaction.Description;
        return await transactionRepository.UpdateTransactionAsync(existingTransaction);
    }

    public async Task DeleteUserTransactionAsync(long userId, long transactionId)
    {
        var transaction = await transactionRepository.GetTransactionByIdAndUserIdAsync(transactionId, userId) ?? throw new TransactionNotFoundException();

        await transactionRepository.DeleteTransactionAsync(transaction);
    }

    private static void CheckTransactionTypeExists(Transaction transaction)
    {
        if (!Enum.IsDefined(transaction.Type))
        {
            throw new TransactionTypeNotFoundException();
        }
    }
}
