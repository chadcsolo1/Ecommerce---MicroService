namespace Basket.Entities
{
    public class ShoppingCart
    {
        public string UserName
        {
            get;
            set;
        } = string.Empty;
        public List<CartItem> Items
        {
            get;
            set;
        } = new List<CartItem>();
        
        public ShoppingCart(string userName)
        {
            UserName = userName;
        }
    }
}
