namespace Basket.Entities
{
    public class BasketCheckout
    {
        public string UserName
        {
            get;
            set;
        } = string.Empty;
        public decimal TotalPrice
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        } = string.Empty;
        public string LastName
        {
            get;
            set;
        } = string.Empty;
        public string Email
        {
            get;
            set;
        } = string.Empty;
        public string Address
        {
            get;
            set;
        } = string.Empty;
        public string Country
        {
            get;
            set;
        } = string.Empty;
        public string State
        {
            get;
            set;
        } = string.Empty;
        public string ZipCode
        {
            get;
            set;
        } = string.Empty;
        public string CardName
        {
            get;
            set;
        } = string.Empty;
        public string CardNumber
        {
            get;
            set;
        } = string.Empty;
        public string CardExpiration
        {
            get;
            set;
        } = string.Empty;
        public string CVV
        {
            get;
            set;
        } = string.Empty;
        public string PaymentMethod
        {
            get;
            set;
        } = string.Empty;
    }
}
