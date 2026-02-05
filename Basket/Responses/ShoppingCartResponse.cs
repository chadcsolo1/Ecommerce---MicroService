namespace Basket.Responses
{
    public record class ShoppingCartResponse
    {
        public string UserName
        {
            get;
            init;
        } = string.Empty;
        public List<ShoppingCartItemResponse> Items
        {
            get;
            init;
        } = new();

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);

        //Default ctor
        public ShoppingCartResponse()
        {
            UserName = string.Empty;
            Items = new List<ShoppingCartItemResponse>();
        }

        //ctor with username only
        public ShoppingCartResponse(string userName) : this(userName, new List<ShoppingCartItemResponse>())
        {
            UserName = userName;
            Items = new List<ShoppingCartItemResponse>();
        }
        

        public ShoppingCartResponse(string userName, List<ShoppingCartItemResponse> items)
        {
            UserName = userName;
            Items = items ?? new List<ShoppingCartItemResponse>();
        }
    }
}
