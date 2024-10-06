using AutoMapper;
using Azure;
using EMS.Models;
using EMS.Service.Interface;
using EMS.Services.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {

        private readonly IResponse _response;
        private readonly IRepository<TblUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public UserController(IResponse response, IRepository<TblUser> repository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = repository;
            _response = response;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet, Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                _response.Result = await _userRepository.GetAllAsync();
               
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }


        [HttpGet, Route("GetUserById")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user != null)
                {
                    _response.Result = user;
                }
                else
                {
                    _response.Message = "User Not Found!";
                }

            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

        [HttpDelete, Route("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(userId);
                if (existingUser == null)
                {
                    _response.Message = "user Not Found";
                }
                else
                {
                    existingUser.Status = 0;
                    TblUser user = _mapper.Map<TblUser>(existingUser);
                    await _userRepository.Delete(user);
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

        [HttpPost, Route("CreateUser")]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            try
            {
                userDto.UserPassword = BCrypt.Net.BCrypt.HashPassword(_configuration.GetValue<string>("DEFAULT_USER_PASSWORD"));
                TblUser user = _mapper.Map<TblUser>(userDto);
                await _userRepository.AddAsync(user);
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

        [HttpPut, Route("UpdateUser")]
        public async Task<IActionResult> Update(UserDto userDto)
        {
            try
            {
                var existingUser = await _userRepository.GetByIdAsync(userDto.UserId);
                if (existingUser == null)
                {
                    _response.Message = "user Not Found";
                }
                else
                {
                    userDto.UserPassword = existingUser.UserPassword;
                    userDto.Status = 1;
                    TblUser user = _mapper.Map<TblUser>(userDto);
                   await _userRepository.Update(user);
                }
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

    }
}
