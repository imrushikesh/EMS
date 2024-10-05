using EMS.Service.Interface;
using EMS.Services.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthentication _authentication;
        private readonly IResponse _response;
        public AuthenticationController(IAuthentication authentication, IResponse response)
        {
            _authentication = authentication;
            _response = response;
        }



        [HttpPost, Route("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticationRequestDto authenticationRequestDto)
        {
            try
            {
                _response.Result = await _authentication.AuthenticateUser(authenticationRequestDto);
                _response.Message = "Success";
            }
            catch (Exception ex)
            {

                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);

        }
    }
}
