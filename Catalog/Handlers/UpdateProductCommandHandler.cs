using Catalog.Commands;
using Catalog.Extensions;
using Catalog.Repositories;
using MediatR;

namespace Catalog.Handlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            //Fetch the existing product
            var existingProduct = await _productRepository.GetProductById(request.Id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
            }

            //Step 1: Fetch Brand and Type
            var brand = await _productRepository.GetBrandByBrandIdAsync(request.BrandId);
            var type = await _productRepository.GetTypeByTypeIdAsync(request.TypeId);

            
            if (brand == null || type == null)
            {
                throw new ApplicationException($"Brand with Id {request.BrandId} not found.");
            }

            //Step 2: Mapper - Update the existing product's properties
            var updatedProduct = request.ToUpdateEntity(existingProduct, brand, type);

            //Step 3: Save the record
            return await _productRepository.UpdateProduct(updatedProduct);
        }
    }
}
