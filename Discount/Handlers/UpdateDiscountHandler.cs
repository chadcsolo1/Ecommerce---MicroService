using Discount.Commands;
using Discount.DTOs;
using Discount.Extensions;
using Discount.Mappers;
using Discount.Repositories;
using Grpc.Core;
using MediatR;

namespace Discount.Handlers
{
    public class UpdateDiscountHandler : IRequestHandler<UpdateDiscountCommand, CouponDto>
    {
        private readonly IDiscountRepository _discountRepository;

        public UpdateDiscountHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<CouponDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
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

            //Convert command to entity
            var couponEntity = request.ToUpdateEntity();

            var updatedCoupon = await _discountRepository.UpdateDiscount(couponEntity);

            if (!updatedCoupon)
                throw new RpcException(new Status(StatusCode.NotFound, $"Failed to update the discount for product {request.ProductName}."));

            //Convert entity back to DTO
            return couponEntity.ToDto();
        }
    }
}
