namespace SmartCharging.DataAccess.Database
{
    public interface IDbContext
    {       
        IQueryable<TEntity> GetEntitySet<TEntity>() where TEntity : class;

        void Create<TEntity>(TEntity entity) where TEntity : class;

        void Update<TEntity>(TEntity currentEntity, TEntity newEntity) where TEntity : class;

        void Delete<TEntity>(TEntity entity) where TEntity : class;

        Task<int> SaveAsync();
    }
}
