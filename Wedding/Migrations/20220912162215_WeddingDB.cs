using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wedding.Migrations
{
    public partial class WeddingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extras",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("extra_peers_table_uuid_primary", x => x.Uuid);
                });

            migrationBuilder.CreateTable(
                name: "Redeems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redeems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guests",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    User = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("invited_table_uuid_primary", x => x.Uuid);
                    table.ForeignKey(
                        name: "invited_table_code_foreign",
                        column: x => x.Code,
                        principalTable: "Redeems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Kids",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kids_table_qr_codes_table",
                        column: x => x.Id,
                        principalTable: "Redeems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Peers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_peers_table_qr_codes_table",
                        column: x => x.Id,
                        principalTable: "Redeems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Assistances",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("assitance_table_uuid_primary", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_assitance_table_extra_peers_table",
                        column: x => x.Uuid,
                        principalTable: "Extras",
                        principalColumn: "Uuid");
                    table.ForeignKey(
                        name: "FK_assitance_table_invited_table",
                        column: x => x.Uuid,
                        principalTable: "Guests",
                        principalColumn: "Uuid");
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("attendance_table_uuid_primary", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_attendance_table_extra_peers_table",
                        column: x => x.Uuid,
                        principalTable: "Extras",
                        principalColumn: "Uuid");
                    table.ForeignKey(
                        name: "FK_attendance_table_invited_table",
                        column: x => x.Uuid,
                        principalTable: "Guests",
                        principalColumn: "Uuid");
                });

            migrationBuilder.CreateTable(
                name: "Deserts",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("desert_table_uuid_primary", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_desert_table_extra_peers_table",
                        column: x => x.Uuid,
                        principalTable: "Extras",
                        principalColumn: "Uuid");
                    table.ForeignKey(
                        name: "FK_desert_table_invited_table",
                        column: x => x.Uuid,
                        principalTable: "Guests",
                        principalColumn: "Uuid");
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Uuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("food_table_uuid_primary", x => x.Uuid);
                    table.ForeignKey(
                        name: "FK_food_table_extra_peers_table",
                        column: x => x.Uuid,
                        principalTable: "Extras",
                        principalColumn: "Uuid");
                    table.ForeignKey(
                        name: "FK_food_table_invited_table",
                        column: x => x.Uuid,
                        principalTable: "Guests",
                        principalColumn: "Uuid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Guests_Code",
                table: "Guests",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "qr_codes_table_id_index",
                table: "Redeems",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assistances");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "Deserts");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Kids");

            migrationBuilder.DropTable(
                name: "Peers");

            migrationBuilder.DropTable(
                name: "Extras");

            migrationBuilder.DropTable(
                name: "Guests");

            migrationBuilder.DropTable(
                name: "Redeems");
        }
    }
}
