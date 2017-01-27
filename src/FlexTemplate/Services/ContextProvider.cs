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
            using (var context = serviceProvider.GetService(typeof(Context)) as Context)
            {
                if (context != null && context.HasNoRows())
                {
                    var ukrainian = new Language { Name = "Українська", ShortName = "UA" };
                    var english = new Language { Name = "English", ShortName = "EN" };

                    var administrator = new UserRole { Name = "Адміністратор" };

                    var admin = new User { Name = "Адміністратор", Surname = "Адмін", Login = "Master", EncryptedPassword = "12345", UserRole = administrator };

                    var Ukraine = new Country { Name = "Україна" };

                    var Kiev = new City { Name = "Київ", Country = Ukraine };
                    var Lviv = new City { Name = "Львів", Country = Ukraine };

                    var ObolonProsp = new Street { Name = "Оболонський проспект", City = Kiev };
                    var PloshaRinok = new Street { Name = "Площа ринок", City = Lviv };

                    var TRC = new Category { Name = "ТРЦ" };
                    var Restaurant = new Category { Name = "Ресторан" };

                    var DrimTown = new Place { Street = ObolonProsp };
                    var Kriivka = new Place { Street = PloshaRinok };

                    var DrimTownAlias = new PlaceAlias { Text = "Dream Town", Place = DrimTown, Language = english };

                    var DrimTownTRC = new PlaceCategory { Place = DrimTown, Category = TRC };
                    var KriivkaRestaurant = new PlaceCategory { Place = Kriivka, Category = Restaurant };

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