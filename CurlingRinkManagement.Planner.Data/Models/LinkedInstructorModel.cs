using CurlingRinkManagement.Planner.Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurlingRinkManagement.Planner.Data.Models;
public class LinkedInstructorModel
{
    public Guid Id { get; set; }
    public string UserIdentity { get; set; } = string.Empty;
    public Guid SheetActivityId { get; set; }

    public LinkedInstructor ToLinkedInstructor()
    {
        return new LinkedInstructor()
        {
            Id = Id,
            UserIdentity = UserIdentity,
            SheetActivityId = SheetActivityId,
        };
    }

    public static LinkedInstructorModel FromLinkedInstructor(LinkedInstructor linkedInstructor)
    {
        return new LinkedInstructorModel()
        {
            Id = linkedInstructor.Id,
            UserIdentity = linkedInstructor.UserIdentity,
            SheetActivityId = linkedInstructor.Activity != null ? linkedInstructor.Activity.Id : linkedInstructor.SheetActivityId,
        };
    }
}

