using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTrello.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    BoardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.BoardId);
                });

            migrationBuilder.CreateTable(
                name: "TaskLists",
                columns: table => new
                {
                    TaskListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    BoardId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TaskListId);
                    table.ForeignKey(
                        name: "TaskList_to_Board_FK",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "BoardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TaskListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Info = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.TaskId);
                    table.ForeignKey(
                        name: "Task_to_TaskList_FK",
                        column: x => x.TaskListId,
                        principalTable: "TaskLists",
                        principalColumn: "TaskListId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_Name",
                table: "Boards",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "TaskList_to_Board_FK_idx",
                table: "TaskLists",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "Unique_TaskList_in_Board",
                table: "TaskLists",
                columns: new[] { "BoardId", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Task_to_TaskList_FK_idx",
                table: "Tasks",
                column: "TaskListId");

            migrationBuilder.CreateIndex(
                name: "Unique_Task_in_TaskList",
                table: "Tasks",
                columns: new[] { "Info", "TaskListId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskLists");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
