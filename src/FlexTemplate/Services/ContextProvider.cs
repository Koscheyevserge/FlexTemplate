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
                if (context != null)
                {
                    if (!context.Users.Any() && !context.Languages.Any() && !context.UserRoles.Any() && !context.Countries.Any() && !context.Cities.Any() && !context.Streets.Any() && !context.Categories.Any() && !context.Places.Any() && !context.PlaceCategories.Any() && !context.PlaceAliases.Any())
                    {

                        Language ukrainian = new Language { Name = "Українська", ShortName = "UA" };
                        Language english = new Language { Name = "Англійська", ShortName = "EN" };

                        UserRole administrator = new UserRole { Name = "Адміністратор" };

                        User admin = new User { Name = "Адміністратор", Surname = "Адмін", Login = "Master", EncryptedPassword = "12345", UserRole = administrator };

                        Country Ukraine = new Country { Name = "Україна" };

                        City Kiev = new City { Name = "Київ", Country = Ukraine };
                        City Lviv = new City { Name = "Львів", Country = Ukraine };

                        Street ObolonProsp = new Street { Name = "Оболонський проспект", City = Kiev };
                        Street PloshaRinok = new Street { Name = "Площа ринок", City = Lviv };

                        Category TRC = new Category { Name = "ТРЦ"};
                        Category Restaurant = new Category { Name = "Ресторан" };
                        
                        Place DrimTown = new Place { Street = ObolonProsp};
                        Place Kriivka = new Place { Street = PloshaRinok};

                        PlaceAlias DrimTownAlias = new PlaceAlias {Text = "Dream Town", Place = DrimTown};

                        PlaceCategory DrimTownTRC = new PlaceCategory {Place = DrimTown, Category = TRC };
                        PlaceCategory KriivkaRestaurant = new PlaceCategory { Place = Kriivka, Category = Restaurant };

                        context.ChangeTracker.AutoDetectChangesEnabled = false;

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

                        context.ChangeTracker.DetectChanges();

                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
