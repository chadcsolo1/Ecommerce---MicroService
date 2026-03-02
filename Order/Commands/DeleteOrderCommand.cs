using MediatR;

namespace Order.Commands
{
    public record DeleteOrderCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
