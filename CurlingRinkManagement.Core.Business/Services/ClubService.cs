using CurlingRinkManagement.Common.Data.Database;
using CurlingRinkManagement.Core.Data.DatabaseModels;
using CurlingRinkManagement.Core.Data.Interfaces;

namespace CurlingRinkManagement.Core.Business.Services;
public class ClubService(IBaseRepository<Club> _clubRepository) : IClubService
{

    public List<Club> GetClubs(List<string> groups)
    {
        return [.. _clubRepository.GetAll().Where(c => groups.Contains(c.ClubGroup))];
    }

    public Club Create(Club club)
    {
        return _clubRepository.Create(club);
    }
}
