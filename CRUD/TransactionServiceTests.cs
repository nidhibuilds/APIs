using CRUD;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class TransactionServiceTests
{
    private readonly Mock<ITransactionRepository> _repoMock;
    private readonly TransactionService _service;

    public TransactionServiceTests()
    {
        _repoMock = new Mock<ITransactionRepository>();
        _service = new TransactionService(_repoMock.Object);
    }

    [Fact]
    public async Task GetAllTransactionsAsync_ReturnsAll()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Transaction> { new Transaction(), new Transaction() });
        var result = await _service.GetAllTransactionsAsync();
        Assert.Equal(2, System.Linq.Enumerable.Count(result));
    }

    [Fact]
    public async Task GetTransactionByIdAsync_ReturnsTransaction()
    {
        var transaction = new Transaction { Id = 1 };
        _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(transaction);
        var result = await _service.GetTransactionByIdAsync(1);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreateTransactionAsync_CallsAddAsync()
    {
        var transaction = new Transaction { Id = 2 };
        _repoMock.Setup(r => r.AddAsync(transaction)).ReturnsAsync(transaction);
        var result = await _service.CreateTransactionAsync(transaction);
        Assert.Equal(2, result.Id);
    }

    [Fact]
    public async Task UpdateTransactionAsync_CallsUpdateAsync()
    {
        var transaction = new Transaction { Id = 3 };
        _repoMock.Setup(r => r.UpdateAsync(transaction)).ReturnsAsync(transaction);
        var result = await _service.UpdateTransactionAsync(transaction);
        Assert.Equal(3, result.Id);
    }

    [Fact]
    public async Task DeleteTransactionAsync_CallsDeleteAsync()
    {
        _repoMock.Setup(r => r.DeleteAsync(4)).ReturnsAsync(true);
        var result = await _service.DeleteTransactionAsync(4);
        Assert.True(result);
    }
} 