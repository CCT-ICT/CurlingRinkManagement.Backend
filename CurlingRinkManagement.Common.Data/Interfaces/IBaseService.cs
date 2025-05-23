
using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Common.Data.Interfaces;
public interface IBaseService<Entity> where Entity : class, IClubEntity
{
    List<Entity> GetAll(int? page, int? amount, string[]? filters, string[]? filterValues);
    Entity GetById(Guid id);
    Entity Update(Entity entity);
    Entity Create(Entity entity);
    void Delete(Guid id);
    long GetAmount(string[]? filters, string[]? filterValues);
}

