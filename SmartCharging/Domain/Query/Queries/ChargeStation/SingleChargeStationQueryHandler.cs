using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class SingleChargeStationQueryHandler : IRequestHandler<SingleChargeStationQuery, ChargeStationDto?>
    {
        private readonly IChargeStationRepository chargeStationRepository;
        private readonly IConnectorRepository connectorRepository;
        private readonly IMapper mapper;

        public SingleChargeStationQueryHandler(IChargeStationRepository chargeStationRepository, IConnectorRepository connectorRepository, IMapper mapper)
        {
            this.chargeStationRepository = chargeStationRepository;
            this.connectorRepository = connectorRepository;
            this.mapper = mapper;
        }

        public async Task<ChargeStationDto?> Handle(SingleChargeStationQuery request, CancellationToken cancellationToken)
        {
            var chargeStationEntity = await chargeStationRepository.GetChargeStation(request.Id);
            if (chargeStationEntity == null) return null;

            var connectorsOfChargeStation = await connectorRepository.GetAllConnectors().Where(cs => cs.ChargeStationId == chargeStationEntity.Id).ToListAsync();
            var connectersOfChargeStationDto = mapper.Map<List<ConnectorDto>>(connectorsOfChargeStation);

            var chargeStation = mapper.Map<ChargeStationEntity, ChargeStationDto>(chargeStationEntity);
            chargeStation.Connectors = connectersOfChargeStationDto;

            return chargeStation;
        }
    }
}
