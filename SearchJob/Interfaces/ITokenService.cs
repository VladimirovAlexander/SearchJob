using SearchJob.Models;

namespace SearchJob.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
