using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "TBL_EMPLOYEE",
                schema: "dbo",
                columns: table => new
                {
                    EMP_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMP_NAME = table.Column<string>(type: "varchar(50)", nullable: true),
                    EMP_DEPARTMENT = table.Column<string>(type: "varchar(50)", nullable: true),
                    EMP_EMAIL = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_EMPLOYEE", x => x.EMP_ID);
                });

            migrationBuilder.CreateTable(
                name: "TBL_USER",
                schema: "dbo",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_NAME = table.Column<string>(type: "varchar(50)", nullable: true),
                    USER_PASSWORD = table.Column<string>(type: "varchar(128)", nullable: true),
                    USER_ROLE = table.Column<string>(type: "varchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_USER", x => x.USER_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_EMPLOYEE_EMP_ID",
                schema: "dbo",
                table: "TBL_EMPLOYEE",
                column: "EMP_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TBL_USER_USER_ID",
                schema: "dbo",
                table: "TBL_USER",
                column: "USER_ID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_EMPLOYEE",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TBL_USER",
                schema: "dbo");
        }
    }
}
