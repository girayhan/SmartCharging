using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Exceptions;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Domain.Query.Queries.Group;

namespace SmartCharging.Domain.Query.Queries
{
    public class GroupOfChargeStationQueryHandler : IRequestHandler<GroupOfChargeStationQuery, GroupDto>
    {
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public GroupOfChargeStationQueryHandler(IChargeStationRepository chargeStationRepository, IGroupRepository groupRepository, IMapper mapper, IMediator mediator)
        {
            this.chargeStationRepository = chargeStationRepository;
            this.groupRepository = groupRepository;
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<GroupDto> Handle(GroupOfChargeStationQuery request, CancellationToken cancellationToken)
        {
            var chargeStationEntity = await chargeStationRepository.GetChargeStation(request.ChargeStationId);
            if (chargeStationEntity == null) throw new ChargeStationDoesNotExistException();

            var groupEntity = await this.groupRepository.GetGroup(chargeStationEntity.GroupId);
            if (groupEntity == null) throw new GroupDoesNotExistException();

            var groupDto = await mediator.Send(new SingleGroupQuery(chargeStationEntity.GroupId), CancellationToken.None).ConfigureAwait(false);
            return groupDto!;
        }
    }
}
