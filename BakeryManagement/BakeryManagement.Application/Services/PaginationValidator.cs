using System.ComponentModel.DataAnnotations;
using BakeryManagement.Application.Constants;

namespace BakeryManagement.Application.Services
{
    public static class PaginationValidator
    {
        public static void Validate(int pageNumber, int pageSize)
        {
            if (pageNumber < PaginationLimits.MinimumPageNumber ||
                pageNumber > PaginationLimits.MaximumPageNumber)
                throw new ValidationException("Page number must be at least 1.");

            if (pageSize < PaginationLimits.MinimumPageSize ||
                pageSize > PaginationLimits.MaximumPageSize)
                throw new ValidationException("Page size must be between 1 and 100.");

            if ((long)(pageNumber - 1) * pageSize > int.MaxValue)
                throw new ValidationException("Requested page number is too large.");
        }
    }
}
