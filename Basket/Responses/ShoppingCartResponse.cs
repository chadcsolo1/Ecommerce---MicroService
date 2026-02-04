namespace Basket.Responses
{
    public record class ShoppingCartResponse(string UserName, List<ShoppingCartItemResponse> Items)
    {
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        public ShoppingCartResponse(string userName) : this(userName, new List<ShoppingCartItemResponse>())
        {
            
        }
    }
}
