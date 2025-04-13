using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Interfaces;

public interface IActivityTypeService
{
    List<ActivityType> GetAll();
    ActivityType Update(ActivityType activityType);
    ActivityType Create(ActivityType activityType);
}

