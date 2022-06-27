using SmartCharging.DataAccess.Entities;
using SmartCharging.DataAccess.Repositories;
using SmartCharging.Domain.Command.Commands.Group;

namespace SmartCharging.Test.Unit.Group
{
    public class CreateGroupCommandTest
    {
        [Fact]
        public async Task CreateGroupTest()
        {
            var guid = Guid.NewGuid();
            var commandRequest = new CreateGroupCommand { CapacityInAmps = 10, Name = "mockGroup" };
            var mappedEntity = new GroupEntity { CapacityInAmps = 10, Name = "mockGroup", Id = guid };

            var groupRepositoryMock = new Mock<IGroupRepository>();
            groupRepositoryMock.Setup(x => x.Create(mappedEntity));

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreateGroupCommand, GroupEntity>(commandRequest)).Returns(mappedEntity);

            var commandHandler = new CreateGroupCommandHandler(groupRepositoryMock.Object, mapperMock.Object);
            await commandHandler.Handle(commandRequest, CancellationToken.None);

            mapperMock.Verify(x => x.Map<CreateGroupCommand, GroupEntity>(commandRequest), Times.Once);
            groupRepositoryMock.Verify(x => x.Create(mappedEntity), Times.Once);
            commandRequest.Id.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateGroupInvalidCapacityTest()
        {
            var guid = Guid.NewGuid();
            var commandRequest = new CreateGroupCommand { CapacityInAmps = -1, Name = "mockGroup" };
            var mappedEntity = new GroupEntity { CapacityInAmps = -1, Name = "mockGroup", Id = guid };

            var groupRepositoryMock = new Mock<IGroupRepository>();

            var mapperMock = new Mock<IMapper>();

            var commandHandler = new CreateGroupCommandHandler(groupRepositoryMock.Object, mapperMock.Object);
            var task = async () => { await commandHandler.Handle(commandRequest, CancellationToken.None); }; 
            await task.Should().ThrowAsync<InvalidCapacityInAmpsException>();
        }
    }
}
