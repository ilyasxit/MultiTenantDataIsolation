using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSite.Core.Migrations
{
    public partial class MainDb_InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recruiter",
                columns: table => new
                {
                    RecruiterId = table.Column<Guid>(nullable: false),
                    RecruiterName = table.Column<string>(maxLength: 100, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruiters", x => x.RecruiterId);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SiteName = table.Column<string>(maxLength: 255, nullable: false),
                    ThemeName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteId);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobId = table.Column<Guid>(nullable: false),
                    JobTitle = table.Column<string>(maxLength: 100, nullable: false),
                    RecruiterId = table.Column<Guid>(nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_Recruiters_Jobs",
                        column: x => x.RecruiterId,
                        principalTable: "Recruiter",
                        principalColumn: "RecruiterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "SiteId", "SiteName", "ThemeName" },
                values: new object[] { 1, "InterNurse", "internurse" });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "SiteId", "SiteName", "ThemeName" },
                values: new object[] { 2, "UkVets", "ukvets" });

            migrationBuilder.InsertData(
                table: "Sites",
                columns: new[] { "SiteId", "SiteName", "ThemeName" },
                values: new object[] { 3, "RhineGold", "rhinegold" });

            migrationBuilder.CreateIndex(
                name: "IX_Job_RecruiterId",
                table: "Job",
                column: "RecruiterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Recruiter");
        }
    }
}
