using EMS.Services.Models.DTO;

namespace EMS.Service.Interface
{
    public interface IAuthentication
    {
        Task<string> AuthenticateUser(AuthenticationRequestDto authenticationRequestDto);
    }
}
