namespace CurlingRinkManagement.Planner.Domain.Interfaces;

public interface IRepository<TEntity> where TEntity : class, IDatabaseEntity
{
    IQueryable<TEntity> GetAll();
    TEntity Create(TEntity entity);
    public void Delete(TEntity entity);
    public TEntity Update(TEntity entity);
}

