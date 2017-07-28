using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexTemplate.PresentationLayer.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "UserPhotos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "ProductPhotos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "PlaceHeaderPhoto",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "PlaceGalleryPhoto",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "PlaceBannerPhoto",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobBannersKey",
                table: "Places",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobGalleryKey",
                table: "Places",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobHeadersKey",
                table: "Places",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "BlogPhotos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BlobKey",
                table: "Blogs",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "ProductPhotos");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "PlaceHeaderPhoto");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "PlaceGalleryPhoto");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "PlaceBannerPhoto");

            migrationBuilder.DropColumn(
                name: "BlobBannersKey",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "BlobGalleryKey",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "BlobHeadersKey",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "BlogPhotos");

            migrationBuilder.DropColumn(
                name: "BlobKey",
                table: "Blogs");
        }
    }
}
