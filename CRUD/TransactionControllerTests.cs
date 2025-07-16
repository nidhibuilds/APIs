using CRUD;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

public class TransactionControllerTests : IClassFixture<WebApplicationFactory<CRUD.Startup>>
{
    private readonly HttpClient _client;

    public TransactionControllerTests(WebApplicationFactory<CRUD.Startup> factory)
    {
        _client = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<FinanceDbContext>));
                if (descriptor != null) services.Remove(descriptor);
                services.AddDbContext<FinanceDbContext>(options =>
                    options.UseInMemoryDatabase("TestDb"));
            });
        }).CreateClient();
    }

    [Fact]
    public async Task Post_And_GetById_Works()
    {
        var transaction = new Transaction { Amount = 123, Date = DateTime.Now, Type = "Credit", Description = "IntegrationTest", AccountId = 10 };
        var postResponse = await _client.PostAsJsonAsync("/api/Transaction", transaction);
        postResponse.EnsureSuccessStatusCode();
        var created = await postResponse.Content.ReadFromJsonAsync<Transaction>();
        Assert.NotNull(created);
        var getResponse = await _client.GetAsync($"/api/Transaction/{created.Id}");
        getResponse.EnsureSuccessStatusCode();
        var fetched = await getResponse.Content.ReadFromJsonAsync<Transaction>();
        Assert.Equal("IntegrationTest", fetched.Description);
    }

    [Fact]
    public async Task GetAll_ReturnsList()
    {
        var response = await _client.GetAsync("/api/Transaction");
        response.EnsureSuccessStatusCode();
        var list = await response.Content.ReadFromJsonAsync<Transaction[]>();
        Assert.NotNull(list);
    }

    [Fact]
    public async Task Put_UpdatesTransaction()
    {
        var transaction = new Transaction { Amount = 200, Date = DateTime.Now, Type = "Debit", Description = "ToUpdate", AccountId = 11 };
        var post = await _client.PostAsJsonAsync("/api/Transaction", transaction);
        var created = await post.Content.ReadFromJsonAsync<Transaction>();
        created.Description = "UpdatedDesc";
        var put = await _client.PutAsJsonAsync($"/api/Transaction/{created.Id}", created);
        put.EnsureSuccessStatusCode();
        var updated = await put.Content.ReadFromJsonAsync<Transaction>();
        Assert.Equal("UpdatedDesc", updated.Description);
    }

    [Fact]
    public async Task Delete_RemovesTransaction()
    {
        var transaction = new Transaction { Amount = 300, Date = DateTime.Now, Type = "Debit", Description = "ToDelete", AccountId = 12 };
        var post = await _client.PostAsJsonAsync("/api/Transaction", transaction);
        var created = await post.Content.ReadFromJsonAsync<Transaction>();
        var delete = await _client.DeleteAsync($"/api/Transaction/{created.Id}");
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
        var get = await _client.GetAsync($"/api/Transaction/{created.Id}");
        Assert.Equal(HttpStatusCode.NotFound, get.StatusCode);
    }
} 