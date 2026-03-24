namespace Basket.DTOs
{
    public record BasketCheckoutDto
    (
        string Username,
        string TotalPrice,
        string FirstName,
        string LastName,
        string Email,
        string Address,
        string Country,
        string State,
        string ZipCode,
        string Cardname,
        string CardNumber,
        string Expiration,
        string Cvv,
        int PaymentMethod
    );
}
