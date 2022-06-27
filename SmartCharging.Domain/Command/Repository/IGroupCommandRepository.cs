using SmartCharging.DataAccess.Entities;

namespace SmartCharging.Domain.Command.Repository
{
    public interface IGroupCommandRepository
    {
        Task CreateGroup(GroupEntity groupEntity);

        Task UpdateGroup(GroupEntity groupEntity);

        Task RemoveGroup(GroupEntity groupEntity);
    }
}
