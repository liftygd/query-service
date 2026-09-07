using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Domain.Enums;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class QS1_CreatedInitialEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:query_state", "completed,failed,not_set,pending")
                .Annotation("Npgsql:Enum:query_type", "not_set,user_statistics");

            migrationBuilder.CreateTable(
                name: "query",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор сущности"),
                    query_type = table.Column<QueryType>(type: "query_type", nullable: false, comment: "Тип запроса."),
                    query_state = table.Column<QueryState>(type: "query_state", nullable: false, comment: "Состояние запроса."),
                    message = table.Column<string>(type: "text", nullable: true, comment: "Сообщение от запроса."),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата создания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_query", x => x.id);
                },
                comment: "Таблица с текущими запросами.");

            migrationBuilder.CreateTable(
                name: "user_sign_in",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор сущности")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя."),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата создания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_sign_in", x => x.id);
                },
                comment: "Данные по авторизации пользователей.");

            migrationBuilder.CreateTable(
                name: "query_user_statistics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор сущности"),
                    query_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор запроса."),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя."),
                    sign_in_count = table.Column<int>(type: "integer", nullable: false, comment: "Количество входов."),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата создания")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_query_user_statistics", x => x.id);
                    table.ForeignKey(
                        name: "FK_query_user_statistics_query_query_id",
                        column: x => x.query_id,
                        principalTable: "query",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Данные для запросов по статистике пользователей.");

            migrationBuilder.CreateIndex(
                name: "IX_query_user_statistics_query_id",
                table: "query_user_statistics",
                column: "query_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "query_user_statistics");

            migrationBuilder.DropTable(
                name: "user_sign_in");

            migrationBuilder.DropTable(
                name: "query");
        }
    }
}
