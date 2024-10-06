using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMS.Services.Models.DTO
{
    public class EmployeeDto
    {
        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? Department { get; set; }
        public string? EmpEmail { get; set; }
        public int Status { get; set; } = 1;
    }
}
