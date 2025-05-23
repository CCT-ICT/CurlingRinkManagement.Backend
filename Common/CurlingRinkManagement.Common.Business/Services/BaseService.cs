using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Common.Data.Interfaces;

namespace CurlingRinkManagement.Common.Business.Services;
public abstract class BaseService<Entity>(IClubRepository<Entity> _entityRepository) : IBaseService<Entity> where Entity : class, IClubEntity
{
    public virtual Entity Create(Entity entity)
    {
        return _entityRepository.Create(entity);
    }

    public virtual void Delete(Guid id)
    {
        var entity = GetById(id);
        _entityRepository.Delete(entity);
    }

    //Add pagination
    public virtual List<Entity> GetAll(int? page, int? amount, string[]? filters, string[]? filterValues)
    {
        var query = _entityRepository.GetAll();
        if (filters != null && filterValues != null)
        {
            query = AddFilter(query, filters, filterValues);
        }
        if (page != null && amount != null && page > 0 && amount > 0 && amount <= 300)
        {
            query = query.Skip((page.Value - 1) * amount.Value).Take(amount.Value);
        }
        else
        {
            query = query.Take(300);
        }

        return query.ToList();
    }

    public virtual long GetAmount(string[]? filters, string[]? filterValues)
    {
        var query = _entityRepository.GetAll();
        if (filters != null && filterValues != null)
        {
            query = AddFilter(query, filters, filterValues);
        }
        return query.LongCount();
    }

    public virtual Entity GetById(Guid id)
    {
        var entity = _entityRepository.GetAll().FirstOrDefault(e => e.Id == id);
        if (entity == null)
            throw new KeyNotFoundException($"No {typeof(Entity).Name} with id {id} found");
        return entity;
    }

    public abstract Entity Update(Entity entity);
    public virtual IQueryable<Entity> AddFilter(IQueryable<Entity> query, string[] filters, string[] filterValues)
    {
        return query;
    }
}

