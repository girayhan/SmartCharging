using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Domain.Query.Queries.Group
{
    public class AllConnectorsQueryHandler : IRequestHandler<AllConnectorsQuery, IEnumerable<ConnectorDto>>
    {
        private readonly IConnectorRepository connectorRepository;
        private readonly IMapper mapper;

        public AllConnectorsQueryHandler(IConnectorRepository connectorRepository, IMapper mapper)
        {
            this.connectorRepository = connectorRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ConnectorDto>> Handle(AllConnectorsQuery request, CancellationToken cancellationToken)
        {
            var connectorEntities = await connectorRepository.GetAllConnectors().ToListAsync();
            var connecterDtos = mapper.Map<List<ConnectorDto>>(connectorEntities);

            return connecterDtos;
        }
    }
}
