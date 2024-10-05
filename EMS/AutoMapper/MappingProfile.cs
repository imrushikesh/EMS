using AutoMapper;
using EMS.Models;
using EMS.Services.Models.DTO;


namespace EMS.AutoMapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<TblUser, UserDto>().ReverseMap();
            CreateMap<TblEmployee, EmployeeDto>().ReverseMap();
        }

    }
}
