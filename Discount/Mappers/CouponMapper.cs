using Discount.DTOs;
using Discount.Entities;

namespace Discount.Mappers
{
    public static class CouponMapper
    {
        public static CouponDto ToDto(this Coupon coupon)
        {
            return new CouponDto(coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount);
        }
    }
}
