using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.Domain.Query.DTOs;
using SmartCharging.Domain.Query.Repository;

namespace SmartCharging.Domain.Query.Queries
{
    public class SingleGroupQueryHandler : IRequestHandler<SingleGroupQuery, GroupDto>
    {
        private readonly IGroupQueryRepository groupQueryRepository;
        private readonly IChargeStationQueryRepository chargeStationQueryRepository;
        private readonly IConnectorQueryRepository connectorQueryRepository;
        private readonly IMapper mapper;

        public SingleGroupQueryHandler(IGroupQueryRepository groupQueryRepository, IChargeStationQueryRepository chargeStationQueryRepository, IConnectorQueryRepository connectorQueryRepository, IMapper mapper)
        {
            this.groupQueryRepository = groupQueryRepository;
            this.chargeStationQueryRepository = chargeStationQueryRepository;
            this.connectorQueryRepository = connectorQueryRepository;
            this.mapper = mapper;
        }

        public async Task<GroupDto> Handle(SingleGroupQuery request, CancellationToken cancellationToken)
        {
            var groupEntity = await this.groupQueryRepository.GetGroup(request.Id);
            var chargeStationEntitiesOfGroup = (await this.chargeStationQueryRepository.GetAllChargeStations()).Where(cs => cs.GroupId == request.Id);

            var chargeStations = new List<ChargeStationDto>();
            foreach(var chargeStationEntity in chargeStationEntitiesOfGroup)
            {
                var connectorsOfChargeStation = (await this.connectorQueryRepository.GetAllConnectors()).Where(cs => cs.ChargeStationId == chargeStationEntity.Id).ToList();
                var connectersOfChargeStationDto = this.mapper.Map<List<ConnectorEntity>, List<ConnectorDto>>(connectorsOfChargeStation);

                var chargeStation = this.mapper.Map<ChargeStationEntity, ChargeStationDto>(chargeStationEntity);
                chargeStation.Connectors = connectersOfChargeStationDto;
                chargeStations.Add(chargeStation);
            }

            var groupDto = this.mapper.Map<GroupEntity, GroupDto>(groupEntity);
            groupDto.ChargeStations = chargeStations;

            return groupDto;
        }
    }
}
