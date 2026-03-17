using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Models;
public class LinkedInstructorModel
{
    public Guid Id { get; set; }
    public string UserIdentity { get; set; } = string.Empty;

    public LinkedInstructor ToLinkedInstructor()
    {
        return new LinkedInstructor()
        {
            Id = Id,
            UserIdentity = UserIdentity
        };
    }

    public static LinkedInstructorModel FromLinkedInstructor(LinkedInstructor linkedInstructor)
    {
        return new LinkedInstructorModel()
        {
            Id = linkedInstructor.Id,
            UserIdentity = linkedInstructor.UserIdentity
        };
    }
}

