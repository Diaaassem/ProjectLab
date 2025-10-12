using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectLab.Migrations
{
    /// <inheritdoc />
    public partial class StdTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    SSN = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.SSN);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "SSN", "Address", "Age", "Email", "Image", "Name" },
                values: new object[,]
                {
                    { 1001, "123 Maple Street, Springfield", 20, "alice.johnson@example.com", "alice.jpg", "Alice Johnson" },
                    { 1002, "456 Oak Avenue, Lincoln", 22, "ben.carter@example.com", "benjamin.jpg", "Benjamin Carter" },
                    { 1003, "789 Pine Road, Riverside", 19, "chloe.martinez@example.com", "chloe.jpg", "Chloe Martinez" },
                    { 1004, "321 Birch Lane, Cedar City", 21, "daniel.kim@example.com", "daniel.jpg", "Daniel Kim" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
