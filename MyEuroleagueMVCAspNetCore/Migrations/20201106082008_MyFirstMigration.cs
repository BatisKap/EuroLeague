using Microsoft.EntityFrameworkCore.Migrations;

namespace MyEuroleagueMVCAspNetCore.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Match",
                columns: table => new
                {
                    MatchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoundNo = table.Column<int>(nullable: false),
                    Home_Team = table.Column<string>(maxLength: 50, nullable: true),
                    away_Team = table.Column<string>(maxLength: 50, nullable: true),
                    HomePointsScored = table.Column<int>(nullable: false),
                    AwayPointsScored = table.Column<int>(nullable: false),
                    hadExtraTime = table.Column<bool>(nullable: false),
                    EndOfFourthPeriodPoints = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Match", x => x.MatchID);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    TeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    Coach = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.TeamID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Match");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
