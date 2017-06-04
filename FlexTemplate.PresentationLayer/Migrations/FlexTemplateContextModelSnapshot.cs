using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FlexTemplate.PresentationLayer.Core;

namespace FlexTemplate.PresentationLayer.Migrations
{
    [DbContext(typeof(FlexTemplateContext))]
    partial class FlexTemplateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.AvailableContainerForContainer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PageId");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("PageId");

                    b.ToTable("AvailableContainerForContainers");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.AvailableContainerForPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PageId");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("PageId");

                    b.ToTable("AvailableContainersForPages");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsModerated");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogBlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogCategoryId");

                    b.Property<int>("BlogId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.HasKey("Id");

                    b.HasIndex("BlogCategoryId");

                    b.HasIndex("BlogId");

                    b.ToTable("BlogBlogCategories");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BlogCategories");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogCategoryAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogCategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("BlogCategoryId");

                    b.HasIndex("LanguageId");

                    b.ToTable("BlogCategoryAliases");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorId");

                    b.Property<int>("BlogId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("BlogId");

                    b.ToTable("BlogComments");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BlogId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("TagId");

                    b.ToTable("BlogTags");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.CityAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CityAliases");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.CommunicationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("CommunicationType");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Container", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Containers");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.ContainerLocalizableString", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Tag");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.HasIndex("LanguageId");

                    b.ToTable("ContainerLocalizableStrings");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.ContainerTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("TemplateName");

                    b.HasKey("Id");

                    b.HasIndex("ContainerId");

                    b.ToTable("ContainerTemplates");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.CountryAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("LanguageId");

                    b.ToTable("CountryAliases");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDefault");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("ShortName")
                        .HasMaxLength(2);

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PageContainerTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContainerTemplateId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PageId");

                    b.Property<int>("ParentId");

                    b.Property<int>("Position");

                    b.HasKey("Id");

                    b.HasIndex("ContainerTemplateId");

                    b.HasIndex("PageId");

                    b.ToTable("PageContainerTemplates");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Place", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("StreetId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("StreetId");

                    b.HasIndex("UserId");

                    b.ToTable("Places");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PlaceId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceAliases");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PlaceCategories");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceCategoryAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PlaceCategoryId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PlaceCategoryId");

                    b.ToTable("PlaceCategoryAliases");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceCommunication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CommunicationTypeId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Number");

                    b.Property<int>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("CommunicationTypeId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceCommunication");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceFeature", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.Property<int>("PlaceFeatureColumnId");

                    b.Property<int>("Row");

                    b.HasKey("Id");

                    b.HasIndex("PlaceFeatureColumnId");

                    b.ToTable("PlaceFeatures");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceFeatureColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlaceFeatureColumn");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlacePlaceCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PlaceCategoryId");

                    b.Property<int>("PlaceId");

                    b.HasKey("Id");

                    b.HasIndex("PlaceCategoryId");

                    b.HasIndex("PlaceId");

                    b.ToTable("PlacePlaceCategories");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("PlaceId");

                    b.Property<int>("Star");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PlaceId");

                    b.HasIndex("UserId");

                    b.ToTable("PlaceReviews");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<TimeSpan>("FridayFrom");

                    b.Property<TimeSpan>("FridayTo");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<TimeSpan>("MondayFrom");

                    b.Property<TimeSpan>("MondayTo");

                    b.Property<int>("PlaceId");

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

                    b.HasIndex("PlaceId")
                        .IsUnique();

                    b.ToTable("PlaceSchedules");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<int>("MenuId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<double>("Price");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("MenuId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("BoolValue");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("IntValue");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("StringValue");

                    b.HasKey("Id");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Street", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CityId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.StreetAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("StreetId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("StreetId");

                    b.ToTable("StreetAliases");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.TagAlias", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedOn");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime>("ModifiedOn");

                    b.Property<int>("TagId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("TagId");

                    b.ToTable("TagAliases");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

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
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
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

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.AvailableContainerForContainer", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Container", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.AvailableContainerForPage", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Container", "Container")
                        .WithMany()
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Page", "Page")
                        .WithMany()
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Blog", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.User", "User")
                        .WithMany("Blogs")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogBlogCategory", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.BlogCategory", "BlogCategory")
                        .WithMany("BlogBlogCategories")
                        .HasForeignKey("BlogCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Blog", "Blog")
                        .WithMany("BlogBlogCategories")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogCategoryAlias", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.BlogCategory", "BlogCategory")
                        .WithMany("Aliases")
                        .HasForeignKey("BlogCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogComment", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Blog", "Blog")
                        .WithMany("Comments")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.BlogTag", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Blog", "Blog")
                        .WithMany("BlogTags")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Tag", "Tag")
                        .WithMany("BlogTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.City", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.CityAlias", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.City", "City")
                        .WithMany("Aliases")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.ContainerLocalizableString", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Container", "Container")
                        .WithMany("LocalizableStrings")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.ContainerTemplate", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Container", "Container")
                        .WithMany("ContainerTemplates")
                        .HasForeignKey("ContainerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.CountryAlias", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Country", "Country")
                        .WithMany("Aliases")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Menu", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Place", "Place")
                        .WithMany("Menus")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PageContainerTemplate", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.ContainerTemplate", "ContainerTemplate")
                        .WithMany("PageContainerTemplates")
                        .HasForeignKey("ContainerTemplateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Page", "Page")
                        .WithMany("Containers")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Place", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Street", "Street")
                        .WithMany()
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.User", "User")
                        .WithMany("Places")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceAlias", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Place", "Place")
                        .WithMany("Aliases")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceCategoryAlias", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.PlaceCategory", "PlaceCategory")
                        .WithMany("Aliases")
                        .HasForeignKey("PlaceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceCommunication", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.CommunicationType", "CommunicationType")
                        .WithMany()
                        .HasForeignKey("CommunicationTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Place", "Place")
                        .WithMany("Communications")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceFeature", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.PlaceFeatureColumn", "Column")
                        .WithMany("Features")
                        .HasForeignKey("PlaceFeatureColumnId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceFeatureColumn", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Place", "Place")
                        .WithMany("FeatureColumns")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlacePlaceCategory", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.PlaceCategory", "PlaceCategory")
                        .WithMany("PlacePlaceCategories")
                        .HasForeignKey("PlaceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Place", "Place")
                        .WithMany("PlacePlaceCategories")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceReview", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Place", "Place")
                        .WithMany("Reviews")
                        .HasForeignKey("PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.PlaceSchedule", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Place", "Place")
                        .WithOne("Schedule")
                        .HasForeignKey("FlexTemplate.DataAccessLayer.Entities.PlaceSchedule", "PlaceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Product", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Menu", "Menu")
                        .WithMany("Products")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.Street", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.City", "City")
                        .WithMany("Streets")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.StreetAlias", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Street", "Street")
                        .WithMany("Aliases")
                        .HasForeignKey("StreetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FlexTemplate.DataAccessLayer.Entities.TagAlias", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.Tag", "Tag")
                        .WithMany("TagAliases")
                        .HasForeignKey("TagId")
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
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.User")
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

                    b.HasOne("FlexTemplate.DataAccessLayer.Entities.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
