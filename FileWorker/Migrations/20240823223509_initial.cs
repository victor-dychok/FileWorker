using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileWorker.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    LatinLetters = table.Column<string>(type: "VARCHAR(10)", maxLength: 10, nullable: false),
                    KirilicLetters = table.Column<string>(type: "NVARCHAR(10)", maxLength: 10, nullable: false),
                    IntegerNumber = table.Column<int>(type: "int", nullable: false),
                    RealNumber = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatisticsResults",
                columns: table => new
                {
                    IntegerSum = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealMedian = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticsResults", x => x.IntegerSum);
                });

            migrationBuilder.Sql(@"
            CREATE PROCEDURE CalculateStatistics
            AS
            BEGIN
                -- Считаем сумму целых чисел с использованием BIGINT для предотвращения переполнения
                DECLARE @IntegerSum BIGINT;
                SELECT @IntegerSum = SUM(CAST(IntegerNumber AS BIGINT)) FROM Lines;
            
                -- Считаем медиану дробных чисел с использованием PERCENTILE_CONT
                DECLARE @RealMedian FLOAT;
                SELECT @RealMedian = PERCENTILE_CONT(0.5) 
                                     WITHIN GROUP (ORDER BY RealNumber) 
                                     OVER() 
                FROM Lines
                WHERE RealNumber IS NOT NULL;
            
                -- Возвращаем результаты
                SELECT @IntegerSum AS IntegerSum, @RealMedian AS RealMedian;
            END;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "StatisticsResults");
        }
    }
}
