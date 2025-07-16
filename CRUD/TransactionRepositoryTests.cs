using CRUD;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

public class TransactionRepositoryTests
{
    private FinanceDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<FinanceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new FinanceDbContext(options);
    }

    [Fact]
    public async Task AddAsync_AddsTransaction()
    {
        var context = GetDbContext();
        var repo = new TransactionRepository(context);
        var transaction = new Transaction { Amount = 100, Date = DateTime.Now, Type = "Credit", Description = "Test", AccountId = 1 };
        var result = await repo.AddAsync(transaction);
        Assert.NotNull(result);
        Assert.Equal(100, result.Amount);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAll()
    {
        var context = GetDbContext();
        context.Transactions.Add(new Transaction { Amount = 50, Date = DateTime.Now, Type = "Debit", Description = "Test1", AccountId = 2 });
        context.Transactions.Add(new Transaction { Amount = 75, Date = DateTime.Now, Type = "Credit", Description = "Test2", AccountId = 3 });
        context.SaveChanges();
        var repo = new TransactionRepository(context);
        var all = await repo.GetAllAsync();
        Assert.Equal(2, System.Linq.Enumerable.Count(all));
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectTransaction()
    {
        var context = GetDbContext();
        var transaction = new Transaction { Amount = 200, Date = DateTime.Now, Type = "Credit", Description = "FindMe", AccountId = 4 };
        context.Transactions.Add(transaction);
        context.SaveChanges();
        var repo = new TransactionRepository(context);
        var found = await repo.GetByIdAsync(transaction.Id);
        Assert.NotNull(found);
        Assert.Equal("FindMe", found.Description);
    }

    [Fact]
    public async Task UpdateAsync_UpdatesTransaction()
    {
        var context = GetDbContext();
        var transaction = new Transaction { Amount = 300, Date = DateTime.Now, Type = "Debit", Description = "Old", AccountId = 5 };
        context.Transactions.Add(transaction);
        context.SaveChanges();
        var repo = new TransactionRepository(context);
        transaction.Description = "Updated";
        var updated = await repo.UpdateAsync(transaction);
        Assert.Equal("Updated", updated.Description);
    }

    [Fact]
    public async Task DeleteAsync_DeletesTransaction()
    {
        var context = GetDbContext();
        var transaction = new Transaction { Amount = 400, Date = DateTime.Now, Type = "Debit", Description = "ToDelete", AccountId = 6 };
        context.Transactions.Add(transaction);
        context.SaveChanges();
        var repo = new TransactionRepository(context);
        var deleted = await repo.DeleteAsync(transaction.Id);
        Assert.True(deleted);
        Assert.Null(await repo.GetByIdAsync(transaction.Id));
    }
} 