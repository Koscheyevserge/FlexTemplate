using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.Database
{
    public class Context : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryAlias> CategoryAliases { get; set; } 
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceAlias> PlaceAliases { get; set; }
        public DbSet<PlacePhoto> PlacePhotos { get; set; }
        public DbSet<PlaceCategory> PlaceCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CityAlias> CityAliases { get; set; } 
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryAlias> CountryAliases { get; set; } 
        public DbSet<Language> Languages { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<StreetAlias> StreetAliases { get; set; } 
        public DbSet<UserRole> UserRoles { get; set; } 
        public DbSet<UserRoleAlias> UserRoleAliases { get; set; } 
        public DbSet<UserKey> UserKeys { get; set; } 
        public DbSet<Page> Pages { get; set; }
        public DbSet<PageLocalizableString> PageLocalizableStrings { get; set; }
        public DbSet<PagePhoto> PagePhotos { get; set; } 

        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasAlternateKey(userRole => userRole.Name);
            modelBuilder.Entity<Category>().HasAlternateKey(category => category.Name);
            base.OnModelCreating(modelBuilder);
        }

        


        public bool HasNoRows()
        {
            if (!Users.Any() && !Languages.Any() && !UserRoles.Any() && !Countries.Any() && !Cities.Any() && !Streets.Any() && !Categories.Any() && !Places.Any() && !PlaceCategories.Any() && !PlaceAliases.Any())
            {
                return true;
            }
            else
                return false; 
        }
    }
}
