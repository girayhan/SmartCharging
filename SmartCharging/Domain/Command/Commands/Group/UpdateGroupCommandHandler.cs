using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Exceptions;
using SmartCharging.Domain.Query.Queries.Group;

namespace SmartCharging.Domain.Command.Commands.Group
{
    public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public UpdateGroupCommandHandler(IGroupRepository groupRepository, IMapper mapper, IMediator mediator)
        {
            this.groupRepository = groupRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var currentGroupEntity = await groupRepository.GetGroup(request.Id);
            if (currentGroupEntity == null) throw new GroupDoesNotExistException();

            var newGroupEntity = this.mapper.Map<UpdateGroupCommand, GroupEntity>(request);
            await this.groupRepository.Update(currentGroupEntity, newGroupEntity);

            await ValidateCapacity(currentGroupEntity, newGroupEntity);

            return Unit.Value;
        }

        private async Task ValidateCapacity(GroupEntity currentGroupEntity, GroupEntity newGroupEntity)
        {
            if (newGroupEntity.CapacityInAmps >= currentGroupEntity.CapacityInAmps) return;

            var groupDto = await mediator.Send(new SingleGroupQuery(currentGroupEntity.Id), CancellationToken.None).ConfigureAwait(false);

            var usedCapacity = groupDto!.ChargeStations
                 .SelectMany(cs => cs.Connectors)
                 .Sum(c => c.MaxCurrentInAmps);

            if (usedCapacity > newGroupEntity.CapacityInAmps) throw new CapacityInAmpsExceededException();
        }
    }
}
