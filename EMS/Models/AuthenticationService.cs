using EMS.Data;
using EMS.Service;
using EMS.Service.Interface;
using EMS.Services;
using EMS.Services.Models.DTO;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace EMS.Models
{
    public class AuthenticationService : IAuthentication
    {
        private readonly IUser _userRepository;
        private readonly IJwtUtils _jwtUtils;
        public AuthenticationService(IUser user, IJwtUtils jwt)
        {
               _jwtUtils = jwt;
            _userRepository = user;
        }
        public async Task<string> AuthenticateUser(AuthenticationRequestDto authenticationRequestDto)
        {
            string userName = authenticationRequestDto.userName;
            string pass = authenticationRequestDto.password;
            TblUser user = await _userRepository.GetByUsernameAsync(userName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(pass, user.UserPassword))
            {
                throw new Exception("Incorrect user name or password");
            }
            string Token = _jwtUtils.GetJwtToken(user);
            return Token;
        }
    }
}
