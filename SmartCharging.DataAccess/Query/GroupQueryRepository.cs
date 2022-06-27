using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Query
{
    public interface GroupQueryRepository
    {
        Task<GroupEntity> GetGroup(Guid id);

        Task<IQueryable<GroupEntity>> GetAllGroups();
    }
}
