using MediatR;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries
{
    public class SingleGroupQuery : IRequest<GroupDto>
    {
        public SingleGroupQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
