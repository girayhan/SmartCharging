using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Exceptions;

namespace SmartCharging.Domain.Command.Commands.ChargeStation
{
    public class UpdateChargeStationCommandHandler : IRequestHandler<UpdateChargeStationCommand>
    {
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public UpdateChargeStationCommandHandler(IChargeStationRepository chargeStationRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            this.chargeStationRepository = chargeStationRepository;
            this.mapper = mapper;
            this.groupRepository = groupRepository;
        }

        public async Task<Unit> Handle(UpdateChargeStationCommand request, CancellationToken cancellationToken)
        {
            var currentChargeStationEntity = await chargeStationRepository.GetChargeStation(request.Id);
            if (currentChargeStationEntity == null) throw new ChargeStationDoesNotExistException();

            var groupEntity = await groupRepository.GetGroup(request.GroupId);
            if (groupEntity == null) throw new GroupDoesNotExistException();

            var newChargeStationEntity = mapper.Map<UpdateChargeStationCommand, ChargeStationEntity>(request);
            await chargeStationRepository.Update(currentChargeStationEntity, newChargeStationEntity);

            return Unit.Value;
        }
    }
}
