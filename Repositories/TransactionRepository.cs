using Florin_Back.Data;
using Florin_Back.Models;
using Florin_Back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Repositories;

public class TransactionRepository(FlorinDbContext ctx) : ITransactionRepository
{
    public async Task<IEnumerable<Transaction>> GetTransactionsByUserIdAsync(long userId)
    {
        return await ctx.Transactions.Where(c => c.UserId == userId).OrderByDescending(c => c.Date).ToListAsync();
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        ctx.Transactions.Add(transaction);
        await ctx.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> GetTransactionByIdAndUserIdAsync(long id, long userId)
    {
        return await ctx.Transactions.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<Transaction> UpdateTransactionAsync(Transaction transaction)
    {
        ctx.Transactions.Update(transaction);
        await ctx.SaveChangesAsync();
        return transaction;
    }

    public async Task DeleteTransactionAsync(Transaction transaction)
    {
        ctx.Transactions.Remove(transaction);
        await ctx.SaveChangesAsync();
    }
}
