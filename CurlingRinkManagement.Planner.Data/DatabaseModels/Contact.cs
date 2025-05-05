using CurlingRinkManagement.Common.Data.Database;
using Microsoft.EntityFrameworkCore;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class Contact : IClubEntity
{
    public Guid Id { get; set; }
    public Guid ClubId { get; set; } = Guid.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string AdditionalInfo { get; set; } = string.Empty;
    public DateTime DateAdded { get; set; } = DateTime.Now;

    public List<Tag>? Tags { get; set; }
}

