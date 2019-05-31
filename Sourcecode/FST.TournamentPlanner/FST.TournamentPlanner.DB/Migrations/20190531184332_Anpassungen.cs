using Microsoft.EntityFrameworkCore.Migrations;

namespace FST.TournamentPlanner.DB.Migrations
{
    public partial class Anpassungen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Tournament_TournametId",
                schema: "tp",
                table: "Team");

            migrationBuilder.RenameColumn(
                name: "TournametId",
                schema: "tp",
                table: "Team",
                newName: "TournamentId");

            migrationBuilder.RenameIndex(
                name: "IX_Team_TournametId",
                schema: "tp",
                table: "Team",
                newName: "IX_Team_TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Tournament_TournamentId",
                schema: "tp",
                table: "Team",
                column: "TournamentId",
                principalSchema: "tp",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Team_Tournament_TournamentId",
                schema: "tp",
                table: "Team");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                schema: "tp",
                table: "Team",
                newName: "TournametId");

            migrationBuilder.RenameIndex(
                name: "IX_Team_TournamentId",
                schema: "tp",
                table: "Team",
                newName: "IX_Team_TournametId");

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Tournament_TournametId",
                schema: "tp",
                table: "Team",
                column: "TournametId",
                principalSchema: "tp",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
