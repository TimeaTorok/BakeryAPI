using BakeryManagement.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace BakeryManagement.Tests.Integration.Infrastructure
{
    public sealed class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<DbContextOptions<BakeryDbContext>>();
                services.RemoveAll<IDbContextOptionsConfiguration<BakeryDbContext>>();
                services.RemoveAll<BakeryDbContext>();

                services.AddSingleton<SqliteConnection>(_ =>
                {
                    var connection = new SqliteConnection("Data Source=:memory:");
                    connection.Open();
                    return connection;
                });

                services.AddDbContext<BakeryDbContext>((serviceProvider, options) =>
                    options.UseSqlite(
                        serviceProvider.GetRequiredService<SqliteConnection>()));
            });
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            var host = base.CreateHost(builder);

            using var scope = host.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<BakeryDbContext>();
            dbContext.Database.Migrate();

            return host;
        }
    }

}
