using Microsoft.EntityFrameworkCore.Migrations;

namespace WardRobe.Data.Migrations
{
    public partial class models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BottomFile",
                table: "MixnMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BottomUrl",
                table: "MixnMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopFile",
                table: "MixnMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopUrl",
                table: "MixnMatch",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Calendar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Calendar",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Backpack",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Backpack",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BottomFile",
                table: "MixnMatch");

            migrationBuilder.DropColumn(
                name: "BottomUrl",
                table: "MixnMatch");

            migrationBuilder.DropColumn(
                name: "TopFile",
                table: "MixnMatch");

            migrationBuilder.DropColumn(
                name: "TopUrl",
                table: "MixnMatch");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Calendar");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Backpack");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Backpack");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
