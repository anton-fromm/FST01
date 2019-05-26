using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FST.TournamentPlanner.DB.Migrations
{
    public partial class ReworkDatabaseV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "tp");

            migrationBuilder.CreateTable(
                name: "Tournament",
                schema: "tp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    MatchDuration = table.Column<int>(nullable: false),
                    TeamCount = table.Column<int>(nullable: false),
                    State = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayArea",
                schema: "tp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    TournamentId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayArea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayArea_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "tp",
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                schema: "tp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    TournametId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Tournament_TournametId",
                        column: x => x.TournametId,
                        principalSchema: "tp",
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayAreaBooking",
                schema: "tp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    PlayAreaId = table.Column<int>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayAreaBooking", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayAreaBooking_PlayArea_PlayAreaId",
                        column: x => x.PlayAreaId,
                        principalSchema: "tp",
                        principalTable: "PlayArea",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchResult",
                schema: "tp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    MatchId = table.Column<int>(nullable: true),
                    TeamId = table.Column<int>(nullable: true),
                    Score = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchResult_Team_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "tp",
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Match",
                schema: "tp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    TeamOneId = table.Column<int>(nullable: true),
                    TeamTwoId = table.Column<int>(nullable: true),
                    PlayAreaBookingId = table.Column<int>(nullable: true),
                    SuccessorId = table.Column<int>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    TournamentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Match_PlayAreaBooking_PlayAreaBookingId",
                        column: x => x.PlayAreaBookingId,
                        principalSchema: "tp",
                        principalTable: "PlayAreaBooking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Match_SuccessorId",
                        column: x => x.SuccessorId,
                        principalSchema: "tp",
                        principalTable: "Match",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_MatchResult_TeamOneId",
                        column: x => x.TeamOneId,
                        principalSchema: "tp",
                        principalTable: "MatchResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_MatchResult_TeamTwoId",
                        column: x => x.TeamTwoId,
                        principalSchema: "tp",
                        principalTable: "MatchResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Match_Tournament_TournamentId",
                        column: x => x.TournamentId,
                        principalSchema: "tp",
                        principalTable: "Tournament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Match_PlayAreaBookingId",
                schema: "tp",
                table: "Match",
                column: "PlayAreaBookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_SuccessorId",
                schema: "tp",
                table: "Match",
                column: "SuccessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TeamOneId",
                schema: "tp",
                table: "Match",
                column: "TeamOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TeamTwoId",
                schema: "tp",
                table: "Match",
                column: "TeamTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Match_TournamentId",
                schema: "tp",
                table: "Match",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResult_MatchId",
                schema: "tp",
                table: "MatchResult",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchResult_TeamId",
                schema: "tp",
                table: "MatchResult",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayArea_TournamentId",
                schema: "tp",
                table: "PlayArea",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayAreaBooking_PlayAreaId",
                schema: "tp",
                table: "PlayAreaBooking",
                column: "PlayAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_TournametId",
                schema: "tp",
                table: "Team",
                column: "TournametId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchResult_Match_MatchId",
                schema: "tp",
                table: "MatchResult",
                column: "MatchId",
                principalSchema: "tp",
                principalTable: "Match",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Match_PlayAreaBooking_PlayAreaBookingId",
                schema: "tp",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_MatchResult_TeamOneId",
                schema: "tp",
                table: "Match");

            migrationBuilder.DropForeignKey(
                name: "FK_Match_MatchResult_TeamTwoId",
                schema: "tp",
                table: "Match");

            migrationBuilder.DropTable(
                name: "PlayAreaBooking",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "PlayArea",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "MatchResult",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "Match",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "Team",
                schema: "tp");

            migrationBuilder.DropTable(
                name: "Tournament",
                schema: "tp");
        }
    }
}
