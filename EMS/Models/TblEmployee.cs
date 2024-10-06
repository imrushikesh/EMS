using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    [Table("TBL_EMPLOYEE", Schema = "dbo")]
    public class TblEmployee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("EMP_ID", TypeName = "int")]
        public int EmpId { get; set; }

        [Column("EMP_NAME", TypeName = "varchar(50)")]
        public string? EmpName { get; set; }
        [Column("EMP_DEPARTMENT", TypeName = "varchar(50)")]
        public string? Department { get; set; }

        [Column("EMP_EMAIL", TypeName = "varchar(50)")]
        public string? EmpEmail { get; set; }
        [Column("EMP_STATUS", TypeName = "int")]
        public int Status { get; set; }
    }
}
