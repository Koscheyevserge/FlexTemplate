using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FlexTemplate.Database;

namespace FlexTemplate.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlexTemplate.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FlexTemplate.Entities.CategoryAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CategoryAliases");
                });

            modelBuilder.Entity("FlexTemplate.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("FlexTemplate.Entities.CityAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CityAliases");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FlexTemplate.Entities.CountryAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CountryAliases");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("ShortName")
                        .HasAnnotation("MaxLength", 2);

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("FlexTemplate.Entities.PageLocalizableString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LanguageId");

                    b.Property<int>("PageId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PageId");

                    b.ToTable("PageLocalizableStrings");
                });

            modelBuilder.Entity("FlexTemplate.Entities.PagePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EntityId");

                    b.Property<string>("EntityName");

                    b.Property<string>("Name");

                    b.Property<int>("PageId");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.ToTable("PagePhotos");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("StreetId");

                    b.HasKey("Id");

                    b.HasIndex("StreetId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlaceAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LanguageId");

                    b.Property<int>("PlaceId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceAliases");
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlaceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<int>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceCategories");
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlacePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EntityId");

                    b.Property<string>("EntityName");

                    b.Property<string>("Name");

                    b.Property<int>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlacePhotos");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("FlexTemplate.Entities.StreetAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LanguageId");

                    b.Property<int>("StreetId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("StreetId");

                    b.ToTable("StreetAliases");
                });

            modelBuilder.Entity("FlexTemplate.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EncryptedPassword");

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Surname");

                    b.Property<int>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("UserRoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlexTemplate.Entities.UserKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("Key");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserKeys");
                });

            modelBuilder.Entity("FlexTemplate.Entities.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("FlexTemplate.Entities.UserRoleAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("LanguageId");

                    b.Property<string>("Text");

                    b.Property<int>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("UserRoleId");

                    b.ToTable("UserRoleAliases");
                });

            modelBuilder.Entity("FlexTemplate.Entities.CategoryAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Category", "Category")
                        .WithMany("CategoryAliases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.City", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.CityAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.City", "City")
                        .WithMany("CityAliases")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.CountryAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Country", "Country")
                        .WithMany("CountryAliases")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.PageLocalizableString", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Page", "Page")
                        .WithMany("LocalizableStrings")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.PagePhoto", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Page", "Page")
                        .WithMany("Photos")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.Place", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Street", "Street")
                        .WithMany()
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlaceAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Place", "Place")
                        .WithMany("PlaceAliases")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlaceCategory", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Category", "Category")
                        .WithMany("PlaceCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Place", "Place")
                        .WithMany("PlaceCategories")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlacePhoto", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Place", "Place")
                        .WithMany("PlacePhotos")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.Street", b =>
                {
                    b.HasOne("FlexTemplate.Entities.City", "City")
                        .WithMany("Streets")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.StreetAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Street", "Street")
                        .WithMany("StreetAliases")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.User", b =>
                {
                    b.HasOne("FlexTemplate.Entities.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.UserKey", b =>
                {
                    b.HasOne("FlexTemplate.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.UserRoleAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.UserRole", "UserRole")
                        .WithMany("Aliases")
                        .HasForeignKey("UserRoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
