using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Commands.Group;

namespace SmartCharging.Test.Unit.Group
{
    public class RemoveGroupCommandTest
    {
        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task RemoveGroupTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)
        {
            var groupToRemove = groupEntities.First();
            var commandRequest = new RemoveGroupCommand { Id = groupToRemove.Id };            

            var groupRepositoryMock = new Mock<IGroupRepository>();
            groupRepositoryMock.Setup(x => x.GetGroup(groupToRemove.Id)).Returns(Task.FromResult(groupToRemove));
            groupRepositoryMock.Setup(x => x.Remove(groupToRemove));

            var chargeStationRepositoryMock = new Mock<IChargeStationRepository>();
            chargeStationRepositoryMock.Setup(x => x.Remove(It.IsAny<ChargeStationEntity>()));
            chargeStationRepositoryMock.Setup(x => x.GetAllChargeStations()).Returns(chargeStationEntities.AsQueryable());

            var connectorRepositoryMock = new Mock<IConnectorRepository>();
            connectorRepositoryMock.Setup(x => x.Remove(It.IsAny<ConnectorEntity>()));
            connectorRepositoryMock.Setup(x => x.GetAllConnectors()).Returns(connectorEntities.AsQueryable());

            var commandHandler = new RemoveGroupCommandHandler(groupRepositoryMock.Object, chargeStationRepositoryMock.Object, connectorRepositoryMock.Object);
            await commandHandler.Handle(commandRequest, CancellationToken.None);

            groupRepositoryMock.Verify(x => x.GetGroup(groupToRemove.Id), Times.Once);
            groupRepositoryMock.Verify(x => x.Remove(groupToRemove), Times.Once);

            var chargeStationsOfGroup = chargeStationEntities.Where(cs => cs.GroupId == groupToRemove.Id);
            var countOfChargeStationsOfGroup = chargeStationsOfGroup.Count();
            chargeStationRepositoryMock.Verify(x => x.Remove(It.IsAny<ChargeStationEntity>()), Times.Exactly(countOfChargeStationsOfGroup));

            var connectorsOfGroup = connectorEntities.Where(c => chargeStationsOfGroup.Any(cs => cs.Id == c.ChargeStationId));
            var countOfConnectorsOfGroup = connectorsOfGroup.Count();
            connectorRepositoryMock.Verify(x => x.Remove(It.IsAny<ConnectorEntity>()), Times.Exactly(countOfConnectorsOfGroup));
        }
    }
}
