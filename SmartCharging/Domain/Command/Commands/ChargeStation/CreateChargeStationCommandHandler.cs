using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Exceptions;

namespace SmartCharging.Domain.Command.Commands.ChargeStation
{
    public class CreateChargeStationCommandHandler : IRequestHandler<CreateChargeStationCommand, Guid>
    {
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IGroupRepository groupRepository;
        private readonly IMapper mapper;

        public CreateChargeStationCommandHandler(IChargeStationRepository chargeStationRepository, IGroupRepository groupRepository, IMapper mapper)
        {
            this.chargeStationRepository = chargeStationRepository;
            this.mapper = mapper;
            this.groupRepository = groupRepository;
        }

        public async Task<Guid> Handle(CreateChargeStationCommand request, CancellationToken cancellationToken)
        {
            var groupEntity = await groupRepository.GetGroup(request.GroupId);
            if (groupEntity == null) throw new GroupDoesNotExistException();

            request.Id = Guid.NewGuid();
            var chargeStationEntity = mapper.Map<CreateChargeStationCommand, ChargeStationEntity>(request);
            await chargeStationRepository.Create(chargeStationEntity);

            return request.Id;
        }
    }
}
