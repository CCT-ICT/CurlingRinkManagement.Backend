using CurlingRinkManagement.Planner.Domain.DatabaseModels;

namespace CurlingRinkManagement.Planner.Domain.Interfaces;

public interface IActivityTypeService
{
    List<ActivityType> GetAll();
    ActivityType Update(ActivityType activityType);
    ActivityType Create(ActivityType activityType);
}

