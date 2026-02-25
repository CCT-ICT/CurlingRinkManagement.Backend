using CurlingRinkManagement.Common.Business.Services;
using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.Enums;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Enums;
using CurlingRinkManagement.Planner.Data.Interfaces;
namespace CurlingRinkManagement.Planner.Business.Services;
public class CustomerRequestService(IClubRepository<CustomerRequest> _customerRequestRepo) : BaseService<CustomerRequest>(_customerRequestRepo), ICustomerRequestService
{

    public override CustomerRequest Update(CustomerRequest entity)
    {
        var inDatabase = GetById(entity.Id);
        inDatabase.AdditionalInfo = entity.AdditionalInfo;
        inDatabase.ActivityId = entity.ActivityId;
        inDatabase.AmountOfPeople = entity.AmountOfPeople;
        inDatabase.CustomPrice = entity.CustomPrice;
        inDatabase.CustomPriceReason = entity.CustomPriceReason;
        inDatabase.ContactId = entity.ContactId;
        inDatabase.Title = entity.Title;
        inDatabase.CustomerRequestState = entity.CustomerRequestState;
        return _entityRepository.Update(inDatabase);
    }

    public override IQueryable<CustomerRequest> AddFilter(IQueryable<CustomerRequest> query, string[] filters, string[] filterValues)
    {
        if (filters.Length != filterValues.Length) return query;
        for (int i = 0; i < filters.Length; i++)
        {
            var filter = filters[i];
            var filterValue = filterValues[i].ToLower().Replace(" ", "");
            if (!Enum.TryParse<GenericFilters>(filter, out var parsedFilter))
                continue;
            switch (parsedFilter)
            {
                case GenericFilters.CustomerRequestState:
                    if (!Enum.TryParse<CustomerRequestState>(filterValue, out var state))
                        break;

                    query = query.Where(c => c.CustomerRequestState == state);
                    break;
            }
        }
        return query;
    }
}

