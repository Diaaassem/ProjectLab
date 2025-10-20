using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectLab.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDepartmentmodelwithCityproperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Departments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Departments");
        }
    }
}
