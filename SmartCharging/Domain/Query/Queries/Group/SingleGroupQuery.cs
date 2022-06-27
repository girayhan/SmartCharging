using MediatR;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class SingleGroupQuery : IRequest<GroupDto?>
    {
        public SingleGroupQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
