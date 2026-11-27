using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Exceptions;
using BakeryManagement.Domain.Entities;
using Moq;

namespace BakeryManagement.Tests.Unit.Customers
{
    public class CustomerServiceTests : CustomerServiceTestBase
    {
        [Fact]
        public async Task GetAllCustomersAsync_WhenNoCustomersExist_ReturnsEmptyPage()
        {
            CustomerRepository
                .Setup(repository => repository.SearchPageAsync(1, 10, null))
                .ReturnsAsync(new PagedResult<Customer>
                {
                    Items = [],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 0
                });

            var result = await CustomerServiceUnderTest.GetAllCustomersAsync(1, 10, null);

            Assert.Empty(result.Items);
            Assert.Equal(0, result.TotalCount);
            Assert.Equal(0, result.TotalPages);
        }

        [Fact]
        public async Task GetAllCustomersAsync_WhenCustomersExist_ReturnsCustomers()
        {
            var customer = MakeCustomer();

            CustomerRepository
                .Setup(repository => repository.SearchPageAsync(1, 10, null))
                .ReturnsAsync(new PagedResult<Customer>
                {
                    Items = [customer],
                    PageNumber = 1,
                    PageSize = 10,
                    TotalCount = 1
                });

            var result = await CustomerServiceUnderTest.GetAllCustomersAsync(1, 10, null);

            Assert.Single(result.Items);
            Assert.Equal(customer.Id, result.Items[0].Id);
            Assert.Equal("Jane", result.Items[0].FirstName);
            Assert.Equal("Doe", result.Items[0].LastName);
            Assert.Equal(1, result.TotalCount);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_WhenCustomerDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            CustomerRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((Customer?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => CustomerServiceUnderTest.GetCustomerByIdAsync(id));
        }

        [Fact]
        public async Task GetCustomerByIdAsync_WhenCustomerExists_ReturnsCustomer()
        {
            var id = Guid.NewGuid();
            var customer = MakeCustomer(id);

            CustomerRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(customer);

            var result = await CustomerServiceUnderTest.GetCustomerByIdAsync(id);

            Assert.Equal(id, result.Id);
            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Doe", result.LastName);
            Assert.Equal("jane@example.com", result.Email);
        }

        [Fact]
        public async Task CreateCustomerAsync_WhenEmailAlreadyExists_ThrowsConflict()
        {
            var request = MakeCreateRequest();

            CustomerRepository
                .Setup(repository => repository.ExistsByEmailAsync(request.Email, null))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ConflictException>(
                () => CustomerServiceUnderTest.CreateCustomerAsync(request));

            CustomerRepository.Verify(
                repository => repository.AddAsync(It.IsAny<Customer>()),
                Times.Never);
        }

        [Fact]
        public async Task CreateCustomerAsync_WhenRequestIsValid_CreatesCustomer()
        {
            var request = MakeCreateRequest();

            CustomerRepository
                .Setup(repository => repository.ExistsByEmailAsync(request.Email, null))
                .ReturnsAsync(false);
            CustomerRepository
                .Setup(repository => repository.AddAsync(It.IsAny<Customer>()))
                .ReturnsAsync((Customer customer) => customer);

            var result = await CustomerServiceUnderTest.CreateCustomerAsync(request);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(request.FirstName, result.FirstName);
            Assert.Equal(request.LastName, result.LastName);
            Assert.Equal(request.Email, result.Email);

            CustomerRepository.Verify(
                repository => repository.AddAsync(
                    It.Is<Customer>(customer => customer.Email == request.Email)),
                Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_WhenCustomerDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            CustomerRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((Customer?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => CustomerServiceUnderTest.UpdateCustomerAsync(id, MakeUpdateRequest()));
        }

        [Fact]
        public async Task UpdateCustomerAsync_WhenCustomerExists_UpdatesCustomer()
        {
            var id = Guid.NewGuid();
            var customer = MakeCustomer(id);
            var request = MakeUpdateRequest();

            CustomerRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(customer);
            CustomerRepository
                .Setup(repository => repository.ExistsByEmailAsync(request.Email, id))
                .ReturnsAsync(false);
            CustomerRepository
                .Setup(repository => repository.UpdateAsync(It.IsAny<Customer>()))
                .Returns(Task.CompletedTask);

            var result = await CustomerServiceUnderTest.UpdateCustomerAsync(id, request);

            Assert.Equal("Janet", result.FirstName);
            Assert.Equal("Smith", result.LastName);
            Assert.Equal("janet@example.com", result.Email);
            Assert.Equal("555-0101", result.PhoneNumber);

            CustomerRepository.Verify(
                repository => repository.UpdateAsync(
                    It.Is<Customer>(updated => updated.Id == id)),
                Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_WhenCustomerDoesNotExist_ThrowsNotFound()
        {
            var id = Guid.NewGuid();

            CustomerRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync((Customer?)null);

            await Assert.ThrowsAsync<EntityNotFoundException>(
                () => CustomerServiceUnderTest.DeleteCustomerAsync(id));
        }

        [Fact]
        public async Task DeleteCustomerAsync_WhenCustomerHasNoOrders_DeletesCustomer()
        {
            var id = Guid.NewGuid();
            var customer = MakeCustomer(id);

            CustomerRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(customer);
            OrderRepository
                .Setup(repository => repository.ExistsByCustomerIdAsync(id))
                .ReturnsAsync(false);
            CustomerRepository
                .Setup(repository => repository.DeleteAsync(customer))
                .Returns(Task.CompletedTask);

            await CustomerServiceUnderTest.DeleteCustomerAsync(id);

            CustomerRepository.Verify(
                repository => repository.DeleteAsync(
                    It.Is<Customer>(deleted => deleted.Id == id)),
                Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_WhenCustomerHasOrders_ThrowsConflict()
        {
            var id = Guid.NewGuid();
            var customer = MakeCustomer(id);

            CustomerRepository
                .Setup(repository => repository.GetAsync(id))
                .ReturnsAsync(customer);
            OrderRepository
                .Setup(repository => repository.ExistsByCustomerIdAsync(id))
                .ReturnsAsync(true);

            await Assert.ThrowsAsync<ConflictException>(
                () => CustomerServiceUnderTest.DeleteCustomerAsync(id));

            CustomerRepository.Verify(
                repository => repository.DeleteAsync(It.IsAny<Customer>()),
                Times.Never);
        }
    }
}

