using AutoMapper;
using Azure;
using EMS.Models;
using EMS.Service.Interface;
using EMS.Services.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private readonly IResponse _response;
        private readonly IRepository<TblEmployee> _empRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IResponse response, IRepository<TblEmployee> repository, IMapper mapper)
        {
            _empRepository = repository;
            _response = response;
            _mapper = mapper;
        }

        [HttpGet, Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int userId)
        {
            try
            {
                _response.Result = await _empRepository.GetByIdAsync(userId);
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

        [HttpDelete, Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int userId)
        {
            try
            {
                _empRepository.Delete(userId);
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

        [HttpPost, Route("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeeDto empDto)
        {
            try
            {
                TblEmployee emp = _mapper.Map<TblEmployee>(empDto);
                await _empRepository.AddAsync(emp);
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

        [HttpPut, Route("UpdateUser")]
        public async Task<IActionResult> Update(EmployeeDto empDto)
        {
            try
            {
                TblEmployee emp = _mapper.Map<TblEmployee>(empDto);
                _empRepository.Update(emp);
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
