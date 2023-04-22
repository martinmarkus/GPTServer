using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPTServer.Common.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admin_keys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Key = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admin_keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    LogLevel = table.Column<int>(type: "int", nullable: false),
                    ClientIP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExecutorId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UniqueId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false),
                    LastAuthRoutingEnv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasExtensionPermission = table.Column<bool>(type: "bit", nullable: false),
                    LastAuthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "api_keys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    KeyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_keys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_api_keys_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_ips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_ips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_client_ips_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gpt_interactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    Success = table.Column<bool>(type: "bit", nullable: false),
                    ResponseMs = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gpt_interactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_gpt_interactions_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_api_keys_UserId",
                table: "api_keys",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_client_ips_UserId",
                table: "client_ips",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_gpt_interactions_UserId",
                table: "gpt_interactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email_IsDeleted",
                table: "users",
                columns: new[] { "Email", "IsDeleted" })
                .Annotation("SqlServer:Clustered", false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_keys");

            migrationBuilder.DropTable(
                name: "api_keys");

            migrationBuilder.DropTable(
                name: "client_ips");

            migrationBuilder.DropTable(
                name: "gpt_interactions");

            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
