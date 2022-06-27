using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Exceptions;

namespace SmartCharging.Domain.Command.Commands.Group
{
    public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Guid>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public CreateGroupCommandHandler(IGroupRepository groupRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
        }

        public async Task<Guid> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            if (request.CapacityInAmps < 0) throw new InvalidCapacityInAmpsException();
            
            request.Id = Guid.NewGuid();
            var groupEntity = this.mapper.Map<CreateGroupCommand, GroupEntity>(request);
            await this.groupRepository.Create(groupEntity);

            return request.Id;
        }
    }
}
