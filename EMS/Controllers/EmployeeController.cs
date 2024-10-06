using AutoMapper;
using Azure;
using EMS.Models;
using EMS.Service;
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


        [HttpGet, Route("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployeeBys()
        {
            try
            {
                _response.Result = await  _empRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }


        [HttpGet, Route("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            try
            {
                _response.Result = await _empRepository.GetByIdAsync(employeeId);
            }
            catch (Exception ex)
            {
                _response.Message = "Someting Went Wrong Try Again";
                _response.IsSuccess = false;
            }
            return StatusCode((int)HttpStatusCode.OK, _response);
        }

        [HttpDelete, Route("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(int employeeId)
        {
            var existingEmp = await _empRepository.GetByIdAsync(employeeId);
            if (existingEmp == null)
            {
                _response.Message = "Employee Not Found";
            }
            else
            {
                existingEmp.Status = 0;
                TblEmployee emp = _mapper.Map<TblEmployee>(existingEmp);
                await _empRepository.Delete(emp);
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

        [HttpPut, Route("UpdateEmployee")]
        public async Task<IActionResult> Update(EmployeeDto empDto)
        {
            try
            {
                TblEmployee emp = _mapper.Map<TblEmployee>(empDto);
               await _empRepository.Update(emp);
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
