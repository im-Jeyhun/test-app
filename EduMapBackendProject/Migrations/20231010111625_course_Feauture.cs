using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMapBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class course_Feauture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourseFeatures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Starts = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentsCount = table.Column<int>(type: "int", nullable: false),
                    Assesments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fee = table.Column<double>(type: "float", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseFeatures_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseFeatures_CourseId",
                table: "CourseFeatures",
                column: "CourseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseFeatures");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
