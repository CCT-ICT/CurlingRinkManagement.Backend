using CurlingRinkManagement.Base.Business.Security;
using CurlingRinkManagement.Base.Data.DatabaseModels;
using CurlingRinkManagement.Base.Data.Interfaces;
using CurlingRinkManagement.Base.Data.Models;


namespace CurlingRinkManagement.Base.Business.Services;

public class AuthenticationService(IRepository<UserDetails> _userDetailsRepo, IRepository<Role> _roleRepo, IRepository<LoginDetails> _loginDetailsRepo, IHashGenerator _hashGenerator, ITokenGenerator _tokenGenerator) : IAuthenticationService
{

    public string Register(RegisterModel registerModel)
    {
        if (_loginDetailsRepo.GetAll().Any(l => l.Username.ToLower().Trim().Equals(registerModel.Username.ToLower())))
        {
            throw new ArgumentException("User with this username already exists");
        }

        var salt = _hashGenerator.GenerateSalt();
        var hashed = _hashGenerator.Hash(registerModel.Password, salt);
        var userDetails = new UserDetails()
        {
            Id = Guid.NewGuid(),
            Email = registerModel.Email,
            DateTimeCreated = DateTime.UtcNow,
            Name = registerModel.Name.ToLower(),
            LoginDetails = new LoginDetails()
            {
                Id = Guid.NewGuid(),
                Password = hashed,
                Salt = salt
            }
        };
        var result = _userDetailsRepo.Create(userDetails);


        return _tokenGenerator.GenerateToken(result.Id.ToString(), result.Email, result.Roles ?? []);
    }
}

