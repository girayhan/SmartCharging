using AutoMapper;
using SmartCharging.DataAccess.Entities;
using SmartCharging.Domain.Command.Commands.ChargeStation;
using SmartCharging.Domain.Command.Commands.Connector;
using SmartCharging.Domain.Command.Commands.Group;
using SmartCharging.Domain.Query.DTOs;

namespace SmartCharging.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateGroupCommand, GroupEntity>();
            CreateMap<ChargeStationEntity, ChargeStationDto>();
            CreateMap<ConnectorEntity, ConnectorDto>();
            CreateMap<GroupEntity, GroupDto>();
            CreateMap<UpdateGroupCommand, GroupEntity>();
            CreateMap<CreateConnectorCommand, ConnectorEntity>();
            CreateMap<UpdateConnectorCommand, ConnectorEntity>();
            CreateMap<CreateChargeStationCommand, ChargeStationEntity>();
            CreateMap<UpdateChargeStationCommand, ChargeStationEntity>();
        }
    }
}
