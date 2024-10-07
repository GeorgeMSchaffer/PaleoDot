using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "paleo");

            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "intervals",
                schema: "paleo",
                columns: table => new
                {
                    interval_no = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    interval_name = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true),
                    min_ma = table.Column<double>(type: "double", nullable: true),
                    max_ma = table.Column<double>(type: "double", nullable: true),
                    color = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    parent_no = table.Column<int>(type: "int", nullable: true),
                    record_type = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    reference_no = table.Column<int>(type: "int", nullable: true),
                    scale_no = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_intervals", x => x.interval_no);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "occurrences",
                schema: "paleo",
                columns: table => new
                {
                    occurrence_no = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    early_interval = table.Column<string>(type: "longtext", nullable: true),
                    early_interval_no = table.Column<int>(type: "int", nullable: true),
                    late_interval_no = table.Column<int>(type: "int", nullable: true),
                    max_ma = table.Column<double>(type: "double", nullable: true),
                    min_ma = table.Column<double>(type: "double", nullable: true),
                    phylum = table.Column<string>(type: "longtext", nullable: false),
                    @class = table.Column<string>(name: "class", type: "longtext", nullable: true),
                    order = table.Column<string>(type: "longtext", nullable: true),
                    family = table.Column<string>(type: "longtext", nullable: true),
                    genus = table.Column<string>(type: "longtext", nullable: true),
                    environment = table.Column<string>(type: "longtext", nullable: true),
                    paleoage = table.Column<string>(type: "longtext", nullable: true),
                    paleolng = table.Column<double>(type: "double", nullable: true),
                    paleolat = table.Column<double>(type: "double", nullable: true),
                    lng = table.Column<double>(type: "double", nullable: true),
                    lat = table.Column<double>(type: "double", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_occurrences", x => x.occurrence_no);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "intervals",
                schema: "paleo");

            migrationBuilder.DropTable(
                name: "occurrences",
                schema: "paleo");
        }
    }
}
