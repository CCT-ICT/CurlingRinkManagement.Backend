using CurlingRinkManagement.Planner.Data.DatabaseModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurlingRinkManagement.Planner.Data.Models;
public class SheetActivityModel
{
    public Guid Id { get; set; }
    public DateTimeRangeModel ActivityTime { get; set; } = new();

    public Guid SheetId { get; set; }
    public Guid ActivityId { get; set; }

    public SheetActivity ToSheetActivity()
    {
        return new SheetActivity()
        {
            Id = Id,
            ActivityId = ActivityId,
            SheetId = SheetId,
            ActivityTime = ActivityTime.ToDateTimeRange()
        };
    }

    public static SheetActivityModel FromSheetActivity(SheetActivity sheetActivity)
    {
        return new SheetActivityModel()
        {
            Id = sheetActivity.Id,
            ActivityId = sheetActivity.ActivityId,
            SheetId = sheetActivity.SheetId,
            ActivityTime = DateTimeRangeModel.FromDateTimeRange(sheetActivity.ActivityTime)
        };
    }
}

