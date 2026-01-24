using Catalog.Entities;
using Catalog.Responses;
using MediatR;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Commands
{
    public class CreateProductCommand: IRequest<ProductResponse>
    {
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
