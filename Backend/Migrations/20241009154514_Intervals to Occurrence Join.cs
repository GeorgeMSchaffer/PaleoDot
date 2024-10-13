using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class IntervalstoOccurrenceJoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "accepted_name",
                schema: "paleo",
                table: "occurrences",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "accepted_no",
                schema: "paleo",
                table: "occurrences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "accepted_rank",
                schema: "paleo",
                table: "occurrences",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "composition",
                schema: "paleo",
                table: "occurrences",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "identified_no",
                schema: "paleo",
                table: "occurrences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "identified_rank",
                schema: "paleo",
                table: "occurrences",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "record_type",
                schema: "paleo",
                table: "occurrences",
                type: "longtext",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "parent_no",
                schema: "paleo",
                table: "intervals",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "min_ma",
                schema: "paleo",
                table: "intervals",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "max_ma",
                schema: "paleo",
                table: "intervals",
                type: "int",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_occurrences_early_interval_no",
                schema: "paleo",
                table: "occurrences",
                column: "early_interval_no");

            migrationBuilder.AddForeignKey(
                name: "FK_occurrences_intervals_early_interval_no",
                schema: "paleo",
                table: "occurrences",
                column: "early_interval_no",
                principalSchema: "paleo",
                principalTable: "intervals",
                principalColumn: "interval_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_occurrences_intervals_early_interval_no",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropIndex(
                name: "IX_occurrences_early_interval_no",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropColumn(
                name: "accepted_name",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropColumn(
                name: "accepted_no",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropColumn(
                name: "accepted_rank",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropColumn(
                name: "composition",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropColumn(
                name: "identified_no",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropColumn(
                name: "identified_rank",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.DropColumn(
                name: "record_type",
                schema: "paleo",
                table: "occurrences");

            migrationBuilder.AlterColumn<int>(
                name: "parent_no",
                schema: "paleo",
                table: "intervals",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "min_ma",
                schema: "paleo",
                table: "intervals",
                type: "double",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "max_ma",
                schema: "paleo",
                table: "intervals",
                type: "double",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
