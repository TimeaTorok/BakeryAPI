using BakeryManagement.Tests.Integration.Infrastructure;

namespace BakeryManagement.Tests.Integration.Customers;

public abstract class CustomerIntegrationTestBase : IntegrationTestBase
{
    protected static readonly Guid SeededCustomerWithOrdersId =
        Guid.Parse("10000000-0000-0000-0000-000000000001");

    protected static readonly Guid SeededCustomerWithoutOrdersId =
        Guid.Parse("10000000-0000-0000-0000-000000000003");

    protected static readonly Guid SeededIoanaCustomerId =
        Guid.Parse("10000000-0000-0000-0000-000000000005");
}
