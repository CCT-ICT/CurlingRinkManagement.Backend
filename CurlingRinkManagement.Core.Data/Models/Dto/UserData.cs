namespace CurlingRinkManagement.Core.Data.Models.Dto;
public class UserData
{
    public int Next { get; set; }
    public int Previous { get; set; }
    public int Count { get; set; }
    public int Current { get; set; }
    public int TotalPages { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }

    public List<User> Users { get; set; } = [];
}

