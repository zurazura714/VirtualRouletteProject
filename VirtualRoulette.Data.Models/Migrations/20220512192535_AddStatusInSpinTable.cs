using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualRoulette.Data.Models.Migrations
{
    public partial class AddStatusInSpinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spins_AppUsers_AppUserID",
                table: "Spins");

            migrationBuilder.DropForeignKey(
                name: "FK_Spins_SessionTokens_SessionTokenID",
                table: "Spins");

            migrationBuilder.AlterColumn<int>(
                name: "SessionTokenID",
                table: "Spins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AppUserID",
                table: "Spins",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Spins",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Spins_AppUsers_AppUserID",
                table: "Spins",
                column: "AppUserID",
                principalTable: "AppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Spins_SessionTokens_SessionTokenID",
                table: "Spins",
                column: "SessionTokenID",
                principalTable: "SessionTokens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Spins_AppUsers_AppUserID",
                table: "Spins");

            migrationBuilder.DropForeignKey(
                name: "FK_Spins_SessionTokens_SessionTokenID",
                table: "Spins");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Spins");

            migrationBuilder.AlterColumn<int>(
                name: "SessionTokenID",
                table: "Spins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserID",
                table: "Spins",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Spins_AppUsers_AppUserID",
                table: "Spins",
                column: "AppUserID",
                principalTable: "AppUsers",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Spins_SessionTokens_SessionTokenID",
                table: "Spins",
                column: "SessionTokenID",
                principalTable: "SessionTokens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
