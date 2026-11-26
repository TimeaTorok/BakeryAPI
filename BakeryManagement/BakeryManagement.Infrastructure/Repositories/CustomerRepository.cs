using BakeryManagement.Application.Interfaces.Repositories;
using BakeryManagement.Domain.Entities;
using BakeryManagement.Infrastructure.Persistence;

namespace BakeryManagement.Infrastructure.Repositories
{
    public class CustomerRepository(BakeryDbContext context)
        : GenericRepository<Customer>(context), ICustomerRepository
    {
    }
}
