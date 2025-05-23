using CurlingRinkManagement.Common.Business.Services;
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.Enums;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;

namespace CurlingRinkManagement.Planner.Business.Services;
public class ContactService : BaseService<Contact>, IContactService
{
    private readonly IClubRepository<Contact> contactRepository;

    public ContactService(IClubRepository<Contact> _contactRepository) : base(_contactRepository)
    {
        contactRepository = _contactRepository;
    }

    public override IQueryable<Contact> AddFilter(IQueryable<Contact> query, string[] filters, string[] filterValues)
    {
        if (filters.Length != filterValues.Length) return query;
        for (int i = 0; i < filters.Length; i++)
        {
            var filter = filters[i];
            var filterValue = filterValues[i].ToLower().Replace(" ", "");
            if (Enum.TryParse<GenericFilters>(filter, out var parsedFilter))
                switch (parsedFilter)
                {
                    case GenericFilters.Generic:
                        query = query.Where(c => c.Email.ToLower().Contains(filterValue) ||
                         c.PhoneNumber.ToLower().Contains(filterValue) ||
                         (c.FirstName + c.Prefix + c.LastName).ToLower().Contains(filterValue));
                        break;
                }
        }
        return query;
    }

    public override Contact Update(Contact entity)
    {
        var inDatabase = GetById(entity.Id);
        inDatabase.FirstName = entity.FirstName;
        inDatabase.Prefix = entity.Prefix;
        inDatabase.LastName = entity.LastName;
        inDatabase.Email = entity.Email;
        inDatabase.PhoneNumber = entity.PhoneNumber;
        inDatabase.AdditionalInfo = entity.AdditionalInfo;
        return contactRepository.Update(inDatabase);
    }

}

