using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class AllGroupsQueryHandler : IRequestHandler<AllGroupsQuery, IEnumerable<GroupDto>>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IConnectorRepository connectorRepository;
        private readonly IMapper mapper;

        public AllGroupsQueryHandler(IGroupRepository groupRepository, IChargeStationRepository chargeStationRepository, IConnectorRepository connectorRepository, IMapper mapper)
        {
            this.groupRepository = groupRepository;
            this.chargeStationRepository = chargeStationRepository;
            this.connectorRepository = connectorRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<GroupDto>> Handle(AllGroupsQuery request, CancellationToken cancellationToken)
        {
            var allGroupEntities = groupRepository.GetAllGroups();

            var groupDtos = new List<GroupDto>();
            foreach (var groupEntity in allGroupEntities)
            {
                var chargeStationEntitiesOfGroup = await chargeStationRepository.GetAllChargeStations().Where(cs => cs.GroupId == groupEntity.Id).ToListAsync();

                var chargeStationDtos = new List<ChargeStationDto>();
                foreach (var chargeStationEntity in chargeStationEntitiesOfGroup)
                {
                    var connectorsOfChargeStation = await connectorRepository.GetAllConnectors().Where(cs => cs.ChargeStationId == chargeStationEntity.Id).ToListAsync();
                    var connectersOfChargeStationDto = mapper.Map<List<ConnectorDto>>(connectorsOfChargeStation);

                    var chargeStationDto = mapper.Map<ChargeStationEntity, ChargeStationDto>(chargeStationEntity);
                    chargeStationDto.Connectors = connectersOfChargeStationDto;
                    chargeStationDtos.Add(chargeStationDto);
                }

                var groupDto = mapper.Map<GroupEntity, GroupDto>(groupEntity);
                groupDto.ChargeStations = chargeStationDtos;

                groupDtos.Add(groupDto);
            }

            return groupDtos;
        }
    }
}
