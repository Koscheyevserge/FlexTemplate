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
        public static void Initialize(IServiceProvider serviceProvider, IConfigurationRoot configuration)
        {
            using (var context = serviceProvider.GetService(typeof (Context)) as Context)
            {
                if (context == null)
                {
                    if (!context.Users.Any() && !context.Languages.Any() && !context.UserRoles.Any() && !context.Countries.Any() && !context.Cities.Any() && !context.Streets.Any() && !context.Categories.Any() && !context.Places.Any() && !context.PlaceCategories.Any() && !context.PlaceAliases.Any())
                    {

                        Language ukrainian = new Language { Id = 1, Name = "Українська", ShortName = "UA" };
                        Language english = new Language { Id = 2, Name = "Англійська", ShortName = "EN" };

                        UserRole administrator = new UserRole { Id = 1, Name = "Адміністратор" };

                        User admin = new User { Id = 1, Name = "Адміністратор", Surname = "Адмін", Login = "Master", EncryptedPassword = "12345", UserRole = administrator };

                        Country Ukraine = new Country { Id = 1, Name = "Україна" };

                        City Kiev = new City { Id = 1, Name = "Київ", CountryId = 1, Country = Ukraine };
                        City Lviv = new City { Id = 2, Name = "Львів", CountryId = 1, Country = Ukraine };

                        Street ObolonProsp = new Street {Id = 1, Name = "Оболонський проспект", CityId = 1, City = Kiev };
                        Street PloshaRinok = new Street { Id = 2, Name = "Площа ринок", CityId = 2, City = Lviv };

                        Category TRC = new Category { Id = 1, Name = "ТРЦ"};
                        Category Restaurant = new Category { Id = 2, Name = "Ресторан" };
                        
                        Place DrimTown = new Place { Id = 1, StreetId = 1, Street = ObolonProsp};
                        Place Kriivka = new Place { Id = 2, StreetId = 2, Street = PloshaRinok};

                        PlaceAlias DrimTownAlias = new PlaceAlias { Id = 1, Name = "Dream Town", Place = DrimTown, PlaceId = 1 };

                        PlaceCategory DrimTownTRC = new PlaceCategory {Id = 1, PlaceId = 1, Place = DrimTown, CategoryId = 1, Category = TRC };
                        PlaceCategory KriivkaRestaurant = new PlaceCategory { Id = 2, PlaceId = 2, Place = Kriivka, CategoryId = 2, Category = Restaurant };

                        context.Languages.Add(ukrainian);
                        context.Languages.Add(english);

                        context.UserRoles.Add(administrator);

                        context.Users.Add(admin);

                        context.Countries.Add(Ukraine);

                        context.Cities.Add(Kiev);
                        context.Cities.Add(Lviv);

                        context.Streets.Add(ObolonProsp);
                        context.Streets.Add(PloshaRinok);

                        context.Categories.Add(TRC);
                        context.Categories.Add(Restaurant);

                        context.Places.Add(DrimTown);
                        context.Places.Add(Kriivka);

                        context.PlaceAliases.Add(DrimTownAlias);

                        context.PlaceCategories.Add(DrimTownTRC);
                        context.PlaceCategories.Add(KriivkaRestaurant);

                        context.SaveChanges();
                    }
                    
                }
                
            }
        }
    }
}
