using Florin_Back.Exceptions.Transaction;
using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;
using Florin_Back.Repositories.Interfaces;
using Florin_Back.Services.Interfaces;

namespace Florin_Back.Services;

public class TransactionService(IUserContextService userContextService, ITransactionRepository transactionRepository, ICategoryService categoryService) : ITransactionService
{
    public async Task<Pagination<Transaction>> GetUserTransactionsAsync(PaginationFilters pagination, TransactionFilters filters)
    {
        var userId = userContextService.GetUserId();

        return await transactionRepository.GetTransactionsByUserIdAsync(userId, pagination, filters);
    }

    public async Task<Transaction?> GetUserTransactionAsync(long transactionId)
    {
        var userId = userContextService.GetUserId();
        var transaction = await transactionRepository.GetTransactionByUserIdAsync(transactionId, userId) ?? throw new TransactionNotFoundException();

        return transaction;
    }

    public async Task<Transaction> CreateUserTransactionAsync(Transaction transaction)
    {
        var userId = userContextService.GetUserId();
        var existingCategory = await categoryService.GetUserCategoryAsync(transaction.CategoryId);

        transaction.UserId = userId;
        transaction.Category = existingCategory;

        return await transactionRepository.CreateTransactionAsync(transaction);
    }

    public async Task<Transaction> UpdateUserTransactionAsync(long transactionId, Transaction transaction)
    {
        var userId = userContextService.GetUserId();
        var existingCategory = await categoryService.GetUserCategoryAsync(transaction.CategoryId);
        var existingTransaction = await transactionRepository.GetTransactionByUserIdAsync(transactionId, userId) ?? throw new TransactionNotFoundException();

        existingTransaction.CategoryId = transaction.CategoryId;
        existingTransaction.Category = existingCategory;
        existingTransaction.Type = transaction.Type;
        existingTransaction.Date = transaction.Date;
        existingTransaction.Amount = transaction.Amount;
        existingTransaction.Description = transaction.Description;

        return await transactionRepository.UpdateTransactionAsync(existingTransaction);
    }

    public async Task DeleteUserTransactionAsync(long transactionId)
    {
        var userId = userContextService.GetUserId();
        var transaction = await transactionRepository.GetTransactionByUserIdAsync(transactionId, userId) ?? throw new TransactionNotFoundException();

        await transactionRepository.DeleteTransactionAsync(transaction);
    }
}
