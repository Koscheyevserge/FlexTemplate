using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlexTemplate.Database
{
    public static class ContextProvider
    {
        public class Seed
        {
            public List<Language> Languages { get; set; }
            public List<UserRole> UserRoles { get; set; }
            public List<User> Users { get; set; }
            public List<Country> Countries { get; set; }
            public List<City> Cities { get; set; }
            public List<Street> Streets { get; set; }
            public List<Category> Categories { get; set; }
            public List<Place> Places { get; set; }
            public List<PlaceAlias> PlaceAliases { get; set; }
            public List<PlaceCategory> PlaceCategories { get; set; }

            private Language GetLanguage(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new Language();
                    
                    result.Name = configuration[$"Seed:Languages:{arrayIndex}:Name"];
                    result.ShortName = configuration[$"Seed:Languages:{arrayIndex}:ShortName"];
                    return result;
                }
                catch
                {
                    return null;
                }
                
            }

            private UserRole GetUserRole(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new UserRole();
                   
                    result.Name = configuration[$"Seed:UserRoles:{arrayIndex}:Name"];
                    
                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private User GetUser(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new User();
                    
                    result.Name = configuration[$"Seed:Users:{arrayIndex}:Name"];
                    result.Surname = configuration[$"Seed:Users:{arrayIndex}:Surname"];
                    result.Login = configuration[$"Seed:Users:{arrayIndex}:Login"];
                    result.EncryptedPassword = configuration[$"Seed:Users:{arrayIndex}:EncryptedPassword"];
                    int userRoleId;
                    result.UserRoleId = int.TryParse(configuration[$"Seed:Users:{arrayIndex}:UserRoleId"], out userRoleId) ? userRoleId : 0;

                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private Country GetCountry(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new Country();
                    
                    result.Name = configuration[$"Seed:Countries:{arrayIndex}:Name"];
                    
                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private City GetCity(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new City();
                   
                    result.Name = configuration[$"Seed:Cities:{arrayIndex}:Name"];
                    int countryId;
                    result.CountryId = int.TryParse(configuration[$"Seed:Cities:{arrayIndex}:CountryId"], out countryId) ? countryId : 0;

                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private Street GetStreet(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new Street();
                   
                    result.Name = configuration[$"Seed:Streets:{arrayIndex}:Name"];
                    int cityId;
                    result.CityId = int.TryParse(configuration[$"Seed:Streets:{arrayIndex}:CityId"], out cityId) ? cityId : 0;

                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private Category GetCategory(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new Category();
                   
                    result.Name = configuration[$"Seed:Categories:{arrayIndex}:Name"];

                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private Place GetPlace(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new Place();
                    
                    int streetId;
                    result.StreetId = int.TryParse(configuration[$"Seed:Places:{arrayIndex}:StreetId"], out streetId) ? streetId : 0;

                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private PlaceAlias GetPlaceAlias(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new PlaceAlias();
                  
                    result.Text = configuration[$"Seed:PlaceAliases:{arrayIndex}:Text"];
                    int placeId;
                    result.PlaceId = int.TryParse(configuration[$"Seed:PlaceAliases:{arrayIndex}:PlaceId"], out placeId) ? placeId : 0;
                    int languageId;
                    result.LanguageId = int.TryParse(configuration[$"Seed:PlaceAliases:{arrayIndex}:LanguageId"], out languageId) ? languageId : 0;

                    return result;
                }
                catch
                {
                    return null;
                }

            }

            private PlaceCategory GetPlaceCategory(IConfigurationRoot configuration, int arrayIndex)
            {
                try
                {
                    var result = new PlaceCategory();
                    
                    int placeId;
                    result.PlaceId = int.TryParse(configuration[$"Seed:PlaceCategories:{arrayIndex}:PlaceId"], out placeId) ? placeId : 0;
                    int categoryId;
                    result.CategoryId = int.TryParse(configuration[$"Seed:PlaceCategories:{arrayIndex}:LanguageId"], out categoryId) ? categoryId : 0;

                    return result;
                }
                catch
                {
                    return null;
                }

            }

            public Seed(IConfigurationRoot configuration)
            {
                Languages = new List<Language>();
                for(var i = 0; configuration[$"Seed:Languages:{i}:Id"] != null; i++)
                { 
                    Languages.Add(GetLanguage(configuration, i));
                }

                UserRoles = new List<UserRole>();
                for (var i = 0; configuration[$"Seed:UserRoles:{i}:Id"] != null; i++)
                {
                    UserRoles.Add(GetUserRole(configuration, i));
                }

                Users = new List<User>();
                for (var i = 0; configuration[$"Seed:Users:{i}:Id"] != null; i++)
                {
                    Users.Add(GetUser(configuration, i));
                }
                Countries = new List<Country>();
                for (var i = 0; configuration[$"Seed:Countries:{i}:Id"] != null; i++)
                {
                    Countries.Add(GetCountry(configuration, i));
                }
                Cities = new List<City>();
                for (var i = 0; configuration[$"Seed:Cities:{i}:Id"] != null; i++)
                {
                    Cities.Add(GetCity(configuration, i));
                }
                Streets = new List<Street>();
                for (var i = 0; configuration[$"Seed:Streets:{i}:Id"] != null; i++)
                {
                    Streets.Add(GetStreet(configuration, i));
                }
                Categories = new List<Category>();
                for (var i = 0; configuration[$"Seed:Categories:{i}:Id"] != null; i++)
                {
                    Categories.Add(GetCategory(configuration, i));
                }
                Places = new List<Place>();
                for (var i = 0; configuration[$"Seed:Places:{i}:Id"] != null; i++)
                {
                    Places.Add(GetPlace(configuration, i));
                }
                PlaceAliases = new List<PlaceAlias>();
                for (var i = 0; configuration[$"Seed:PlaceAliases:{i}:Id"] != null; i++)
                {
                    PlaceAliases.Add(GetPlaceAlias(configuration, i));
                }
                PlaceCategories = new List<PlaceCategory>();
                for (var i = 0; configuration[$"Seed:PlaceCategories:{i}:Id"] != null; i++)
                {
                    PlaceCategories.Add(GetPlaceCategory(configuration, i));
                }
            }
        }
        public static void Initialize(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
           
            using (var context = serviceProvider.GetService(typeof (Context)) as Context)
            {
                if (context != null && context.HasNoRows() == true)
                {
                    var seed = new Seed(configuration);

                    context.Languages.AddRange(seed.Languages);
                    context.Users.AddRange(seed.Users);
                    context.UserRoles.AddRange(seed.UserRoles);

                    context.Countries.AddRange(seed.Countries);
                    context.Cities.AddRange(seed.Cities);
                    context.Streets.AddRange(seed.Streets);
                    context.Categories.AddRange(seed.Categories);
                    context.Places.AddRange(seed.Places);
                    context.PlaceAliases.AddRange(seed.PlaceAliases);
                    context.PlaceCategories.AddRange(seed.PlaceCategories);

                    context.SaveChanges();

                }
            }
        }
    }
}
