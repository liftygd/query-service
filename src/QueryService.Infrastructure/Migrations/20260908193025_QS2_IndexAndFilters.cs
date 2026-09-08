using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QS2_IndexAndFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "query_user_statistics",
                type: "uuid",
                nullable: true,
                comment: "Идентификатор пользователя.",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор пользователя.");

            migrationBuilder.AlterColumn<int>(
                name: "sign_in_count",
                table: "query_user_statistics",
                type: "integer",
                nullable: true,
                comment: "Количество входов.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldComment: "Количество входов.");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_from",
                table: "query_user_statistics",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Фильтр по дате - Дата От.");

            migrationBuilder.AddColumn<DateTime>(
                name: "date_to",
                table: "query_user_statistics",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Фильтр по дате - Дата По.");

            migrationBuilder.AddColumn<DateTime>(
                name: "completed_time",
                table: "query",
                type: "timestamp with time zone",
                nullable: true,
                comment: "Время, когда запрос завершился.");

            migrationBuilder.CreateIndex(
                name: "IX_query_query_state",
                table: "query",
                column: "query_state");

            migrationBuilder.CreateIndex(
                name: "IX_query_query_type",
                table: "query",
                column: "query_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_query_query_state",
                table: "query");

            migrationBuilder.DropIndex(
                name: "IX_query_query_type",
                table: "query");

            migrationBuilder.DropColumn(
                name: "date_from",
                table: "query_user_statistics");

            migrationBuilder.DropColumn(
                name: "date_to",
                table: "query_user_statistics");

            migrationBuilder.DropColumn(
                name: "completed_time",
                table: "query");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "query_user_statistics",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Идентификатор пользователя.",
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true,
                oldComment: "Идентификатор пользователя.");

            migrationBuilder.AlterColumn<int>(
                name: "sign_in_count",
                table: "query_user_statistics",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                comment: "Количество входов.",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldComment: "Количество входов.");
        }
    }
}
