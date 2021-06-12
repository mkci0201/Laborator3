using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator3.Migrations
{
    public partial class AssignmentInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "toDoTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_toDoTasks_AssignmentId",
                table: "toDoTasks",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_userId",
                table: "Assignments",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_toDoTasks_Assignments_AssignmentId",
                table: "toDoTasks",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_toDoTasks_Assignments_AssignmentId",
                table: "toDoTasks");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_toDoTasks_AssignmentId",
                table: "toDoTasks");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "toDoTasks");
        }
    }
}
