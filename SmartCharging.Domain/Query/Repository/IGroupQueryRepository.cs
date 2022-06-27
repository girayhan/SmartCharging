using SmartCharging.DataAccess.Entities;

namespace SmartCharging.Domain.Query.Repository
{
    public interface IGroupQueryRepository
    {
        Task<GroupEntity> GetGroup(Guid id);

        Task<IQueryable<GroupEntity>> GetAllGroups();
    }
}
