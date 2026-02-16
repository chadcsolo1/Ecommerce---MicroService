using Discount.Commands;
using Discount.Grpc.Protos;
using Discount.Mappers;
using Discount.Queries;
using Grpc.Core;
using MediatR;

namespace Discount.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator _mediator;

        public DiscountService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var query = new GetDiscountQuery(request.ProductName);
            var dto = await _mediator.Send(query);
            return dto.ToCouponModel();
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var command = request.Coupon.ToCreateCommand();
            var dto = await _mediator.Send(command);
            return dto.ToCouponModel();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = request.Coupon.ToUpdateCommand();
            var dto = await _mediator.Send(command);
            return dto.ToCouponModel();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var command = new DeleteDiscountCommand(request.ProductName);
            var success = await _mediator.Send(command);
            return new DeleteDiscountResponse { Success = success };
        }
    }
}
