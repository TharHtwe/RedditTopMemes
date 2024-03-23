using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditTopMemes.Migrations
{
    /// <inheritdoc />
    public partial class Added_Rank_To_TopMemeItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "TopMemeItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "TopMemeItems");
        }
    }
}
