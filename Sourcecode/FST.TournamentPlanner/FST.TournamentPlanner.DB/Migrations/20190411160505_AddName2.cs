using Microsoft.EntityFrameworkCore.Migrations;

namespace FST.TournamentPlanner.DB.Migrations
{
    public partial class AddName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name2",
                schema: "test",
                table: "UserTest",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name2",
                schema: "test",
                table: "UserTest");
        }
    }
}
