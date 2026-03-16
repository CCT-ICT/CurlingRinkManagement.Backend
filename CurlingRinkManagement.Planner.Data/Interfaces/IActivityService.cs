

using CurlingRinkManagement.Common.Data.Interfaces;
using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Interfaces;

public interface IActivityService : IBaseService<Activity>
{
    void AddInstructor(string instructorId, Guid activityId, Guid sheetActivityId);
}

