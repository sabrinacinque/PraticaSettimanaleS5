using PraticaSettimanaleS5.Models;

namespace PraticaSettimanaleS5.Services
{
    public interface IAuthService
    {
        ApplicationUser Login(string username, string password);
    }
}
