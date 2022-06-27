using AutoMapper;
using MediatR;
using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class SingleConnectorQueryHandler : IRequestHandler<SingleConnectorQuery, ConnectorDto?>
    {
        private readonly IConnectorRepository connectorRepository;
        private readonly IMapper mapper;

        public SingleConnectorQueryHandler(IConnectorRepository connectorRepository, IMapper mapper)
        {
            this.connectorRepository = connectorRepository;
            this.mapper = mapper;
        }

        public async Task<ConnectorDto?> Handle(SingleConnectorQuery request, CancellationToken cancellationToken)
        {
            var connectorEntity = await connectorRepository.GetConnector(request.Id, request.ChargeStationId);
            if (connectorEntity == null) { return null; }

            var connectorDto = mapper.Map<ConnectorEntity, ConnectorDto>(connectorEntity);

            return connectorDto;
        }
    }
}
