

using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Interfaces;

public interface IActivityService
{
    public Activity Create(Activity activity);
    public void Delete(Guid id);
    public Activity Update(Activity activity);
    public List<Activity> GetAllOnSheet(Guid sheetId, DateTime start, DateTime end);
}

