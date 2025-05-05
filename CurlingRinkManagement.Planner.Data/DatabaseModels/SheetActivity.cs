using CurlingRinkManagement.Common.Data.Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class SheetActivity : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; }

    //references
    [ForeignKey("Sheet")]
    public Guid SheetId { get; set; }
    public Sheet? Sheet { get; set; }

    [ForeignKey("Activity")]
    public Guid ActivityId { get; set; }
    public Activity? Activity { get; set; }
}
