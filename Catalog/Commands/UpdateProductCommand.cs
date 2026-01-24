using MediatR;

namespace Catalog.Commands
{
    public record UpdateProductCommand : IRequest<bool>
    {
        public string Id
        {
            get;
            init;
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
        public string BrandId
        {
            get;
            init;
        }
        public string TypeId
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
