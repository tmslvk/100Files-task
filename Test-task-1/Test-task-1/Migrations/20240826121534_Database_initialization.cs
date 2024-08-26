using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_task_1.Migrations
{
    public partial class Database_initialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LatinChars = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CyrillicChars = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvenNumber = table.Column<int>(type: "int", nullable: false),
                    FloatNumber = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
