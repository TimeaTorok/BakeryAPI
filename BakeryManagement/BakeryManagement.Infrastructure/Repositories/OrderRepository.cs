using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class OrderRepository(BakeryDbContext context)
        : GenericRepository<Order>(context), IOrderRepository
    {
    }
}
