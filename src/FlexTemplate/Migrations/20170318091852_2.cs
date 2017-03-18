using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexTemplate.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContainerTemplates_Pages_PageId",
                table: "PageContainerTemplates");

            migrationBuilder.AddColumn<int>(
                name: "PageId",
                table: "Containers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "PageContainerTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_PageId",
                table: "Containers",
                column: "PageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Pages_PageId",
                table: "Containers",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageContainerTemplates_Pages_PageId",
                table: "PageContainerTemplates",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Pages_PageId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_PageContainerTemplates_Pages_PageId",
                table: "PageContainerTemplates");

            migrationBuilder.DropIndex(
                name: "IX_Containers_PageId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Containers");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "PageContainerTemplates",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_PageContainerTemplates_Pages_PageId",
                table: "PageContainerTemplates",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
