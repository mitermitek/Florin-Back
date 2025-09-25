using Florin_Back.Data;
using Florin_Back.Enums;
using Florin_Back.Models.Entities;
using Florin_Back.Models.Utilities;
using Florin_Back.Models.Utilities.Filters;
using Florin_Back.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Repositories;

public class TransactionRepository(FlorinDbContext ctx) : ITransactionRepository
{
    public async Task<Pagination<Transaction>> GetTransactionsByUserIdAsync(long userId, PaginationFilters pagination, TransactionFilters filters)
    {
        var page = pagination.Page;
        var size = pagination.Size;

        var query = ctx.Transactions.Include(t => t.Category).Where(t => t.UserId == userId).AsQueryable();

        if (!string.IsNullOrEmpty(filters.Search))
        {
            query = query.Where(t =>
                (t.Description != null && t.Description.Contains(filters.Search)) ||
                t.Amount.ToString().Contains(filters.Search) ||
                t.Type.ToString().Contains(filters.Search) ||
                t.Category!.Name.Contains(filters.Search) ||
                t.Date.ToString().Contains(filters.Search)
            );
        }

        if (!string.IsNullOrEmpty(filters.Type) && Enum.TryParse<TransactionType>(filters.Type, out var typeEnum))
        {
            query = query.Where(t => t.Type == typeEnum);
        }

        if (filters.CategoryId.HasValue)
        {
            query = query.Where(t => t.CategoryId == filters.CategoryId.Value);
        }

        if (filters.StartDate.HasValue)
        {
            query = query.Where(t => t.Date >= filters.StartDate.Value);
        }

        if (filters.EndDate.HasValue)
        {
            query = query.Where(t => t.Date <= filters.EndDate.Value);
        }

        if (!string.IsNullOrEmpty(filters.MinAmount.ToString()))
        {
            query = query.Where(t => t.Amount >= filters.MinAmount);
        }

        if (!string.IsNullOrEmpty(filters.MaxAmount.ToString()))
        {
            query = query.Where(t => t.Amount <= filters.MaxAmount);
        }

        query = query.OrderByDescending(t => t.Date).AsNoTracking();

        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * size).Take(size).ToListAsync();

        return new Pagination<Transaction>
        {
            Items = items,
            Total = total,
            Page = page,
            Size = size
        };
    }

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        ctx.Transactions.Add(transaction);
        await ctx.SaveChangesAsync();
        return transaction;
    }

    public async Task<Transaction?> GetTransactionByUserIdAsync(long id, long userId)
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
