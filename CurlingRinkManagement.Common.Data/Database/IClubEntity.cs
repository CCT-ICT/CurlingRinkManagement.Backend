namespace CurlingRinkManagement.Common.Data.Database;

public interface IClubEntity : IDatabaseEntity
{
    public Guid ClubId { get; set; }
}

