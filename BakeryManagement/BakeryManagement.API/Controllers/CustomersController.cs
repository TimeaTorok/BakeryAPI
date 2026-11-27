using BakeryManagement.Application.Constants;
using BakeryManagement.Application.DTOs.Customers;
using BakeryManagement.Application.DTOs.Pagination;
using BakeryManagement.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BakeryManagement.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CustomersController(ICustomerService customerService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<CustomerDto>>> GetAll(
            [FromQuery, Range(PaginationLimits.MinimumPageNumber, PaginationLimits.MaximumPageNumber)]
                int pageNumber = PaginationLimits.DefaultPageNumber,
            [FromQuery, Range(PaginationLimits.MinimumPageSize, PaginationLimits.MaximumPageSize)]
                int pageSize = PaginationLimits.DefaultPageSize,
            [FromQuery] string? name = null) =>
            Ok(await customerService.GetAllCustomersAsync(pageNumber, pageSize, name));

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetById(Guid id)
        {
            var customer = await customerService.GetCustomerByIdAsync(id);
            return Ok(customer);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CustomerDto>> Create(CreateCustomerDto createCustomer)
        {
            var customer = await customerService.CreateCustomerAsync(createCustomer);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<CustomerDto>> Update(
            Guid id,
            UpdateCustomerDto updateCustomer)
        {
            var customer = await customerService.UpdateCustomerAsync(id, updateCustomer);
            return Ok(customer);
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
