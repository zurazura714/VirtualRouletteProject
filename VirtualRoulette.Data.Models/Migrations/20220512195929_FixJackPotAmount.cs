using Microsoft.EntityFrameworkCore.Migrations;

namespace VirtualRoulette.Data.Models.Migrations
{
    public partial class FixJackPotAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "JackPotAmount",
                table: "JackPots",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "JackPotAmount",
                table: "JackPots",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");
        }
    }
}
