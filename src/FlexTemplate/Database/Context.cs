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
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceCategory> PlaceCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LocalizableString> LocalizableStrings { get; set; }
        public DbSet<PlaceAlias> PlaceAliases { get; set; }
        public DbSet<PlacePhoto> PlacePhotos { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<UserRole> UserRoles { get; set; } 
        public DbSet<UserKey> UserKeys { get; set; } 

        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasAlternateKey(userRole => userRole.Name);
            modelBuilder.Entity<Category>().HasAlternateKey(category => category.Name);
            base.OnModelCreating(modelBuilder);
        }
    }
}
