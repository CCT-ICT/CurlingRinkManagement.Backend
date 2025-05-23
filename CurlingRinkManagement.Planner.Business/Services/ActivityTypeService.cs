﻿using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Planner.Data.DatabaseModels;
using CurlingRinkManagement.Planner.Data.Interfaces;

namespace CurlingRinkManagement.Planner.Business.Services;

public class ActivityTypeService(IClubRepository<ActivityType> activityTypeRepository) : IActivityTypeService
{
    private readonly IClubRepository<ActivityType> _activityTypeRepository = activityTypeRepository;

    public ActivityType Create(ActivityType activityType)
    {
        return _activityTypeRepository.Create(activityType);
    }

    public List<ActivityType> GetAll()
    {
        return _activityTypeRepository.GetAll().ToList();
    }
    public ActivityType GetById(Guid id)
    {
        var activityType = _activityTypeRepository.GetAll().FirstOrDefault(x => x.Id == id);

        if (activityType == null)
            throw new KeyNotFoundException($"ActivityType with id {id} does not exist");
        return activityType;
    }

    public ActivityType Update(ActivityType activityType)
    {
        var toUpdate = GetById(activityType.Id);
        toUpdate.RecommendedMinutesBlockedBefore = activityType.RecommendedMinutesBlockedBefore;
        toUpdate.RecommendedMinutesBlockedAfter = activityType.RecommendedMinutesBlockedAfter;
        toUpdate.Color = activityType.Color;
        toUpdate.Type = activityType.Type;
        return _activityTypeRepository.Update(toUpdate);
    }
}