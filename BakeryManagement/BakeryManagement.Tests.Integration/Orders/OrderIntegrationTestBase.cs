using BakeryManagement.Tests.Integration.Infrastructure;

namespace BakeryManagement.Tests.Integration.Orders;

public abstract class OrderIntegrationTestBase : IntegrationTestBase
{
    protected static readonly Guid SeededCustomerWithOrdersId =
        Guid.Parse("10000000-0000-0000-0000-000000000001");

    protected static readonly Guid SeededOrderId =
        Guid.Parse("20000000-0000-0000-0000-000000000001");
}
