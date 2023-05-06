using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GPTServer.Common.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "CreationDate", "Email", "HasExtensionPermission", "IsDeleted", "LastAuthDate", "LastAuthRoutingEnv", "PasswordHash", "PasswordSalt", "UniqueId", "UserAgent" },
                values: new object[] { new Guid("21a2b3df-0553-4d66-91ec-90cfbb285dd0"), new DateTime(2023, 5, 6, 16, 46, 7, 402, DateTimeKind.Local).AddTicks(1648), "teszt@aichatmester.hu", true, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ChatGPTExtension", "M+QCWZJLLRFCKU0V/PUERW4F21H8V3DTTJTPDDXVLGA=", "SDA6HQ+8CYIJ+9OM23GM9KJDVGYOIP+TJ9SSAAN9TWM09PXVPINP/OL38JDIPRQQHAKXWONR1TESEM05XTRLPKRBY2QBSW/1IXIFMGWP91HPIQP0F2A1WHGQHTMCX10W", "c95aff8f-3fff-4b3e-a8f6-08dee0dc7c3a", "" });

            migrationBuilder.CreateIndex(
                name: "IX_api_keys_IsDeleted_Id",
                table: "api_keys",
                columns: new[] { "IsDeleted", "Id" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_api_keys_IsDeleted_UserId",
                table: "api_keys",
                columns: new[] { "IsDeleted", "UserId" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_api_keys_IsDeleted_UserId_IsActive",
                table: "api_keys",
                columns: new[] { "IsDeleted", "UserId", "IsActive" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_api_keys_IsDeleted_UserId_Key",
                table: "api_keys",
                columns: new[] { "IsDeleted", "UserId", "Key" })
                .Annotation("SqlServer:Clustered", false);

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
                name: "IX_users_IsDeleted_Email",
                table: "users",
                columns: new[] { "IsDeleted", "Email" })
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_users_IsDeleted_Id",
                table: "users",
                columns: new[] { "IsDeleted", "Id" })
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
