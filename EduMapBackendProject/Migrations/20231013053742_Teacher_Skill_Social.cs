using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMapBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class Teacher_Skill_Social : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Experience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faculty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<int>(type: "int", nullable: false),
                    TeamLeader = table.Column<int>(type: "int", nullable: false),
                    Development = table.Column<int>(type: "int", nullable: false),
                    Design = table.Column<int>(type: "int", nullable: false),
                    Innovation = table.Column<int>(type: "int", nullable: false),
                    Communication = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Socials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaceBook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pinterest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vimeo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Socials_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skills_TeacherId",
                table: "Skills",
                column: "TeacherId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Socials_TeacherId",
                table: "Socials",
                column: "TeacherId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Socials");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
