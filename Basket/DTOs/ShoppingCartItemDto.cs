namespace Basket.DTOs
{
    public record ShoppingCartItemDto
    (
        string ProductId,
        string ProductName,
        decimal Price,
        int Quantity,
        string ImageFile
    );
}
