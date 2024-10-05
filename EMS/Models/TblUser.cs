using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMS.Models
{
    [Table("TBL_USER", Schema = "dbo")]
    public class TblUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("USER_ID", TypeName = "int")]
        public int UserId { get; set; }

        [Column("USER_NAME", TypeName = "varchar(50)")]
        public string? UserName { get; set; }

        [Column("USER_PASSWORD", TypeName = "varchar(128)")]
        public string? UserPassword { get; set; }

        [Column("USER_ROLE", TypeName = "varchar(30)")]
        public string? Role { get; set; }

        [Column("USER_STATUS", TypeName = "int")]
        public int Status { get; set; }
    }
}