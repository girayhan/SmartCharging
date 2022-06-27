using MediatR;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Commands.Connector;
using SmartCharging.Domain.Query.Queries;

namespace SmartCharging.Test.Unit.Connector
{
    public class CreateConnectorCommandTest
    {
        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task CreateConnectorTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)
        {
            var id = 4;
            var chargeStationToAddConnector = chargeStationEntities.First();
            var groupDtoTree = SmartChargingData.CreateGroupDtoTree();
            var groupOfChargeStation = groupDtoTree.First(g => g.Id == chargeStationToAddConnector.GroupId);

            var commandRequest = new CreateConnectorCommand { Id = id, MaxCurrentInAmps = 3, ChargeStationId = chargeStationToAddConnector.Id };
            var mappedEntity = new ConnectorEntity { Id = id, MaxCurrentInAmps = 3, ChargeStationId = chargeStationToAddConnector.Id };

            var chargeStationRepositoryMock = new Mock<IChargeStationRepository>();
            chargeStationRepositoryMock.Setup(x => x.GetChargeStation(chargeStationToAddConnector.Id)).Returns(Task.FromResult(chargeStationToAddConnector));

            var connectorRepositoryMock = new Mock<IConnectorRepository>();
            connectorRepositoryMock.Setup(x => x.GetConnector(id, chargeStationToAddConnector.Id)).Returns(Task.FromResult(default(ConnectorEntity)));
            connectorRepositoryMock.Setup(x => x.Create(mappedEntity));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreateConnectorCommand, ConnectorEntity>(commandRequest)).Returns(mappedEntity);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GroupOfChargeStationQuery>(), CancellationToken.None)).Returns(Task.FromResult(groupOfChargeStation));

            var commandHandler = new CreateConnectorCommandHandler(connectorRepositoryMock.Object, chargeStationRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);
            await commandHandler.Handle(commandRequest, CancellationToken.None);

            connectorRepositoryMock.Verify(x => x.GetConnector(id, chargeStationToAddConnector.Id), Times.Once);
            chargeStationRepositoryMock.Verify(x => x.GetChargeStation(chargeStationToAddConnector.Id), Times.Once);
            mapperMock.Verify(x => x.Map<CreateConnectorCommand, ConnectorEntity>(commandRequest), Times.Once);
            mediatorMock.Verify(x => x.Send(It.IsAny<GroupOfChargeStationQuery>(), CancellationToken.None), Times.Once);
            connectorRepositoryMock.Verify(x => x.Create(mappedEntity), Times.Once);
        }

        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task CreateConnectorInvalidCapacityTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)        
        {
            var id = 4;
            var chargeStationToAddConnector = chargeStationEntities.First();
            var groupDtoTree = SmartChargingData.CreateGroupDtoTree();
            var groupOfChargeStation = groupDtoTree.First(g => g.Id == chargeStationToAddConnector.GroupId);

            var commandRequest = new CreateConnectorCommand { Id = id, MaxCurrentInAmps = 50, ChargeStationId = chargeStationToAddConnector.Id };
            var mappedEntity = new ConnectorEntity { Id = id, MaxCurrentInAmps = 50, ChargeStationId = chargeStationToAddConnector.Id };

            var chargeStationRepositoryMock = new Mock<IChargeStationRepository>();
            chargeStationRepositoryMock.Setup(x => x.GetChargeStation(chargeStationToAddConnector.Id)).Returns(Task.FromResult(chargeStationToAddConnector));

            var connectorRepositoryMock = new Mock<IConnectorRepository>();
            connectorRepositoryMock.Setup(x => x.GetConnector(id, chargeStationToAddConnector.Id)).Returns(Task.FromResult(default(ConnectorEntity)));
            connectorRepositoryMock.Setup(x => x.Create(mappedEntity));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreateConnectorCommand, ConnectorEntity>(commandRequest)).Returns(mappedEntity);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GroupOfChargeStationQuery>(), CancellationToken.None)).Returns(Task.FromResult(groupOfChargeStation));

            var commandHandler = new CreateConnectorCommandHandler(connectorRepositoryMock.Object, chargeStationRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);
            var task = async () => { await commandHandler.Handle(commandRequest, CancellationToken.None); };
            await task.Should().ThrowAsync<CapacityInAmpsExceededException>();
          
            connectorRepositoryMock.Verify(x => x.Create(mappedEntity), Times.Never);
        }

        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task CreateConnectorNoChargeStationTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)
        {
            var id = 4;
            var groupDtoTree = SmartChargingData.CreateGroupDtoTree();
            var groupOfChargeStation = groupDtoTree.First();

            var commandRequest = new CreateConnectorCommand { Id = id, MaxCurrentInAmps = 3 };
            var mappedEntity = new ConnectorEntity { Id = id, MaxCurrentInAmps = 3 };

            var chargeStationRepositoryMock = new Mock<IChargeStationRepository>();
            chargeStationRepositoryMock.Setup(x => x.GetChargeStation(commandRequest.ChargeStationId)).Returns(Task.FromResult(default(ChargeStationEntity)));

            var connectorRepositoryMock = new Mock<IConnectorRepository>();
            connectorRepositoryMock.Setup(x => x.GetConnector(id, commandRequest.ChargeStationId)).Returns(Task.FromResult(default(ConnectorEntity)));
            connectorRepositoryMock.Setup(x => x.Create(mappedEntity));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreateConnectorCommand, ConnectorEntity>(commandRequest)).Returns(mappedEntity);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GroupOfChargeStationQuery>(), CancellationToken.None)).Returns(Task.FromResult(groupOfChargeStation));

            var commandHandler = new CreateConnectorCommandHandler(connectorRepositoryMock.Object, chargeStationRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);
            var task = async () => { await commandHandler.Handle(commandRequest, CancellationToken.None); };
            await task.Should().ThrowAsync<ChargeStationDoesNotExistException>();
            
            connectorRepositoryMock.Verify(x => x.Create(mappedEntity), Times.Never);
        }

        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task CreateConnectorConnectorWithIdAlreadyExistsTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)
        {
            var id = 1;            
            var chargeStationToAddConnector = chargeStationEntities.First();
            var currentConnector = connectorEntities.First(c => c.Id == id && c.ChargeStationId == chargeStationToAddConnector.Id);
            var groupDtoTree = SmartChargingData.CreateGroupDtoTree();
            var groupOfChargeStation = groupDtoTree.First(g => g.Id == chargeStationToAddConnector.GroupId);

            var commandRequest = new CreateConnectorCommand { Id = id, MaxCurrentInAmps = 3, ChargeStationId = chargeStationToAddConnector.Id };
            var mappedEntity = new ConnectorEntity { Id = id, MaxCurrentInAmps = 3, ChargeStationId = chargeStationToAddConnector.Id };

            var chargeStationRepositoryMock = new Mock<IChargeStationRepository>();
            chargeStationRepositoryMock.Setup(x => x.GetChargeStation(chargeStationToAddConnector.Id)).Returns(Task.FromResult(chargeStationToAddConnector));

            var connectorRepositoryMock = new Mock<IConnectorRepository>();
            connectorRepositoryMock.Setup(x => x.GetConnector(id, chargeStationToAddConnector.Id)).Returns(Task.FromResult(currentConnector));
            connectorRepositoryMock.Setup(x => x.Create(mappedEntity));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreateConnectorCommand, ConnectorEntity>(commandRequest)).Returns(mappedEntity);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<GroupOfChargeStationQuery>(), CancellationToken.None)).Returns(Task.FromResult(groupOfChargeStation));

            var commandHandler = new CreateConnectorCommandHandler(connectorRepositoryMock.Object, chargeStationRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);
            var task = async () => { await commandHandler.Handle(commandRequest, CancellationToken.None); };
            await task.Should().ThrowAsync<ConnectorWithGivenIdAlreadyAlreadyExistsException>();

            connectorRepositoryMock.Verify(x => x.Create(mappedEntity), Times.Never);
        }
    }
}