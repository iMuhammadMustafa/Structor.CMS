using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PostsService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostNullableAuthorName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Author",
                table: "Posts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
