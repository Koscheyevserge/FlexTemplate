using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FlexTemplate.PresentationLayer.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceBannerPhoto_Places_PlaceId",
                table: "PlaceBannerPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceGalleryPhoto_Places_PlaceId",
                table: "PlaceGalleryPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceHeaderPhoto_Places_PlaceId",
                table: "PlaceHeaderPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceHeaderPhoto",
                table: "PlaceHeaderPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceGalleryPhoto",
                table: "PlaceGalleryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceBannerPhoto",
                table: "PlaceBannerPhoto");

            migrationBuilder.DropColumn(
                name: "BlobBannersKey",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "BlobGalleryKey",
                table: "Places");

            migrationBuilder.RenameTable(
                name: "PlaceHeaderPhoto",
                newName: "PlaceHeaderPhotos");

            migrationBuilder.RenameTable(
                name: "PlaceGalleryPhoto",
                newName: "PlaceGalleryPhotos");

            migrationBuilder.RenameTable(
                name: "PlaceBannerPhoto",
                newName: "PlaceBannerPhotos");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceHeaderPhoto_PlaceId",
                table: "PlaceHeaderPhotos",
                newName: "IX_PlaceHeaderPhotos_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceGalleryPhoto_PlaceId",
                table: "PlaceGalleryPhotos",
                newName: "IX_PlaceGalleryPhotos_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceBannerPhoto_PlaceId",
                table: "PlaceBannerPhotos",
                newName: "IX_PlaceBannerPhotos_PlaceId");

            migrationBuilder.RenameColumn(
                name: "BlobHeadersKey",
                table: "Places",
                newName: "BlobKey");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceHeaderPhotos",
                table: "PlaceHeaderPhotos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceGalleryPhotos",
                table: "PlaceGalleryPhotos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceBannerPhotos",
                table: "PlaceBannerPhotos",
                column: "Id");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlaceBannerPhotos_Places_PlaceId",
                table: "PlaceBannerPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceGalleryPhotos_Places_PlaceId",
                table: "PlaceGalleryPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaceHeaderPhotos_Places_PlaceId",
                table: "PlaceHeaderPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceHeaderPhotos",
                table: "PlaceHeaderPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceGalleryPhotos",
                table: "PlaceGalleryPhotos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaceBannerPhotos",
                table: "PlaceBannerPhotos");

            migrationBuilder.RenameTable(
                name: "PlaceHeaderPhotos",
                newName: "PlaceHeaderPhoto");

            migrationBuilder.RenameTable(
                name: "PlaceGalleryPhotos",
                newName: "PlaceGalleryPhoto");

            migrationBuilder.RenameTable(
                name: "PlaceBannerPhotos",
                newName: "PlaceBannerPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceHeaderPhotos_PlaceId",
                table: "PlaceHeaderPhoto",
                newName: "IX_PlaceHeaderPhoto_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceGalleryPhotos_PlaceId",
                table: "PlaceGalleryPhoto",
                newName: "IX_PlaceGalleryPhoto_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaceBannerPhotos_PlaceId",
                table: "PlaceBannerPhoto",
                newName: "IX_PlaceBannerPhoto_PlaceId");

            migrationBuilder.RenameColumn(
                name: "BlobKey",
                table: "Places",
                newName: "BlobHeadersKey");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceHeaderPhoto",
                table: "PlaceHeaderPhoto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceGalleryPhoto",
                table: "PlaceGalleryPhoto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaceBannerPhoto",
                table: "PlaceBannerPhoto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceBannerPhoto_Places_PlaceId",
                table: "PlaceBannerPhoto",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceGalleryPhoto_Places_PlaceId",
                table: "PlaceGalleryPhoto",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaceHeaderPhoto_Places_PlaceId",
                table: "PlaceHeaderPhoto",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
