using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Repositories
{
    public interface IGroupRepository
    {
        Task<GroupEntity?> GetGroup(Guid id);

        IQueryable<GroupEntity> GetAllGroups();

        Task Create(GroupEntity groupEntity);

        Task Update(GroupEntity currentGroupEntity, GroupEntity newGroupEntity);

        Task Remove(GroupEntity groupEntity);
    }
}
