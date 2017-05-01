﻿using System;
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

            modelBuilder.Entity("FlexTemplate.Entities.AvailableContainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerId");

                    b.Property<int>("PageId");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("PageId");

                    b.ToTable("AvailableContainers");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<string>("Caption");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsModerated");

                    b.Property<string>("Preamble");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("FlexTemplate.Entities.BlogComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<int>("BlogId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogId");

                    b.ToTable("BlogComments");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

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

            modelBuilder.Entity("FlexTemplate.Entities.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("PanelId");

                    b.HasKey("Id");

                    b.HasIndex("PanelId");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("FlexTemplate.Entities.ContainerLocalizableString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerId");

                    b.Property<int>("LanguageId");

                    b.Property<string>("Tag");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ContainerLocalizableStrings");
                });

            modelBuilder.Entity("FlexTemplate.Entities.ContainerTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerId");

                    b.Property<string>("TemplateName");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.ToTable("ContainerTemplates");
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

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDefault");

                    b.Property<string>("Name");

                    b.Property<string>("ShortName")
                        .HasAnnotation("MaxLength", 2);

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BodyClasses");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("FlexTemplate.Entities.PageContainerTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerTemplateId");

                    b.Property<int>("PageId");

                    b.Property<int>("Position");

                    b.HasKey("Id");

                    b.HasIndex("ContainerTemplateId");

                    b.HasIndex("PageId");

                    b.ToTable("PageContainerTemplates");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Panel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Panel");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<int?>("ScheduleId");

                    b.Property<int>("StreetId");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId")
                        .IsUnique();

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

            modelBuilder.Entity("FlexTemplate.Entities.PlaceFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Column");

                    b.Property<string>("Name");

                    b.Property<int>("PlaceId");

                    b.Property<int>("Row");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceFeatures");
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlaceReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PlaceId");

                    b.Property<int>("Star");

                    b.Property<string>("Text");

                    b.Property<int>("UserId");

                    b.Property<string>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UserId1");

                    b.ToTable("PlaceReviews");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("MenuId");

                    b.Property<double>("Price");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<TimeSpan>("FridayFrom");

                    b.Property<TimeSpan>("FridayTo");

                    b.Property<TimeSpan>("MondayFrom");

                    b.Property<TimeSpan>("MondayTo");

                    b.Property<TimeSpan>("SaturdayFrom");

                    b.Property<TimeSpan>("SaturdayTo");

                    b.Property<TimeSpan>("SundayFrom");

                    b.Property<TimeSpan>("SundayTo");

                    b.Property<TimeSpan>("ThurstdayFrom");

                    b.Property<TimeSpan>("ThurstdayTo");

                    b.Property<TimeSpan>("TuesdayFrom");

                    b.Property<TimeSpan>("TuesdayTo");

                    b.Property<TimeSpan>("WednesdayFrom");

                    b.Property<TimeSpan>("WednesdayTo");

                    b.HasKey("Id");

                    b.ToTable("Schedules");
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
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FlexTemplate.Entities.AvailableContainer", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Container", "Container")
                        .WithMany("AvailableContainers")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Page", "Page")
                        .WithMany("AvailableContainers")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.Blog", b =>
                {
                    b.HasOne("FlexTemplate.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("FlexTemplate.Entities.BlogComment", b =>
                {
                    b.HasOne("FlexTemplate.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("FlexTemplate.Entities.Blog", "Blog")
                        .WithMany("Comments")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.CategoryAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Category", "Category")
                        .WithMany("Aliases")
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
                        .WithMany("Aliases")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.Container", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Panel", "Panel")
                        .WithMany("Containers")
                        .HasForeignKey("PanelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.ContainerLocalizableString", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Container", "Container")
                        .WithMany("LocalizableStrings")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.ContainerTemplate", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Container", "Container")
                        .WithMany("ContainerTemplates")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.CountryAlias", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Country", "Country")
                        .WithMany("Aliases")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.Menu", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Place", "Place")
                        .WithMany("Menus")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.PageContainerTemplate", b =>
                {
                    b.HasOne("FlexTemplate.Entities.ContainerTemplate", "ContainerTemplate")
                        .WithMany("PageContainerTemplates")
                        .HasForeignKey("ContainerTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.Page", "Page")
                        .WithMany("PageContainerTemplates")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.Place", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Schedule", "Schedule")
                        .WithOne("Place")
                        .HasForeignKey("FlexTemplate.Entities.Place", "ScheduleId");

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
                        .WithMany("Aliases")
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

            modelBuilder.Entity("FlexTemplate.Entities.PlaceFeature", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Place", "Place")
                        .WithMany("PlaceFeatures")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.Entities.PlaceReview", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Place", "Place")
                        .WithMany("Reviews")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("FlexTemplate.Entities.Product", b =>
                {
                    b.HasOne("FlexTemplate.Entities.Menu", "Menu")
                        .WithMany("Products")
                        .HasForeignKey("MenuId")
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
                        .WithMany("Aliases")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("FlexTemplate.Entities.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FlexTemplate.Entities.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.Entities.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
