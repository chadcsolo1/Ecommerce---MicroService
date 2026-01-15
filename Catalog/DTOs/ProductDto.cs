using System.ComponentModel.DataAnnotations;

namespace Catalog.DTOs
{
    public record ProductDto(
        string Id,
        string Name,
        string Summary,
        string Description,
        string ImageFile,
        BrandDto Brand,
        TypeDto Type,
        DateTimeOffset CreatedDate
    );

    public record BrandDto(string Id, string Name);

    public class TypeDto(string Id, string Name);

    public record CreateProductDto
    {
        [Required]
        public string Name
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string Sumamry
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string Description
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string ImageFile
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string BrandId
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string TypeId
        {
            get;
            init;
        } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price
        {
            get;
            init;
        }

    };

    public record UpdateProductDto
    {
        [Required]
        public string Name
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string Sumamry
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string Description
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string ImageFile
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string BrandId
        {
            get;
            init;
        } = string.Empty;

        [Required]
        public string TypeId
        {
            get;
            init;
        } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price
        {
            get;
            init;
        }

    };
}
