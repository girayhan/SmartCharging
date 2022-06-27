using SmartCharging.Domain.Query.DTOs;
using System.Collections;

namespace SmartCharging.Test.Unit.Data
{
    public class SmartChargingData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { Groups, ChargeStations, Connectors };
        }

        public static List<GroupDto> CreateGroupDtoTree()
        {
            var allGroupEntities = Groups;

            var groupDtos = new List<GroupDto>();
            foreach (var groupEntity in allGroupEntities)
            {
                var chargeStationEntitiesOfGroup = ChargeStations.Where(cs => cs.GroupId == groupEntity.Id);

                var chargeStationDtos = new List<ChargeStationDto>();
                foreach (var chargeStationEntity in chargeStationEntitiesOfGroup)
                {
                    var connectorsOfChargeStation = Connectors.Where(cs => cs.ChargeStationId == chargeStationEntity.Id);

                    var connectorDtos = new List<ConnectorDto>();
                    foreach (var connectorEntity in connectorsOfChargeStation)
                    {
                        var connectorDto = new ConnectorDto
                        {
                            Id = connectorEntity.Id,
                            ChargeStationId = connectorEntity.ChargeStationId,
                            MaxCurrentInAmps = connectorEntity.MaxCurrentInAmps
                        };

                        connectorDtos.Add(connectorDto);
                    }

                    var chargeStationDto = new ChargeStationDto
                    {
                        Id = chargeStationEntity.Id,
                        Connectors = connectorDtos,
                        GroupId = chargeStationEntity.GroupId,
                        Name = chargeStationEntity.Name
                    };
                    chargeStationDtos.Add(chargeStationDto);
                }

                var groupDto = new GroupDto
                {
                    Id = groupEntity.Id,
                    Name = groupEntity.Name,
                    CapacityInAmps = groupEntity.CapacityInAmps,
                    ChargeStations = chargeStationDtos
                };
                groupDtos.Add(groupDto);
            }

            return groupDtos;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();        

        public static GroupEntity[] Groups
        {
            get
            {
                return new GroupEntity[]
                    {
                        new GroupEntity
                        {
                            Id = Guid.Parse("ca2d44b6-afbb-4761-9d78-5ece1ee2d5d6"),
                            CapacityInAmps = 30,
                            Name = "Group1"
                        },
                        new GroupEntity
                        {
                            Id =  Guid.Parse("02283d21-e78a-4f92-bad0-f301937128f4"),
                            CapacityInAmps = 20,
                            Name = "Group2"
                        }
                    };
            }
        }

        public static ChargeStationEntity[] ChargeStations
        {
            get
            {
                return new ChargeStationEntity[]
                    {
                        new ChargeStationEntity
                        {
                            Id = Guid.Parse("1dfee74b-43a2-452b-b1a3-d6a81a38dff0"),
                            Name = "ChargeStation1",
                            GroupId = Guid.Parse("ca2d44b6-afbb-4761-9d78-5ece1ee2d5d6")
                        },
                        new ChargeStationEntity
                        {
                            Id = Guid.Parse("e797b242-c153-4249-9da4-644f36a6369e"),
                            Name = "ChargeStation2",
                            GroupId = Guid.Parse("ca2d44b6-afbb-4761-9d78-5ece1ee2d5d6")
                        },
                        new ChargeStationEntity
                        {
                            Id = Guid.Parse("8cc30769-8573-4262-a589-2f2277c10acf"),
                            Name = "ChargeStation3",
                            GroupId = Guid.Parse("02283d21-e78a-4f92-bad0-f301937128f4")
                        },
                        new ChargeStationEntity
                        {
                            Id = Guid.Parse("b801857c-2f35-42eb-b61d-d94a84227cac"),
                            Name = "ChargeStation4",
                            GroupId = Guid.Parse("02283d21-e78a-4f92-bad0-f301937128f4")
                        }
                    };
            }
        }

        public static ConnectorEntity[] Connectors
        {
            get
            {
                return new ConnectorEntity[]
                    {
                        new ConnectorEntity
                        {
                            Id = 1,
                            MaxCurrentInAmps = 5,
                            ChargeStationId = Guid.Parse("1dfee74b-43a2-452b-b1a3-d6a81a38dff0")
                        },
                        new ConnectorEntity
                        {
                            Id = 2,
                            MaxCurrentInAmps = 5,
                            ChargeStationId = Guid.Parse("1dfee74b-43a2-452b-b1a3-d6a81a38dff0")
                        },
                        new ConnectorEntity
                        {
                            Id = 5,
                            MaxCurrentInAmps = 5,
                            ChargeStationId = Guid.Parse("1dfee74b-43a2-452b-b1a3-d6a81a38dff0")
                        },
                        new ConnectorEntity
                        {
                            Id = 1,
                            MaxCurrentInAmps = 10,
                            ChargeStationId = Guid.Parse("e797b242-c153-4249-9da4-644f36a6369e")
                        },
                        new ConnectorEntity
                        {
                            Id = 1,
                            MaxCurrentInAmps = 10,
                            ChargeStationId = Guid.Parse("8cc30769-8573-4262-a589-2f2277c10acf")
                        },
                        new ConnectorEntity
                        {
                            Id = 1,
                            MaxCurrentInAmps = 10,
                            ChargeStationId = Guid.Parse("8cc30769-8573-4262-a589-2f2277c10acf")
                        }
                    };
            }
        }
    }
}
