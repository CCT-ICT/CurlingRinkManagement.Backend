using CurlingRinkManagement.Planner.Data.DatabaseModels;

namespace CurlingRinkManagement.Planner.Data.Models;
public class SheetActivityModel
{
    public Guid Id { get; set; }
    public DateTimeRangeModel ActivityTime { get; set; } = new();
    public List<LinkedInstructorModel> LinkedInstructors { get; set; } = new();
    public int AmountOfInstructors { get; set; } = new();
    public Guid SheetId { get; set; }
    public Guid ActivityId { get; set; }

    public SheetActivity ToSheetActivity()
    {
        return new SheetActivity()
        {
            Id = Id,
            ActivityId = ActivityId,
            SheetId = SheetId,
            ActivityTime = ActivityTime.ToDateTimeRange(),
            AmountOfInstructors = AmountOfInstructors,
            LinkedInstructors = LinkedInstructors.Select(l => l.ToLinkedInstructor()).ToList()
        };
    }

    public static SheetActivityModel FromSheetActivity(SheetActivity sheetActivity)
    {
        return new SheetActivityModel()
        {
            Id = sheetActivity.Id,
            ActivityId = sheetActivity.ActivityId,
            SheetId = sheetActivity.SheetId,
            AmountOfInstructors = sheetActivity.AmountOfInstructors,
            ActivityTime = DateTimeRangeModel.FromDateTimeRange(sheetActivity.ActivityTime),
            LinkedInstructors = sheetActivity.LinkedInstructors.Select(LinkedInstructorModel.FromLinkedInstructor).ToList()
        };
    }
}

