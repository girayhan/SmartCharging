using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class SingleGroupQueryHandler : IRequestHandler<SingleGroupQuery, GroupDto?>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IConnectorRepository connectorRepository;
        private readonly IMapper mapper;

        public SingleGroupQueryHandler(IGroupRepository groupRepository, IChargeStationRepository chargeStationRepository, IConnectorRepository connectorRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.chargeStationRepository = chargeStationRepository;
            this.connectorRepository = connectorRepository;
            this.mapper = mapper;
        }

        public async Task<GroupDto?> Handle(SingleGroupQuery request, CancellationToken cancellationToken)
        {
            var groupEntity = await groupRepository.GetGroup(request.Id);
            if (groupEntity == null) return null;
            var chargeStationEntitiesOfGroup = await chargeStationRepository.GetAllChargeStations().Where(cs => cs.GroupId == request.Id).ToListAsync();

            var chargeStations = new List<ChargeStationDto>();
            foreach (var chargeStationEntity in chargeStationEntitiesOfGroup)
            {
                var connectorsOfChargeStation = await connectorRepository.GetAllConnectors().Where(cs => cs.ChargeStationId == chargeStationEntity.Id).ToListAsync();
                var connectersOfChargeStationDto = mapper.Map<List<ConnectorDto>>(connectorsOfChargeStation);

                var chargeStation = mapper.Map<ChargeStationEntity, ChargeStationDto>(chargeStationEntity);
                chargeStation.Connectors = connectersOfChargeStationDto;
                chargeStations.Add(chargeStation);
            }

            var groupDto = mapper.Map<GroupEntity, GroupDto>(groupEntity);
            groupDto.ChargeStations = chargeStations;

            return groupDto;
        }
    }
}
