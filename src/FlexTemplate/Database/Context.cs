using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.Database
{
    public class Context : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAlias> CategoryAliases { get; set; } 
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceAlias> PlaceAliases { get; set; }
        public DbSet<PlacePhoto> PlacePhotos { get; set; }
        public DbSet<PlaceCategory> PlaceCategories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityAlias> CityAliases { get; set; } 
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryAlias> CountryAliases { get; set; } 
        public DbSet<Language> Languages { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<StreetAlias> StreetAliases { get; set; }  
        public DbSet<Page> Pages { get; set; }
        public DbSet<ContainerPhoto> PagePhotos { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<ContainerLocalizableString> ContainerLocalizableStrings { get; set; } 
        public DbSet<ContainerTemplate> ContainerTemplates { get; set; }
        public DbSet<PlaceReview> PlaceReviews { get; set; }
        public DbSet<PageContainerTemplate> PageContainerTemplates { get; set; }
        public DbSet<AvailableContainer> AvailableContainers { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public bool HasNoRows()
        {
            return !Languages.Any() && !Countries.Any() && !Cities.Any() && !Streets.Any() && !Categories.Any() && !Places.Any() && !PlaceCategories.Any() && !PlaceAliases.Any() && !PlaceReviews.Any();
        }
    }
}