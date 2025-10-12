using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProjectLab.Migrations
{
    /// <inheritdoc />
    public partial class updateSSN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 1001);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 1002);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 1003);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 1004);

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "SSN", "Address", "Age", "Email", "Image", "Name" },
                values: new object[,]
                {
                    { 1, "123 Maple Street, Springfield", 20, "alice.johnson@example.com", "alice.jpg", "Alice Johnson" },
                    { 2, "456 Oak Avenue, Lincoln", 22, "ben.carter@example.com", "benjamin.jpg", "Benjamin Carter" },
                    { 3, "789 Pine Road, Riverside", 19, "chloe.martinez@example.com", "chloe.jpg", "Chloe Martinez" },
                    { 4, "321 Birch Lane, Cedar City", 21, "daniel.kim@example.com", "daniel.jpg", "Daniel Kim" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "SSN",
                keyValue: 4);

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
    }
}
