namespace Order.DTOs
{
    public record OrderingDto
    (
        int Id,
        string UserName,
        decimal TotalPrice,
        string FirstName,
        string LastName,
        string Email,
        string Address,
        string State,
        string Country,
        string ZipCode,
        string CardNumber,
        string Expiration,
        string Cvv,
        int PaymentMethod
    );
}
