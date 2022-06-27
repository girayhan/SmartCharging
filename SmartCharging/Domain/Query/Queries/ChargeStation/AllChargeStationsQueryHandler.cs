using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class AllChargeStationsQueryHandler : IRequestHandler<AllChargeStationsQuery, IEnumerable<ChargeStationDto>>
    {
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IConnectorRepository connectorRepository;
        private readonly IMapper mapper;

        public AllChargeStationsQueryHandler(IChargeStationRepository chargeStationRepository, IConnectorRepository connectorRepository, IMapper mapper)
        {
            this.chargeStationRepository = chargeStationRepository;
            this.connectorRepository = connectorRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ChargeStationDto>> Handle(AllChargeStationsQuery request, CancellationToken cancellationToken)
        {
            var allChargeStationEntities = chargeStationRepository.GetAllChargeStations();
            
            var chargeStationDtos = new List<ChargeStationDto>();
            foreach (var chargeStationEntity in allChargeStationEntities)
            {
                var connectorsOfChargeStation = await connectorRepository.GetAllConnectors().Where(cs => cs.ChargeStationId == chargeStationEntity.Id).ToListAsync();
                var connectersOfChargeStationDto = mapper.Map<List<ConnectorDto>>(connectorsOfChargeStation);

                var chargeStation = mapper.Map<ChargeStationEntity, ChargeStationDto>(chargeStationEntity);
                chargeStation.Connectors = connectersOfChargeStationDto;
                chargeStationDtos.Add(chargeStation);
            }               

            return chargeStationDtos;
        }
    }
}
