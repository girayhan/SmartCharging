using Microsoft.EntityFrameworkCore;
using SmartCharging.DataAccess.Database;
using SmartCharging.DataAccess.Entities;

namespace SmartCharging.DataAccess.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IDbContext dbContext;

        public GroupRepository(IDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<GroupEntity> GetAllGroups()
        {
            return dbContext.GetEntitySet<GroupEntity>().AsQueryable();
        }

        public async Task<GroupEntity?> GetGroup(Guid id)
        {
            return await dbContext.GetEntitySet<GroupEntity>().FirstOrDefaultAsync(cs => cs.Id == id);
        }

        public async Task Create(GroupEntity groupEntity)
        {
            dbContext.Create(groupEntity);
            await dbContext.SaveAsync();
        }

        public async Task Remove(GroupEntity groupEntity)
        {
            dbContext.Delete(groupEntity);
            await dbContext.SaveAsync();
        }

        public async Task Update(GroupEntity currentGroupEntity, GroupEntity newGroupEntity)
        {
            dbContext.Update(currentGroupEntity, newGroupEntity);
            await dbContext.SaveAsync();
        }
    }
}
