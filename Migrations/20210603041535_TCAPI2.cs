using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ThanksCardAPI.Migrations
{
    public partial class TCAPI2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_ParentId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ParentId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Departments");

            migrationBuilder.AddColumn<long>(
                name: "DepartmentChildrenId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DepartmentChildrens",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentChildrens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentChildrens_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentChildrenId",
                table: "Users",
                column: "DepartmentChildrenId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentChildrens_DepartmentId",
                table: "DepartmentChildrens",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_DepartmentChildrens_DepartmentChildrenId",
                table: "Users",
                column: "DepartmentChildrenId",
                principalTable: "DepartmentChildrens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_DepartmentChildrens_DepartmentChildrenId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "DepartmentChildrens");

            migrationBuilder.DropIndex(
                name: "IX_Users_DepartmentChildrenId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DepartmentChildrenId",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "DepartmentId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Departments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ParentId",
                table: "Departments",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_ParentId",
                table: "Departments",
                column: "ParentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Departments_DepartmentId",
                table: "Users",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
