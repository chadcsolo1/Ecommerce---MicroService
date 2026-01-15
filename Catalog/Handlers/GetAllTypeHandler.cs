using Catalog.Extensions;
using Catalog.Queries;
using Catalog.Repositories;
using Catalog.Responses;
using MediatR;

namespace Catalog.Handlers
{
    public class GetAllTypeHandler : IRequestHandler<GetAllTypesQuery, IEnumerable<TypeResponse>>
    {
        private readonly ITypeRepository _typeRepository;
        public GetAllTypeHandler(ITypeRepository typeRepository)
        {
            _typeRepository = typeRepository;
        }
        public async Task<IEnumerable<TypeResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _typeRepository.GetAllTypes();
            return types.ToResponseList();
        }
    }
}
