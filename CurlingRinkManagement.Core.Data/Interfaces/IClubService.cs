using CurlingRinkManagement.Core.Data.DatabaseModels;

namespace CurlingRinkManagement.Core.Data.Interfaces;
public interface IClubService
{
    Club Create(Club club);
    List<Club> GetClubs(List<string> groups);
}

