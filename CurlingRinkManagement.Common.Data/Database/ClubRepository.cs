using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Common.Data.Database;


/*
 Repository that automatically checks if a user is allowed to access the data
 */
public class ClubRepository<TEntity>(DbContext _dataContext, IHttpContextAccessor _httpContext) : IClubRepository<TEntity> where TEntity : class, IClubEntity
{

    public TEntity Create(TEntity entity)
    {
        entity.ClubId = GetClubId();
        entity.Id = Guid.NewGuid();
        var e = _dataContext.Add(entity);
        _dataContext.SaveChanges();
        return e.Entity;
    }

    public void Delete(TEntity entity)
    {
        if (entity.ClubId != GetClubId())
        {
            throw new Exception("User can't delete this object. Club ids don't match");
        }

        _dataContext.Remove(entity);
        _dataContext.SaveChanges();
    }

    public IQueryable<TEntity> GetAll()
    {
        var id = GetClubId();
        return _dataContext.Set<TEntity>().Where(e => e.ClubId == id);
    }

    public TEntity Update(TEntity entity)
    {
        if (entity.ClubId != GetClubId())
        {
            throw new Exception("User can't delete this object. Club ids don't match");
        }
        var returned = _dataContext.Update(entity);
        _dataContext.SaveChanges();
        return returned.Entity;
    }

    private Guid GetClubId()
    {
        if (_httpContext.HttpContext.Request.Headers.TryGetValue("X-Club-Id", out var clubId))
        {
            var id = clubId.First();
            if (id != null)
            {
                return Guid.Parse(id);
            }
        }
        throw new Exception("No club id defined");
    }
}
