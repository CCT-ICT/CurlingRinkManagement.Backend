
using CurlingRinkManagement.Planner.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class CustomerRequest : IDatabaseEntity
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public int AmountOfPeople { get; set; } = 0;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string? CustomPriceReason { get; set; } = string.Empty;
    public float CustomPrice { get; set; } = 0f;


    [ForeignKey("Contact")]
    public Guid ContactId { get; set; }
    public Contact? Contact { get; set; }

    [ForeignKey("Activity")]
    public Guid ActivityId { get; set; }
    public Activity? Activity { get; set; }

}

