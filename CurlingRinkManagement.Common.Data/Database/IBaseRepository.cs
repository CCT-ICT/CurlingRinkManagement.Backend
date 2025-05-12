
namespace CurlingRinkManagement.Common.Data.Database;

/**
 * Repository for system wide entities
 */
public interface IBaseRepository<TEntity> where TEntity : IDatabaseEntity
{
    TEntity Create(TEntity entity);
    void Delete(TEntity entity);
    IQueryable<TEntity> GetAll();
    TEntity Update(TEntity entity);
}

