using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "USER_STATUS",
                schema: "dbo",
                table: "TBL_USER",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EMP_STATUS",
                schema: "dbo",
                table: "TBL_EMPLOYEE",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "USER_STATUS",
                schema: "dbo",
                table: "TBL_USER");

            migrationBuilder.DropColumn(
                name: "EMP_STATUS",
                schema: "dbo",
                table: "TBL_EMPLOYEE");
        }
    }
}
