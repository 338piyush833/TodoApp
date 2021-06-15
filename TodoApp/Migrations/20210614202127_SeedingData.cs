using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApp.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoTasks",
                columns: new[] { "Id", "Title", "description", "status" },
                values: new object[] { 1, "college project", "d1", "doing" });

            migrationBuilder.InsertData(
                table: "TodoTasks",
                columns: new[] { "Id", "Title", "description", "status" },
                values: new object[] { 2, "company project", "d2", "doing" });

            migrationBuilder.InsertData(
                table: "TodoTasks",
                columns: new[] { "Id", "Title", "description", "status" },
                values: new object[] { 3, "personal project", "d3", "completed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoTasks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodoTasks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TodoTasks",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
