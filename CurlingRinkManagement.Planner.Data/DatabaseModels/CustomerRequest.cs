using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Planner.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class CustomerRequest : IClubEntity
{
    public Guid ClubId { get; set; } = Guid.Empty;
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public int AmountOfPeople { get; set; } = 0;
    public string AdditionalInfo { get; set; } = string.Empty;
    public string? CustomPriceReason { get; set; } = string.Empty;
    public float CustomPrice { get; set; } = 0f;
    public CustomerRequestState CustomerRequestState { get; set; }


    [ForeignKey("Contact")]
    public Guid ContactId { get; set; }
    public Contact? Contact { get; set; }

    public Activity? Activity { get; set; }

}

