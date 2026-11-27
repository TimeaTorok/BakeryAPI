using BakeryManagement.Tests.Integration.Infrastructure;

namespace BakeryManagement.Tests.Integration.OrderItems;

public abstract class OrderItemIntegrationTestBase : IntegrationTestBase
{
    protected static readonly Guid SeededCustomerId =
        Guid.Parse("10000000-0000-0000-0000-000000000001");

    protected static readonly Guid SeededProductId =
        Guid.Parse("00000000-0000-0000-0000-000000000001");
}
