using System.Threading.Tasks;
using AmanaSite.Models;

namespace AmanaSite.Interfaces
{
    public interface ITokenService
    {
        public Task<string> CreateToken(AppUser user);
    }
}