namespace CurlingRinkManagement.Common.Data.Database;

public interface IClubRepository<TEntity> where TEntity : class, IClubEntity
{
    IQueryable<TEntity> GetAll();
    TEntity Create(TEntity entity);
    public void Delete(TEntity entity);
    public TEntity Update(TEntity entity);
}

