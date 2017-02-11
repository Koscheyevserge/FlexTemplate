﻿using System;
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
                if (context == null || !context.HasNoRows()) return;

                context.ChangeTracker.AutoDetectChangesEnabled = false;
                var english = new Language
                {
                    Name = "English",
                    ShortName = "EN"
                };
                var ukrainian = new Language
                {
                    Name = "Українська",
                    ShortName = "UA"
                };
                var supervisor = new UserRole
                {
                    Name = "Supervisor"
                };
                context.Add(new UserRoleAlias
                {
                    UserRole = supervisor,
                    Language = ukrainian,
                    Text = "Адміністратор"
                });
                context.Add(new UserRoleAlias
                {
                    UserRole = supervisor,
                    Language = english,
                    Text = "Supervisor"
                });
                var guest = new UserRole
                {
                    Name = "Guest"
                };
                context.Add(new UserRoleAlias
                {
                    UserRole = guest,
                    Language = ukrainian,
                    Text = "Гість"
                });
                context.Add(new UserRoleAlias
                {
                    UserRole = guest,
                    Language = english,
                    Text = "Guest"
                });
                context.Add(new User
                {
                    Login = "Supervisor",
                    EncryptedPassword = "Supervisor",
                    UserRole = supervisor
                });
                var ukraine = new Country
                {
                    Name = "Ukraine"
                };
                context.Add(new CountryAlias
                {
                    Country = ukraine,
                    Language = ukrainian,
                    Text = "Україна"
                });
                var kiev = new City
                {
                    Name = "Kiev",
                    Country = ukraine
                };
                context.Add(new CityAlias
                {
                    City = kiev,
                    Language = ukrainian,
                    Text = "Київ"
                });
                var lviv = new City
                {
                    Name = "Lviv",
                    Country = ukraine
                };
                context.Add(new CityAlias
                {
                    City = lviv,
                    Language = ukrainian,
                    Text = "Львів"
                });
                var iF = new City
                {
                    Name = "Ivano-Frankivsk",
                    Country = ukraine
                };
                context.Add(new CityAlias
                {
                    City = iF,
                    Language = ukrainian,
                    Text = "Івано-Франківськ"
                });
                var kievStreet1 = new Street { Name = "Obolonsky avenue", City = kiev};
                context.Add(new StreetAlias { Street = kievStreet1, Language = ukrainian, Text = "Оболонський проспект"});
                var kievStreet2 = new Street { Name = "Khreschatyk avenue", City = kiev };
                context.Add(new StreetAlias { Street = kievStreet2, Language = ukrainian, Text = "Вулиця Хрещатик" });
                var kievStreet3 = new Street { Name = "Antonovycha street", City = kiev };
                context.Add(new StreetAlias { Street = kievStreet3, Language = ukrainian, Text = "Вулиця Антовича" });
                var kievStreet4 = new Street { Name = "Shota Rustaveli street", City = kiev };
                context.Add(new StreetAlias { Street = kievStreet4, Language = ukrainian, Text = "Вулиця Шота Руставелі" });
                var lvivStreet1 = new Street { Name = "Rynok square", City = lviv };
                context.Add(new StreetAlias { Street = lvivStreet1, Language = ukrainian, Text = "Площа Ринок" });
                var lvivStreet2 = new Street { Name = "Katedralniy square", City = lviv };
                context.Add(new StreetAlias { Street = lvivStreet2, Language = ukrainian, Text = "Катедральна площа" });
                var lvivStreet3 = new Street { Name = "Operniy square", City = lviv };
                context.Add(new StreetAlias { Street = lvivStreet3, Language = ukrainian, Text = "Оперна площа" });

                var ifStreet1 = new Street { Name = "Symonenka street", City = iF };
                context.Add(new StreetAlias { Street = ifStreet1, Language = ukrainian, Text = "Вулиця Симоненка" });
                var ifStreet2 = new Street { Name = "Nezalezhnosty street", City = iF };
                context.Add(new StreetAlias { Street = ifStreet2, Language = ukrainian, Text = "Вулиця Незалежності" });
                var ifStreet3 = new Street { Name = "Chornovola street", City = iF };
                context.Add(new StreetAlias { Street = ifStreet3, Language = ukrainian, Text = "Вулиця Чорновола" });


                var category1 = new Category { Name = "Ukrainian cuisine" };
                context.Add(new CategoryAlias { Category = category1, Language = ukrainian, Text = "Українська кухня" });
                var category2 = new Category { Name = "Italian cuisine" };
                context.Add(new CategoryAlias { Category = category2, Language = ukrainian, Text = "Італійська кухня" });
                var category3 = new Category { Name = "Georgian cuisine" };
                context.Add(new CategoryAlias { Category = category3, Language = ukrainian, Text = "Грузинська кухня" });
                var category4 = new Category { Name = "American cuisine" };
                context.Add(new CategoryAlias { Category = category4, Language = ukrainian, Text = "Американська кухня" });

                context.AddRange(
                    new Place
                    {
                        Name = "Пузата хата",
                        Street = kievStreet1,
                        PlaceCategories = new List<PlaceCategory>
                        {
                            new PlaceCategory {Category = category1}
                        }
                    },
                    new Place
                    {
                        Name = "Пузата хата",
                        Street = kievStreet2,
                        PlaceCategories = new List<PlaceCategory>
                        {
                            new PlaceCategory {Category = category1}
                        }
                    }, 
                    new Place
                    {
                        Name = "Il molino",
                        Street = kievStreet1,
                        PlaceCategories = new List<PlaceCategory>
                        {
                            new PlaceCategory
                            {
                                Category = category2
                            },
                            new PlaceCategory
                            {
                                Category = category3
                            }
                        },
                        Aliases = new List<PlaceAlias>
                        {
                            new PlaceAlias {Language = ukrainian, Text = "Іль моліно"}
                        }
                    }, 
                    new Place
                    {
                        Name = "Banka",
                        Street = kievStreet3,
                        PlaceCategories = new List<PlaceCategory>
                        {
                            new PlaceCategory
                            {
                                Category = category2
                            },
                            new PlaceCategory
                            {
                                Category = category3
                            },
                            new PlaceCategory
                            {
                                Category = category1
                            }
                        },
                        Aliases = new List<PlaceAlias>
                        {
                            new PlaceAlias {Language = ukrainian, Text = "Банка"}
                        }
                    }, 
                    new Place
                    {
                        Name = "Borjomi",
                        Street = kievStreet4,
                        PlaceCategories = new List<PlaceCategory>
                        {
                            new PlaceCategory
                            {
                                Category = category4
                            }
                        },
                        Aliases = new List<PlaceAlias>
                        {
                            new PlaceAlias {Language = ukrainian, Text = "Боржомі"}
                        }
                    });

                context.ChangeTracker.DetectChanges();

                context.SaveChanges();
            }
        }
    }
}