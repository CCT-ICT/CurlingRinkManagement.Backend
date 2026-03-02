using CurlingRinkManagement.Common.Data.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class LinkedInstructor : IDatabaseEntity
{
    public Guid Id { get; set; }
    public string UserIdentity { get; set; } = string.Empty;


    [ForeignKey("SheetActivity")]
    public Guid SheetActivityId { get; set; }

    public SheetActivity? Activity { get; set; }
}

