using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexTemplate.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.DataAccessLayer
{
    public abstract class FlexContext : IdentityDbContext<User>
    {
        protected string ConnectionString { get; set; } =
            "Server=(localdb)\\mssqllocaldb;Database=FlexTemplateDB;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<PlaceCategory> PlaceCategories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<PlaceCategoryAlias> PlaceCategoryAliases { get; set; }
        public DbSet<BlogCategoryAlias> BlogCategoryAliases { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceAlias> PlaceAliases { get; set; }
        public DbSet<BlogBlogCategory> BlogBlogCategories { get; set; }
        public DbSet<PlacePlaceCategory> PlacePlaceCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityAlias> CityAliases { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryAlias> CountryAliases { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<StreetAlias> StreetAliases { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<ContainerLocalizableString> ContainerLocalizableStrings { get; set; }
        public DbSet<ContainerTemplate> ContainerTemplates { get; set; }
        public DbSet<PlaceReview> PlaceReviews { get; set; }
        public DbSet<PageContainerTemplate> PageContainerTemplates { get; set; }
        public DbSet<AvailableContainerForPage> AvailableContainersForPages { get; set; }
        public DbSet<AvailableContainerForContainer> AvailableContainerForContainers { get; set; }
        public DbSet<PlaceSchedule> PlaceSchedules { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PlaceFeature> PlaceFeatures { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<TagAlias> TagAliases { get; set; }
        public DbSet<Setting> Settings { get; set; }

        public FlexContext(DbContextOptions options) : base(options)
        {
            
        }

        public FlexContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<T> GetSet<T>() where T : class
        {
            return Set<T>();
        }
    }
}