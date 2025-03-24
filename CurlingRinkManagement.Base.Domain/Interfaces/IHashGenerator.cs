
namespace CurlingRinkManagement.Base.Business.Security;
public interface IHashGenerator
{
    byte[] GenerateSalt();
    string Hash(string input, byte[] salt);
}

