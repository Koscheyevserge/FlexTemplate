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
                Name = "Deutch",
                ShortName = "GE"
            });
            context.Add(new Language
            {
                Name = "Francais",
                ShortName = "FR"
            });
            context.Add(new Language
            {
                Name = "Italiano",
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
            var guestAddResult = await roleManager.CreateAsync(new IdentityRole
            {
                Name = "Guest"
            });
            var guest = new User { UserName = "Guest", Name = "Олексій", Surname = "Мирний" };
            if (guestAddResult.Succeeded)
            {

                var result = await userManager.CreateAsync(guest, "Guest");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(guest, "Guest");
                }
            }
            var ukraine = new Country
            {
                Name = "Україна"
            };
            context.Add(new CountryAlias
            {
                Country = ukraine,
                Language = ukrainian,
                Text = "Україна"
            });
            var kiev = new City
            {
                Name = "Київ",
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
                Name = "Львів",
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
                Name = "Харків",
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
                Name = "Одеса",
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
                Name = "Дніпро",
                Country = ukraine
            };
            context.Add(new CityAlias
            {
                City = dnepr,
                Language = ukrainian,
                Text = "Дніпро"
            });

            var user1 = new User { UserName = "aminailov94", Name = "Alexeii", Surname = "Minailov" };
            if (guestAddResult.Succeeded)
            {

                var result = await userManager.CreateAsync(user1, "aminailov94");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user1, "Guest");
                }
            }
            var user2 = new User { Name = "Павел", Surname = "Балыбердин" };
            var user3 = new User { Name = "Анна", Surname = "Сергеева" };
            var user4 = new User { Name = "Marina", Surname = "Rostova" };
            var user5 = new User { Name = "Dariia", Surname = "Spase" };
            var user6 = new User { Name = "Max", Surname = "Grigoriev" };
            var user7 = new User { Name = "Женя", Surname = "Нестеров" };
            var user8 = new User { Name = "Vetal", Surname = "Crimea" };
            var user9 = new User { Name = "Ann", Surname = "Starling" };
            var user10 = new User { Name = "Владимир", Surname = "Жолудь" };

            context.AddRange(
            new Blog
            {
               Author = user10,
               Caption = "10 кращих веганських рецептів",
               Preamble = "Веганство іноді називають 'суворим вегетаріанством', оскільки поряд з відмовою від м'ясної їжі вегани також виключають зі свого раціону всі інші продукти тваринного походження, скажімо, молочні продукти.",
               Text = "Веганство іноді називають 'суворим вегетаріанством', оскільки поряд з відмовою від м'ясної їжі вегани також виключають зі свого раціону всі інші продукти тваринного походження, скажімо, молочні продукти. Приблизно так само виглядає християнський пост, але на відміну від тих, хто поститься, вегани дотримуються такої дієти круглий рік, через що ставлення до них в нашому суспільстві кілька неоднозначне. Воно, це ставлення, поширюється і на будь-які вегетаріанські та веганські рецепти.",
               Comments = new List<BlogComment>
               {
                 new BlogComment {  Author = user1, Text = "Заманчиво, но страшно)) Вдруг не получится?!"},
                 new BlogComment { Author = user2, Text = "Конечно, может не получиться. Но если не попробовать – точно не получится." }
               }
           },

           new Blog
           {
               Author = user9,
               Caption = "Кулінарні секрети",
               Preamble = "Безе сміливо можна назвати кулінарним парадоксом - будучи дуже простим кондитерським виробом з усього двох компонентів",
               Text = "Безе сміливо можна назвати кулінарним парадоксом - будучи дуже простим кондитерським виробом з усього двох компонентів(білок і цукор), воно примудряється виглядати як справжній вишукування.І часом вимагає чималого кулінарної майстерності, а також знання великої кількості нюансів.Сьогоднішній гостьовий пост від проекту «Маніфо ТВ» представляє вашій увазі те, що буде цікаво і корисно дізнатися всім любителям солодкого.",
               Comments = new List<BlogComment>
               {
                 new BlogComment {  Author = user1, Text = "Очень хорошая статья- как всегда- ни убавить ни прибавить. Будучи уверенной в том, что ни капли желтка не должно попасть в белок, так и делала. "},
                 new BlogComment { Author = user2, Text = "Очень интересно! Историю не знала. Весьма познавательно и красиво оформлена статья. Спасибо. )" }
               }
           },

           new Blog
           {
                Author = user8,
                Caption = "Місця і події",
                Preamble = "Кинувши побіжний погляд на календар, неважко здогадатися, що сьогодні - третій четвер листопада.",
                Text = "Кинувши побіжний погляд на календар, неважко здогадатися, що сьогодні - третій четвер листопада. Він був би зовсім не примітним днем, якби не одне але: саме в третій четвер листопада в усьому світі відзначається свято молодого вина Божоле, а я традиційно публікую цю статтю. За моїми спостереженнями, останнім часом популярність молодого Божоле в нашій країні почала поступово сходити нанівець, але ще пару років тому кожен поважаючий себе ресторан зазивав гостей на Божоле Нуво, а різні особистості, які вважають себе експертами в області випити, щорічно виступали з заявами про тому, що Божоле можуть пити тільки дурні, яким нікуди дівати свої гроші.",
                Comments = new List<BlogComment>
                {
                 new BlogComment {  Author = user1, Text = "Я, конечно, не могу претендовать на столь изысканный вкус, коим, несомненно, обладает уважаемый автор статьи, но абхазское вино (уж не знаю, какое продают в тех краях, где гурман проживает) совсем не новоявленное, а “Лыхны” (настоящее) ничуть не хуже, если не лучше Божоле и, как минимум, в 1,5 раза дешевле (хорошего Божоле, конечно, потому что в бутылках с этой наклейкой тоже можно купить, что угодно).  "},
                 new BlogComment { Author = user2, Text = "Не соглашусь с фразой, что вина из сорта Гамэ не предназначены для долгого хранения. То, что новое – да, оно и изготавливается особым способом, и выпускается на рынок раньше всех вин нового года, его и пьют в течение 3-4 месяцев." }
                }
            },

            new Blog
            {
                   Author = user7,
                   Caption = "Кухонна техніка",
                   Preamble = "Здавалося б - чашка кави, суща дрібниця, не випадково навіть популярний метод оцінки добробуту тієї...",
                   Text = "Здавалося б - чашка кави, суща дрібниця, не випадково навіть популярний метод оцінки добробуту тієї чи іншої нації пропонує підрахувати кількість чашок кави, які можуть дозволити собі їх представники.Але варто озирнутися на всі боки, і стає зрозуміло, що кава - це не дрібниця, і навіть не новий загальний еквівалент.Кава - це нова релігія.",
                   Comments = new List<BlogComment>
                   {
                     new BlogComment {  Author = user1, Text = "Отличная статья, узнал много нового!"},
                     new BlogComment { Author = user2, Text = "Работать с техникой станет значительно проще после прочтения данного материала." }
                   }
             },
                  
             new Blog
             {
                      Author = user6,
                      Caption = "Персона",
                      Preamble = "У 2009 році мені довелося взяти інтерв'ю у Влада Піскунова, який в ту пору вів скромний блог в Живому Журналі.",
                      Text = "У 2009 році мені довелося взяти інтерв'ю у Влада Піскунова, який в ту пору вів скромний блог в Живому Журналі. З тих пір Влад залишив ЖЖ, які мігрували на сайт з тривожним назвою 'Залізо та вогонь', встиг стати автором декількох кулінарних книг і провідним телепередач про їжу, почав проводити майстер-класи і їздити в кулінарні експедиції. Я вирішив знову поспілкуватися з Владом і з'ясувати, що ще змінилося за ці кілька років.",
                      Comments = new List<BlogComment>
                      {
                         new BlogComment {  Author = user1, Text = "Интересное интервью, еще и все строго по делу, без всякой воды, благодарю."},
                         new BlogComment { Author = user2, Text = "Оказалось очень интересный человек и действительно заслуживает уважения. Пока не прочитал даже не подозревал о таких фактах." }
                      }
             });
            
            var kievStreet1 = new Street { Name = "Оболонський проспект", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet1, Language = ukrainian, Text = "Оболонський проспект" });
            var kievStreet2 = new Street { Name = "Вулиця Хрещатик", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet2, Language = ukrainian, Text = "Вулиця Хрещатик" });
            var kievStreet3 = new Street { Name = "Вулиця Антоновича", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet3, Language = ukrainian, Text = "Вулиця Антоновича" });
            var kievStreet4 = new Street { Name = "Вулиця Шота Руставелі", City = kiev };
            context.Add(new StreetAlias { Street = kievStreet4, Language = ukrainian, Text = "Вулиця Шота Руставелі" });
            var lvivStreet1 = new Street { Name = "Площа Ринок", City = lviv };
            context.Add(new StreetAlias { Street = lvivStreet1, Language = ukrainian, Text = "Площа Ринок" });
            var lvivStreet2 = new Street { Name = "Катедральна площа", City = lviv };
            context.Add(new StreetAlias { Street = lvivStreet2, Language = ukrainian, Text = "Катедральна площа" });
            var lvivStreet3 = new Street { Name = "Оперна площа", City = lviv };
            context.Add(new StreetAlias { Street = lvivStreet3, Language = ukrainian, Text = "Оперна площа" });

            var harkovStreet1 = new Street { Name = "Вулиця Павлова", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet1, Language = ukrainian, Text = "Вулиця Павлова" });
            var harkovStreet2 = new Street { Name = "Вулиця Сумська", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet2, Language = ukrainian, Text = "Вулиця Сумська" });
            var harkovStreet3 = new Street { Name = "Вулиця Гоголя", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet3, Language = ukrainian, Text = "Вулиця Гоголя" });
            var harkovStreet4 = new Street { Name = "Вулиця Університетська", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet4, Language = ukrainian, Text = "Вулиця Університетська" });
            var harkovStreet5 = new Street { Name = "Вулиця Пушкінська", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet5, Language = ukrainian, Text = "Вулиця Пушкінська" });
            var harkovStreet6 = new Street { Name = "Вулиця Батумська", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet6, Language = ukrainian, Text = "Вулиця Батумська" });
            var harkovStreet7 = new Street { Name = "Вулиця Новгородська", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet7, Language = ukrainian, Text = "Вулиця Новгородська" });
            var harkovStreet8 = new Street { Name = "Вулиця Культури", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet8, Language = ukrainian, Text = "Вулиця Культури" });
            var harkovStreet9 = new Street { Name = "Вулиця Квітки-Основ'яненка", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet9, Language = ukrainian, Text = "Вулиця Квітки-Основ'яненка" });
            var harkovStreet10 = new Street { Name = "Вулиця Гагаріна", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet10, Language = ukrainian, Text = "Вулиця Гагаріна" });
            var harkovStreet11 = new Street { Name = "Вулиця Шатилівська", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet11, Language = ukrainian, Text = "Вулиця Шатилівська" });
            var harkovStreet12 = new Street { Name = "Вулиця Римарська", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet12, Language = ukrainian, Text = "Вулиця Римарська" });
            var harkovStreet13 = new Street { Name = "Вулиця Людвига Свободи", City = harkov };
            context.Add(new StreetAlias { Street = harkovStreet13, Language = ukrainian, Text = "Вулиця Людвига Свободи" });

            var category1 = new Category { Name = "Українська кухня" };
            context.Add(new CategoryAlias { Category = category1, Language = ukrainian, Text = "Українська кухня" });
            var category2 = new Category { Name = "Італійська кухня" };
            context.Add(new CategoryAlias { Category = category2, Language = ukrainian, Text = "Італійська кухня" });
            var category3 = new Category { Name = "Грузинська кухня" };
            context.Add(new CategoryAlias { Category = category3, Language = ukrainian, Text = "Грузинська кухня" });
            var category4 = new Category { Name = "Американська кухня" };
            context.Add(new CategoryAlias { Category = category4, Language = ukrainian, Text = "Американська кухня" });
            var category5 = new Category { Name = "Японська кухня" };
            context.Add(new CategoryAlias { Category = category5, Language = ukrainian, Text = "Японська кухня" });
            var category6 = new Category { Name = "Француська кухня" };
            context.Add(new CategoryAlias { Category = category6, Language = ukrainian, Text = "Француська кухня" });
            var category7 = new Category { Name = "Вірменська кухня" };
            context.Add(new CategoryAlias { Category = category7, Language = ukrainian, Text = "Вірменська кухня" });
            var category8 = new Category { Name = "Китайська кухня" };
            context.Add(new CategoryAlias { Category = category8, Language = ukrainian, Text = "Китайська кухня" });
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
                   },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user9,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user10,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        }
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        }
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user10,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        }
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user9,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        }
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        }
                    }
                },
                new Place
                {
                    Name = "Nikas Restaurant",
                    Street = harkovStreet4,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "2",
                    Latitude = 49.992792,
                    Longitude = 36.229767,
                    Website = "contoso.com",
                    Phone = "0670001114",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Настоящий ресторан в Харькове. Новый современный интерьер. Цены выше среднего. Официанты вышколены. Обслуживание супер. Стильно, дорого, вкусно. Есть деньги - советую, нет денег, тогда только бизнес-ланч около 150 грн."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "Был в Харькове у родственников в ноябре, решили провести вечер в этом ресторане. Я там ни разу до этого не был, а они мне расхвалили место, заинтриговав. Порадовало, что заведение находится в самом центре."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "Обстановка замечательная: интерьер новый, музыка приятная, зал просторный. Цены, к слову, не заоблачные, а средние ресторанные. За такой сервис от меня 5 баллов ресторану! Всё очень понравилось, особенно еда и тёплый приём."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "В Харькове была во многих местах, всё как-то однообразно. В Никасе совсем другие ощущения. Была первый раз с мужем в этом ресторане на событии 'Weekend в Nikas' 03.12. Приехали на такси в 17 часов, дорога к нему не загруженная."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "Замечательный ресторан в центре Харькова. Гостей встречает приятная, комфортная обстановка. Внутри очень чисто и красиво. Здесь вкусно готовят морепродукты, особенно мне понравился тунец. Цены не выше чем у других."
                        }
                    }
                },
                new Place
                {
                    Name = "Гармата-Мигдаль",
                    Street = harkovStreet5,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "31",
                    Latitude = 49.997320,
                    Longitude = 36.238342,
                    Website = "contoso.com",
                    Phone = "0577545722",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user9,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user10,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "Наша Дача",
                    Street = harkovStreet6,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "4-А",
                    Latitude = 50.050475,
                    Longitude = 36.271476,
                    Website = "contoso.com",
                    Phone = "0577140989",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "Мисливський двір",
                    Street = harkovStreet7,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "85-А",
                    Latitude = 50.023063,
                    Longitude = 36.238607,
                    Website = "contoso.com",
                    Phone = "0987591986",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user9,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user10,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "Зелений Папуга",
                    Street = harkovStreet8,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "20",
                    Latitude = 50.012689,
                    Longitude = 36.235175,
                    Website = "contoso.com",
                    Phone = "0577021391",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "Шарикоff",
                    Street = harkovStreet9,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "12",
                    Latitude = 49.989468,
                    Longitude = 36.231402,
                    Website = "contoso.com",
                    Phone = "0577523344",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user9,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user10,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "Osteria il Tartufo",
                    Street = harkovStreet11,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "10",
                    Latitude = 50.015797,
                    Longitude = 36.233386,
                    Website = "contoso.com",
                    Phone = "0577020703",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "Шато",
                    Street = harkovStreet12,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "30",
                    Latitude = 49.998054,
                    Longitude = 36.231981,
                    Website = "contoso.com",
                    Phone = "0577050806",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user9,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user10,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "MAFIA",
                    Street = harkovStreet13,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "48-Г",
                    Latitude = 50.056752,
                    Longitude = 36.204618,
                    Website = "contoso.com",
                    Phone = "0675464341",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                        }
                    }
                },
                new Place
                {
                    Name = "Абажур",
                    Street = harkovStreet2,
                    Description = "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                    Address = "19",
                    Latitude = 49.997254,
                    Longitude = 36.232988,
                    Website = "contoso.com",
                    Phone = "0577160022",
                    Email = "contoso@contoso.com",
                    PlaceCategories = new List<PlaceCategory>
                    {
                        new PlaceCategory
                        {
                            Category = category1
                        }
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user6,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user7,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user8,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user9,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user10,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
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
                    },
                    Reviews = new List<PlaceReview>
                    {
                        new PlaceReview
                        {
                            Star = 4,
                            User = user1,
                            Text = "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user2,
                            Text = "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user3,
                            Text = "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user4,
                            Text = "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                        },
                        new PlaceReview
                        {
                            Star = 4,
                            User = user5,
                            Text = "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
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
                            new ContainerLocalizableString {Language = ukrainian, Text = @"<span id='loadmoreplaces_btn' dataId='0' class='btn btn-primary' contenteditable='true'>Більше ресторанів</span>", Tag = "MorePlacesButtonCaption"}
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