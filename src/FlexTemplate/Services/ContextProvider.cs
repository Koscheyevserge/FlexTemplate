using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FlexTemplate.Services
{
    public class ContextProvider
    {
        private readonly RequestDelegate _next;
        private readonly Context context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public ContextProvider(Context Context, UserManager<User> UserManager, RoleManager<IdentityRole> RoleManager, RequestDelegate next)
        {
            context = Context;
            userManager = UserManager;
            roleManager = RoleManager;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await Initialize();
            await _next.Invoke(httpContext);
        }

        private async Task Initialize()
        {
            if (context == null || !context.HasNoRows())
            {
                return;
            }
            var ukrainian = new Language
            {
                Name = "Українська",
                ShortName = "UA",
                IsDefault = true,
                IsActive = true
            };
            context.Add(new Language
            {
                Name = "English",
                ShortName = "EN",
                IsActive = true
            });
            context.Add(new Language
            {
                Name = "Русский",
                ShortName = "RU",
                IsActive = true
            });
            context.Add(new Language
            {
                Name = "German",
                ShortName = "GE"
            });
            context.Add(new Language
            {
                Name = "French",
                ShortName = "FR"
            });
            context.Add(new Language
            {
                Name = "Italian",
                ShortName = "IT"
            });
            var supervisorAddResult = await roleManager.CreateAsync(new IdentityRole
            {
                Name = "Supervisor"
            });
            var supervisor = new User { UserName = "Supervisor" };
            if (supervisorAddResult.Succeeded)
            {
             
                var result = await userManager.CreateAsync(supervisor, "Supervisor123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(supervisor, "Supervisor");
                }
            }
            await roleManager.CreateAsync(new IdentityRole
            {
                Name = "Guest"
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
           
            var harkov = new City
            {
                Name = "Harkov",
                Country = ukraine
            };
            context.Add(new CityAlias
            {
                City = harkov,
                Language = ukrainian,
                Text = "Харків"
            });

            var odessa = new City
            {
                Name = "Odessa",
                Country = ukraine
            };
            context.Add(new CityAlias
            {
                City = odessa,
                Language = ukrainian,
                Text = "Одеса"
            });

            var dnepr = new City
            {
                Name = "Dnepr",
                Country = ukraine
            };
            context.Add(new CityAlias
            {
                City = dnepr,
                Language = ukrainian,
                Text = "Дніпро"
            });
            var kievStreet1 = new Street { Name = "Obolonsky avenue", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet1, Language = ukrainian, Text = "Оболонський проспект" });
            var kievStreet2 = new Street { Name = "Khreschatyk avenue", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet2, Language = ukrainian, Text = "Вулиця Хрещатик" });
            var kievStreet3 = new Street { Name = "Antonovycha street", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet3, Language = ukrainian, Text = "Вулиця Антоновича" });
            var kievStreet4 = new Street { Name = "Shota Rustaveli street", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet4, Language = ukrainian, Text = "Вулиця Шота Руставелі" });
            var lvivStreet1 = new Street { Name = "Rynok square", City = lviv };
            context.Add(new StreetAlias { Street = lvivStreet1, Language = ukrainian, Text = "Площа Ринок" });
            var lvivStreet2 = new Street { Name = "Katedralniy square", City = lviv };
            context.Add(new StreetAlias { Street = lvivStreet2, Language = ukrainian, Text = "Катедральна площа" });
            var lvivStreet3 = new Street { Name = "Operniy square", City = lviv };
            context.Add(new StreetAlias { Street = lvivStreet3, Language = ukrainian, Text = "Оперна площа" });

            var harkovStreet1 = new Street { Name = "Pavlova street", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet1, Language = ukrainian, Text = "Вулиця Павлова" });
            var harkovStreet2 = new Street { Name = "Sumskaya street", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet2, Language = ukrainian, Text = "Вулиця Сумська" });
            var harkovStreet3 = new Street { Name = "Gogolya street", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet3, Language = ukrainian, Text = "Вулиця Гоголя" });


            var category1 = new Category { Name = "Ukrainian cuisine" };
            context.Add(new CategoryAlias { Category = category1, Language = ukrainian, Text = "Українська кухня" });
            var category2 = new Category { Name = "Italian cuisine" };
            context.Add(new CategoryAlias { Category = category2, Language = ukrainian, Text = "Італійська кухня" });
            var category3 = new Category { Name = "Georgian cuisine" };
            context.Add(new CategoryAlias { Category = category3, Language = ukrainian, Text = "Грузинська кухня" });
            var category4 = new Category { Name = "American cuisine" };
            context.Add(new CategoryAlias { Category = category4, Language = ukrainian, Text = "Американська кухня" });
            var category5 = new Category { Name = "Japanese cuisine" };
            context.Add(new CategoryAlias { Category = category5, Language = ukrainian, Text = "Японська кухня" });
            context.AddRange(
                new Place
                {
                    Name = "Пузата хата",
                    Street = kievStreet1,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory {Category = category1}
                    },
                    Reviews = new List<PlaceReview>
                    {
                         new PlaceReview {Text = "Спасибо за хороший вечер. Зашли случайно. Согрели, накормили. И цена приемлимая. Будем если в этом районе обязательно зайдем еще раз)", Star = 4, User = supervisor } 
                    }
                },
                new Place
                {
                    Name = "Пузата хата",
                    Street = kievStreet2,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory {Category = category1}
                    }
                },
                new Place
                {
                    Name = "Il molino",
                    Street = kievStreet1,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                         new PlaceReview {Text = "Решили отметить день рождения с молодым человеком в данном ресторане. Я забронировала столик за неделю, при этом указав, что у спутника праздник. Поздравления с днем рождения устраивают многие заведения, начиная от простых кафе и заканчивая ресторанами класса люкс", Star = 5, User = supervisor }
                    }
                },
                new Place
                {
                    Name = "Banka",
                    Street = kievStreet3,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                         new PlaceReview {Text = "В январе с друзьями посетили этот ресторан. В Трипе по отзывам искали лучшее место. Придя в 'Банку' несколько не разочаровались.", Star = 4, User = supervisor }
                    }
                },
                new Place
                {
                    Name = "Borjomi",
                    Street = kievStreet4,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                         new PlaceReview {Text = "Вкусно, красиво и спокойно. Мясо действительно очень вкусное и хорошо приготовлено. Рекомендую. Цены конечно высокие, но и ресторан не для каждого дня.", Star = 5, User = supervisor }
                    }
                },
                new Place
                {
                    Name = "Rojo Ojo",
                    Street = kievStreet1,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        },
                        new PlaceCategory
                        {
                            Category = category3
                        }
                    },
                    Aliases = new List<PlaceAlias>
                    {
                        new PlaceAlias {Language = ukrainian, Text = "Ройо Ойо"}
                    },
                    Reviews = new List<PlaceReview>
                    {
                         new PlaceReview {Text = "Обслуживание по форме предупредительное, при этом очень неторопливое. Если пришли на обед, имейте в виду, что вряд ли за час управитесь....", Star = 3, User = supervisor }
                    }
                },
                new Place
                {
                   Name = "KFC",
                   Street = kievStreet1,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                   {
                       new PlaceCategory
                       {
                           Category = category4
                       }
                      
                   },
                   Aliases = new List<PlaceAlias>
                   {
                       new PlaceAlias {Language = ukrainian, Text = "КФс"}
                   }
                },
                new Place
                {
                    Name = "Сушия",
                    Street = kievStreet1,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category5
                        }
                    },
                    Aliases = new List<PlaceAlias>
                    {
                        new PlaceAlias {Language = ukrainian, Text = "Суші Я"}
                    }
                },
                new Place
                {
                    Name = "Царьград",
                    Street = kievStreet2,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Aliases = new List<PlaceAlias>
                    {
                        new PlaceAlias {Language = ukrainian, Text = "Цаград"}
                    }
                },
                new Place
                {
                    Name = "Buddha-bar",
                    Street = kievStreet2,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        },
                         new PlaceCategory
                        {
                            Category = category4
                        }
                    },
                    Aliases = new List<PlaceAlias>
                    {
                        new PlaceAlias {Language = ukrainian, Text = "Будка бар"}
                    }
                },
                new Place
                {
                    Name = "MAFIA",
                    Street = kievStreet2,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category2
                        }
                    },
                    Aliases = new List<PlaceAlias>
                    {
                        new PlaceAlias {Language = ukrainian, Text = "МАФІЯ"}
                    }
                },
                new Place
                {
                    Name = "Хінкалі",
                    Street = kievStreet4,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category3
                        }
                    }
                },
                new Place
                {
                    Name = "BEEF",
                    Street = kievStreet4,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category4
                        }
                    },
                    Aliases = new List<PlaceAlias>
                    {
                        new PlaceAlias {Language = ukrainian, Text = "Біф"}
                    }
                },
                new Place
                {
                    Name = "Нобу",
                    Street = kievStreet4,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    }
                },
                new Place
                {
                    Name = "Кафе Кентавр",
                    Street = lvivStreet1,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "23-A 3-й поверх",
                    Latitude = 48.918503,
                    Longitude = 24.712582,
                    Website = "contoso.com",
                    Phone = "0975556501",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    }
                }
            );
            var index = new Page
            {
                Name = "Index",
                BodyClasses = "full-width-container transparent-header",
                Title = "Index"
            };
            var mainPanel = new Panel{Name = "Main"};
            var sidebar = new Panel{Name = "Sidebar"};
            var top = new Panel{Name = "Top"};
            var bottom = new Panel{Name = "Bottom"};
            var place = new Page
            {
                Name = "Place",
                BodyClasses = "full-width-container transparent-header",
                Title = "Place"
            };
            var places = new Page
            {
                Name = "Places",
                BodyClasses = "full-width-container transparent-header",
                Title = "Places"
            };
            var blog = new Page
            {
                Name = "Blog",
                BodyClasses = "full-width-container transparent-header",
                Title = "Blog"
            };
            var blogs = new Page
            {
                Name = "Blogs",
                BodyClasses = "full-width-container transparent-header",
                Title = "Blogs"
            };
            context.Containers.AddRange
            ( 
                new List<Container>
                {
                    new Container
                    {
                        Name = "Search",
                        Panel = mainPanel,
                        AvailableContainers = new List<AvailableContainer>{new AvailableContainer{Page = index}},
                        ContainerTemplates = new List<ContainerTemplate>
                        {
                            new ContainerTemplate {TemplateName = "CenterShort", PageContainerTemplates = new List<PageContainerTemplate> { new PageContainerTemplate { Page = index,  Position = 1} } },
                            new ContainerTemplate {TemplateName = "CenterWide" },
                            new ContainerTemplate {TemplateName = "LeftShort" },
                            new ContainerTemplate {TemplateName = "LeftShortAnimated" },
                            new ContainerTemplate {TemplateName = "LeftShortVideo" },
                            new ContainerTemplate {TemplateName = "LeftWide" },
                            new ContainerTemplate {TemplateName = "LeftWideAnimated" }
                        },
                        LocalizableStrings = new List<ContainerLocalizableString>
                        {
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<h1 dataId='0' tag='TitleFirstLabelCaption' contenteditable='true'>Знайдіть найкращі ресторани за привабливою ціною!</h1>", Tag = "TitleFirstLabelCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<p dataId='0' tag='SubtitleLabelCaption' contenteditable='true'>Більш ніж 5000 ресторанів по всій Україні</p>", Tag = "SubtitleLabelCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<button dataId='0' class='btn btn-primary btn-form' tag='FindButtonCaption' contenteditable='true'>Знайти</button>", Tag = "FindButtonCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<p dataId='0' class='around-you' tag='EndLabelCaption' contenteditable='true'>Або погляньте на інші 128 ресторанів вашого міста</p>", Tag = "EndLabelCaption"}
                        }
                    },
                    new Container
                    {
                        Name = "SearchSlider",
                        Panel = mainPanel,
                        AvailableContainers = new List<AvailableContainer>{new AvailableContainer{Page = index}},
                        ContainerTemplates = new List<ContainerTemplate>
                        {
                            new ContainerTemplate {TemplateName = "CenterWide" },
                            new ContainerTemplate {TemplateName = "LeftWide" },
                            new ContainerTemplate {TemplateName = "RightWide" }
                        },
                        LocalizableStrings = new List<ContainerLocalizableString>
                        {
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<h1 dataId='0' contenteditable='true'>Знайдіть найкращі ресторани за привабливою ціною!</h1>", Tag = "TitleFirstLabelCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<p dataId='0' contenteditable='true'>Більш ніж 5000 ресторанів по всій Україні</p>", Tag = "SubtitleLabelCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<button dataId='0' class='btn btn-primary btn-form' contenteditable='true'>Знайти</button>", Tag = "FindButtonCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<p dataId='0' class='around-you' contenteditable='true'>Або погляньте на інші 128 ресторанів вашого міста</p>", Tag = "EndLabelCaption"}
                        }
                    },
                    new Container
                    {
                        Name = "OtherCitiesPlaces",
                        Panel = mainPanel,
                        AvailableContainers = new List<AvailableContainer>{new AvailableContainer{Page = index}},
                        ContainerTemplates = new List<ContainerTemplate>
                        {
                            new ContainerTemplate { TemplateName = "Default", PageContainerTemplates = new List<PageContainerTemplate> { new PageContainerTemplate { Page = index,  Position = 2} }}
                        },
                        LocalizableStrings = new List<ContainerLocalizableString>
                        {
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<span dataId='0' contenteditable='true'>Інші популярні міста</span>", Tag = "TitleLabelCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<p dataId='0' contenteditable='true'>Mist is highly flexible, and will be able to adjust to any of your customization’s. Get your projects to a new level. Included is animation on view, Parallax block, counters and charts, high resolution graphics etc.</p>", Tag = "SubtitleLabelCaption"}
                        }
                    },
                    new Container
                    {
                        Name = "ThisCityPlaces",
                        Panel = mainPanel,
                        AvailableContainers = new List<AvailableContainer>{new AvailableContainer{Page = index}},
                        ContainerTemplates = new List<ContainerTemplate>
                        {
                            new ContainerTemplate { TemplateName = "Default", PageContainerTemplates = new List<PageContainerTemplate> { new PageContainerTemplate { Page = index,  Position = 3} }}
                        },
                        LocalizableStrings = new List<ContainerLocalizableString>
                        {
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<span dataId='0' contenteditable='true'>Ресторани у твоєму місті</span>", Tag = "TitleLabelCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<p dataId='0' contenteditable='true'>Найкращі ресторани міста на будь-який смак</p>", Tag = "SubtitleLabelCaption"},
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<a href='#' dataId='0' class='btn btn-primary' contenteditable='true'>Більше ресторанів</a>", Tag = "MorePlacesButtonCaption"}
                        }
                    },
                    new Container
                    {
                        Name = "Capabilities",
                        Panel = mainPanel,
                        AvailableContainers = new List<AvailableContainer>{new AvailableContainer{Page = index}},
                        ContainerTemplates = new List<ContainerTemplate>
                        {
                            new ContainerTemplate { TemplateName = "Default", PageContainerTemplates = new List<PageContainerTemplate> { new PageContainerTemplate { Page = index,  Position = 4} }}
                        }
                    },
                    new Container
                    {
                        Name = "Suggestions",
                        Panel = mainPanel,
                        AvailableContainers = new List<AvailableContainer>{new AvailableContainer{Page = index}},
                        ContainerTemplates = new List<ContainerTemplate>
                        {
                            new ContainerTemplate { TemplateName = "Default", PageContainerTemplates = new List<PageContainerTemplate> { new PageContainerTemplate { Page = index,  Position = 5} }}
                        }
                    }
                }
            );
            context.SaveChanges();
        }
    }
}