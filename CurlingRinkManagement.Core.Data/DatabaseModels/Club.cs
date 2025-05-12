using CurlingRinkManagement.Common.Data.Database;

namespace CurlingRinkManagement.Core.Data.DatabaseModels;
public class Club : IDatabaseEntity
{
    public Guid Id { get; set; }
    public string ClubName { get; set; } = string.Empty;
    public string ClubAbbriviation { get; set; } = string.Empty;
    public string ClubGroup { get; set; } = string.Empty;
}

