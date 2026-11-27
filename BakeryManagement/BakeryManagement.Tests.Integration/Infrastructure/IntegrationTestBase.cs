namespace BakeryManagement.Tests.Integration.Infrastructure;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    private CustomWebApplicationFactory _factory = null!;

    protected HttpClient Client { get; private set; } = null!;

    public Task InitializeAsync()
    {
        _factory = new CustomWebApplicationFactory();
        Client = _factory.CreateClient();

        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        Client.Dispose();
        await _factory.DisposeAsync();
    }
}
