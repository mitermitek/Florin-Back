using Florin_Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Florin_Back.Data;

public class FlorinDbContext(DbContextOptions<FlorinDbContext> opt) : DbContext(opt)
{
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is User || e.Entity is RefreshToken && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                ((dynamic)entry.Entity).CreatedAt = now;
            }
            ((dynamic)entry.Entity).UpdatedAt = now;
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is User || e.Entity is RefreshToken && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                ((dynamic)entry.Entity).CreatedAt = now;
            }
            ((dynamic)entry.Entity).UpdatedAt = now;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
