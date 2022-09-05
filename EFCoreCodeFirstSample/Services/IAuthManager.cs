using EFCoreCodeFirstSample.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EFCoreCodeFirstSample.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
        //Task<string> CreateRefreshToken();
      //  Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    }
}