using Discount.Commands;
using Discount.DTOs;
using Discount.Extensions;
using Discount.Repositories;
using MediatR;

namespace Discount.Handlers
{
    public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public CreateDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<CouponDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            //Input Validation
            var validationErrors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(request.ProductName))
                validationErrors.Add(nameof(request.ProductName), "Product name is required.");

            if (string.IsNullOrWhiteSpace(request.Description))
                validationErrors.Add(nameof(request.Description), "Product description is required.");

            if (request.Amount <= 0)
                validationErrors.Add(nameof(request.Amount), "Product amount must be greater than zero.");

            if (validationErrors.Any())
                throw GrpcErrorHelper.CreateValidationException(validationErrors);
        }
    }
}
