using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetricsAgent.Migrations
{
    /// <inheritdoc />
    public partial class InnitTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CpuMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Utilization = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    CurrentDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CpuMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GpuMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Utilization = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    CurrentDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GpuMetrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RamMetrics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Utilization = table.Column<double>(type: "float", maxLength: 100, nullable: false),
                    CurrentDate = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RamMetrics", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CpuMetrics");

            migrationBuilder.DropTable(
                name: "GpuMetrics");

            migrationBuilder.DropTable(
                name: "RamMetrics");
        }
    }
}
