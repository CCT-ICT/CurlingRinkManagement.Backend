using CurlingRinkManagement.Planner.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurlingRinkManagement.Planner.Data.DatabaseModels;
public class Contact : IDatabaseEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string Prefix { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public string AdditionalInfo { get; set; } = string.Empty;


    public List<Label>? Labels { get; set; }
}

