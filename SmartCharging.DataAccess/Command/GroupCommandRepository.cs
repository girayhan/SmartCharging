using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Command
{
    public interface GroupCommandRepository
    {
        Task CreateGroup(GroupEntity groupEntity);

        Task UpdateGroup(GroupEntity groupEntity);

        Task RemoveGroup(GroupEntity groupEntity);
    }
}
