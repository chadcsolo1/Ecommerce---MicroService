namespace Basket.Responses
{
    public record ShoppingCartItemResponse
    (
        string ProductId,
        string ProductName,
        decimal Price,
        int Quantity,
        string Color
    );
}
