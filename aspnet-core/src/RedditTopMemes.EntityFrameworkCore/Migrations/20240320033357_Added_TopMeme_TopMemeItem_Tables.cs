using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditTopMemes.Migrations
{
    /// <inheritdoc />
    public partial class Added_TopMeme_TopMemeItem_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetNotifiers",
                table: "AbpNotificationSubscriptions",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TopMemes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopMemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TopMemeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopMemeId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Score = table.Column<int>(type: "int", nullable: false),
                    NumComments = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopMemeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopMemeItems_TopMemes_TopMemeId",
                        column: x => x.TopMemeId,
                        principalTable: "TopMemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopMemeItems_TopMemeId",
                table: "TopMemeItems",
                column: "TopMemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopMemeItems");

            migrationBuilder.DropTable(
                name: "TopMemes");

            migrationBuilder.DropColumn(
                name: "TargetNotifiers",
                table: "AbpNotificationSubscriptions");
        }
    }
}
