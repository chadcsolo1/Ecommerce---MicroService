using Catalog.Entities;

namespace Catalog.Responses
{
    public record ProductResponse
    {
        public string Id
        {
            get;
            set;
        } = string.Empty;
        public string Name
        {
            get;
            init;
        } = string.Empty;

        public string Summary
        {
            get;
            init;
        } = string.Empty;
        public string Description
        {
            get;
            init;
        } = string.Empty;
        public string ImageFile
        {
            get;
            init;
        } = string.Empty;
        public ProductBrand Brand
        {
            get;
            init;
        }
        public ProductType Type
        {
            get;
            init;
        }
        public decimal Price
        {
            get;
            init;
        }
        public DateTimeOffset CreatedDate
        {
            get;
            init;
        } 
    }
}
