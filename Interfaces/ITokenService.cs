using StocksApp.Models;

namespace StocksApp.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
