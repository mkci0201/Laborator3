using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator3.Migrations
{
    public partial class ToDoTaskAndCommentClassUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_toDoTasks_ToDoTaskId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ToDoTaskId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_toDoTasks_ToDoTaskId",
                table: "Comments",
                column: "ToDoTaskId",
                principalTable: "toDoTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_toDoTasks_ToDoTaskId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "ToDoTaskId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_toDoTasks_ToDoTaskId",
                table: "Comments",
                column: "ToDoTaskId",
                principalTable: "toDoTasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
