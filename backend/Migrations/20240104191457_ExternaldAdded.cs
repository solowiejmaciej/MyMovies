using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyMovies.Migrations
{
    /// <inheritdoc />
    public partial class ExternaldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ThirdPartyId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThirdPartyId",
                table: "Movies");
        }
    }
}
