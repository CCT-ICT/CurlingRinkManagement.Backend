using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Core.Data.Database;

/**
 * Repository for system wide entities
 */
public class BaseRepository<TEntity>(CoreDataContext dataContext): IBaseRepository<TEntity> where TEntity : class, IDatabaseEntity
{
    private readonly CoreDataContext _dataContext = dataContext;

    public TEntity Create(TEntity entity)
    {
        entity.Id = Guid.NewGuid();
        var e = _dataContext.Add(entity);
        _dataContext.SaveChanges();
        return e.Entity;
    }

    public void Delete(TEntity entity)
    {
        _dataContext.Remove(entity);
        _dataContext.SaveChanges();
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dataContext.Set<TEntity>();
    }

    public TEntity Update(TEntity entity)
    {
        var returned = _dataContext.Update(entity);
        _dataContext.SaveChanges();
        return returned.Entity;
    }
}

