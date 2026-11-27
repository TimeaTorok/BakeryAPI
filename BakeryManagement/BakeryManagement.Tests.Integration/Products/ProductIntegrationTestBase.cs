using BakeryManagement.Tests.Integration.Infrastructure;

namespace BakeryManagement.Tests.Integration.Products;

public abstract class ProductIntegrationTestBase : IntegrationTestBase
{
    protected static readonly Guid SeededCountrySourdoughId =
        Guid.Parse("00000000-0000-0000-0000-000000000001");

    protected static readonly Guid SeededUnreferencedProductId =
        Guid.Parse("00000000-0000-0000-0000-000000000090");
}
