using Microsoft.EntityFrameworkCore.Migrations;

namespace Laborator3.Migrations
{
    public partial class updatedtoDoTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_userId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_toDoTasks_Assignments_AssignmentId",
                table: "toDoTasks");

            migrationBuilder.DropIndex(
                name: "IX_toDoTasks_AssignmentId",
                table: "toDoTasks");

            migrationBuilder.DropColumn(
                name: "AssignmentId",
                table: "toDoTasks");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Assignments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_userId",
                table: "Assignments",
                newName: "IX_Assignments_UserId");

            migrationBuilder.CreateTable(
                name: "AssignmentToDoTask",
                columns: table => new
                {
                    AssignmentsId = table.Column<int>(type: "int", nullable: false),
                    ToDoTasksId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentToDoTask", x => new { x.AssignmentsId, x.ToDoTasksId });
                    table.ForeignKey(
                        name: "FK_AssignmentToDoTask_Assignments_AssignmentsId",
                        column: x => x.AssignmentsId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentToDoTask_toDoTasks_ToDoTasksId",
                        column: x => x.ToDoTasksId,
                        principalTable: "toDoTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentToDoTask_ToDoTasksId",
                table: "AssignmentToDoTask",
                column: "ToDoTasksId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_UserId",
                table: "Assignments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_AspNetUsers_UserId",
                table: "Assignments");

            migrationBuilder.DropTable(
                name: "AssignmentToDoTask");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Assignments",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_UserId",
                table: "Assignments",
                newName: "IX_Assignments_userId");

            migrationBuilder.AddColumn<int>(
                name: "AssignmentId",
                table: "toDoTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_toDoTasks_AssignmentId",
                table: "toDoTasks",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_AspNetUsers_userId",
                table: "Assignments",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_toDoTasks_Assignments_AssignmentId",
                table: "toDoTasks",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
