
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace CurlingRinkManagement.Base.Business.Security;
public class HashGenerator : IHashGenerator
{
    public byte[] GenerateSalt()
    {
        return RandomNumberGenerator.GetBytes(128 / 8);
    }

    public string Hash(string input, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: input!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
    }
}

