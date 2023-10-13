using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduMapBackendProject.Migrations
{
    /// <inheritdoc />
    public partial class FooterSetting_Icon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FooterSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FooterSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IconClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FooterSettingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Icons_FooterSettings_FooterSettingId",
                        column: x => x.FooterSettingId,
                        principalTable: "FooterSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Icons_FooterSettingId",
                table: "Icons",
                column: "FooterSettingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropTable(
                name: "FooterSettings");
        }
    }
}
