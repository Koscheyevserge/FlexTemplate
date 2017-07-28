using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexTemplate.PresentationLayer.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPhotos_Blogs_BlogId",
                table: "BlogPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceBannerPhotos_Places_PlaceId",
                table: "PlaceBannerPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceGalleryPhotos_Places_PlaceId",
                table: "PlaceGalleryPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceHeaderPhotos_Places_PlaceId",
                table: "PlaceHeaderPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserPhotos",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "PlaceHeaderPhotos",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "PlaceGalleryPhotos",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "PlaceBannerPhotos",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "BlogPhotos",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPhotos_Blogs_BlogId",
                table: "BlogPhotos",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceBannerPhotos_Places_PlaceId",
                table: "PlaceBannerPhotos",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceGalleryPhotos_Places_PlaceId",
                table: "PlaceGalleryPhotos",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceHeaderPhotos_Places_PlaceId",
                table: "PlaceHeaderPhotos",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPhotos_Blogs_BlogId",
                table: "BlogPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceBannerPhotos_Places_PlaceId",
                table: "PlaceBannerPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceGalleryPhotos_Places_PlaceId",
                table: "PlaceGalleryPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceHeaderPhotos_Places_PlaceId",
                table: "PlaceHeaderPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "UserPhotos",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductPhotos",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "PlaceHeaderPhotos",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "PlaceGalleryPhotos",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "PlaceBannerPhotos",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "BlogPhotos",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPhotos_Blogs_BlogId",
                table: "BlogPhotos",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceBannerPhotos_Places_PlaceId",
                table: "PlaceBannerPhotos",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceGalleryPhotos_Places_PlaceId",
                table: "PlaceGalleryPhotos",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceHeaderPhotos_Places_PlaceId",
                table: "PlaceHeaderPhotos",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPhotos_Products_ProductId",
                table: "ProductPhotos",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
