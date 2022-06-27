using MediatR;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Commands.Group;
using SmartCharging.Domain.Query.Queries.Group;

namespace SmartCharging.Test.Unit.Group
{
    public class UpdateGroupCommandTest
    {
        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task UpdateGroupTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)
        {
            var groupToUpdate = groupEntities.First();
            var groupDtoTree = SmartChargingData.CreateGroupDtoTree();
            var groupDto = groupDtoTree.Single(g => g.Id == groupToUpdate.Id);

            var commandRequest = new UpdateGroupCommand { Id = groupToUpdate.Id, CapacityInAmps = 29, Name = "mockGroup" };
            var mappedEntity = new GroupEntity { Id = groupToUpdate.Id, CapacityInAmps = 29, Name = "mockGroup" };

            var groupRepositoryMock = new Mock<IGroupRepository>();
            groupRepositoryMock.Setup(x => x.GetGroup(groupToUpdate.Id)).Returns(Task.FromResult(groupToUpdate));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<UpdateGroupCommand, GroupEntity>(commandRequest)).Returns(mappedEntity);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<SingleGroupQuery>(), CancellationToken.None)).Returns(Task.FromResult(groupDto));

            var commandHandler = new UpdateGroupCommandHandler(groupRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);
            await commandHandler.Handle(commandRequest, CancellationToken.None);

            groupRepositoryMock.Verify(x => x.GetGroup(groupToUpdate.Id), Times.Once);
            groupRepositoryMock.Verify(x => x.Update(groupToUpdate, mappedEntity), Times.Once);
            mapperMock.Verify(x => x.Map<UpdateGroupCommand, GroupEntity>(commandRequest), Times.Once);
            mediatorMock.Verify(x => x.Send(It.IsAny<SingleGroupQuery>(), CancellationToken.None), Times.Once);
        }

        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task UpdateGrouNoValidationNeededTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)
        {
            var groupToUpdate = groupEntities.First();
            var groupDtoTree = SmartChargingData.CreateGroupDtoTree();
            var groupDto = groupDtoTree.Single(g => g.Id == groupToUpdate.Id);

            var commandRequest = new UpdateGroupCommand { Id = groupToUpdate.Id, CapacityInAmps = 50, Name = "mockGroup" };
            var mappedEntity = new GroupEntity { Id = groupToUpdate.Id, CapacityInAmps = 50, Name = "mockGroup" };

            var groupRepositoryMock = new Mock<IGroupRepository>();
            groupRepositoryMock.Setup(x => x.GetGroup(groupToUpdate.Id)).Returns(Task.FromResult(groupToUpdate));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<UpdateGroupCommand, GroupEntity>(commandRequest)).Returns(mappedEntity);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<SingleGroupQuery>(), CancellationToken.None)).Returns(Task.FromResult(groupDto));

            var commandHandler = new UpdateGroupCommandHandler(groupRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);
            await commandHandler.Handle(commandRequest, CancellationToken.None);

            groupRepositoryMock.Verify(x => x.GetGroup(groupToUpdate.Id), Times.Once);
            groupRepositoryMock.Verify(x => x.Update(groupToUpdate, mappedEntity), Times.Once);
            mapperMock.Verify(x => x.Map<UpdateGroupCommand, GroupEntity>(commandRequest), Times.Once);
            mediatorMock.Verify(x => x.Send(It.IsAny<SingleGroupQuery>(), CancellationToken.None), Times.Never);
        }

        [Theory]
        [ClassData(typeof(SmartChargingData))]
        public async Task UpdateGroupExceedCapacityTest(GroupEntity[] groupEntities, ChargeStationEntity[] chargeStationEntities, ConnectorEntity[] connectorEntities)
        {
            var groupToUpdate = groupEntities.First();
            var groupDtoTree = SmartChargingData.CreateGroupDtoTree();
            var groupDto = groupDtoTree.Single(g => g.Id == groupToUpdate.Id);

            var commandRequest = new UpdateGroupCommand { Id = groupToUpdate.Id, CapacityInAmps = 10, Name = "mockGroup" };
            var mappedEntity = new GroupEntity { Id = groupToUpdate.Id, CapacityInAmps = 10, Name = "mockGroup" };

            var groupRepositoryMock = new Mock<IGroupRepository>();
            groupRepositoryMock.Setup(x => x.GetGroup(groupToUpdate.Id)).Returns(Task.FromResult(groupToUpdate));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<UpdateGroupCommand, GroupEntity>(commandRequest)).Returns(mappedEntity);

            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(x => x.Send(It.IsAny<SingleGroupQuery>(), CancellationToken.None)).Returns(Task.FromResult(groupDto));

            var commandHandler = new UpdateGroupCommandHandler(groupRepositoryMock.Object, mapperMock.Object, mediatorMock.Object);
            var task = async () => { await commandHandler.Handle(commandRequest, CancellationToken.None); };
            await task.Should().ThrowAsync<CapacityInAmpsExceededException>();

            groupRepositoryMock.Verify(x => x.GetGroup(groupToUpdate.Id), Times.Once);
            groupRepositoryMock.Verify(x => x.Update(groupToUpdate, mappedEntity), Times.Never);
            mapperMock.Verify(x => x.Map<UpdateGroupCommand, GroupEntity>(commandRequest), Times.Once);
            mediatorMock.Verify(x => x.Send(It.IsAny<SingleGroupQuery>(), CancellationToken.None), Times.Once);
        }
    }
}
