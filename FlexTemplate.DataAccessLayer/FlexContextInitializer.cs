using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.DataAccessLayer.Entities;
using FlexTemplate.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace FlexTemplate.DataAccessLayer
{
    public class FlexContextInitializer
    {
        private readonly RequestDelegate _next;
        private RoleManager<IdentityRole> RoleManager { get; }
        private UserManager<User> UserManager { get; }
        private FlexContext FlexContext { get; }

        public FlexContextInitializer(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, FlexContext flexContext, RequestDelegate next)
        {
            RoleManager = roleManager;
            UserManager = userManager;
            FlexContext = flexContext;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            FlexContext.Database.Migrate();
            await SeedBeforeUsers();
            await _next.Invoke(httpContext);
        }

        private async Task SeedBeforeUsers()
        {
            if (FlexContext.Settings.Any(s => s.Code == "IsInitialized" && s.BoolValue == true))
            {
                return;
            }
            if (FlexContext.Languages.Any() || FlexContext.Users.Any())
            {
                return;
            }
            using (var transaction = FlexContext.Database.BeginTransaction())
            {
                var isInitialized = new Setting {Code = "IsInitialized", BoolValue = true};
                FlexContext.AddRange
                (
                    isInitialized
                );
                var ukrainian = new Language {IsDefault = true, Name = "Українська", ShortName = "UA", IsActive = true};
                var english = new Language {Name = "English", ShortName = "EN", IsActive = true};
                FlexContext.AddRange(ukrainian, english);
                FlexContext.SaveChanges();
                transaction.Commit();
                await SeedUsers();
            }
        }

        private async Task SeedUsers()
        {
            var ukrainian = FlexContext.Languages.SingleOrDefault(l => l.ShortName == "UA");
            //UserRoles
            var supervisorRole = await RoleManager.CreateAsync(new IdentityRole("Supervisor"));
            var moderatorRole = await RoleManager.CreateAsync(new IdentityRole("Moderator"));
            var userRole = await RoleManager.CreateAsync(new IdentityRole("User"));
            //Users
            var supervisor = new User { UserName = "Supervisor" };
            if (supervisorRole.Succeeded)
            {
                var result = await UserManager.CreateAsync(supervisor, "Supervisor");
                if (result.Succeeded)
                {
                    await UserManager.AddToRolesAsync(supervisor, new[] { "Supervisor", "Moderator", "User" });
                    await UserManager.AddClaimAsync(supervisor, new Claim("language", ukrainian.ShortName));
                }
            }
            var moderator = new User { UserName = "Moderator" };
            if (moderatorRole.Succeeded)
            {
                var result = await UserManager.CreateAsync(moderator);
                if (result.Succeeded)
                {
                    await UserManager.AddToRolesAsync(moderator, new[] { "Moderator", "User" });
                    await UserManager.AddClaimAsync(moderator, new Claim("language", ukrainian.ShortName));
                }
            }
            var tasks = new List<Task>();
            if (userRole.Succeeded)
            {
                var user1 = new User { Name = "Сергій", Surname = "Антонович", UserName = "santon" };
                var user2 = new User { Name = "Олена", Surname = "Андріївна", UserName = "oand12" };
                var user3 = new User { Name = "Олег", Surname = "Савчук", UserName = "savchuk89" };
                var user4 = new User { Name = "Ірина", Surname = "Шевченко", UserName = "iryna.shevchenko" };
                var user5 = new User { Name = "Антон", Surname = "Книш", UserName = "knysh" };
                var user6 = new User { Name = "Сергій", Surname = "Антонович", UserName = "santon2" };
                var user7 = new User { Name = "Олена", Surname = "Андріївна", UserName = "oand122" };
                var user8 = new User { Name = "Олег", Surname = "Савчук", UserName = "savchuk892" };
                var user9 = new User { Name = "Ірина", Surname = "Шевченко", UserName = "iryna.shevchenko2" };
                var user10 = new User { Name = "Антон", Surname = "Книш", UserName = "knysh2" };
                async Task RegisterUser(User user)
                {
                    var result = await UserManager.CreateAsync(user, "password");
                    if (result.Succeeded)
                    {
                        await UserManager.AddClaimAsync(user, new Claim("language", ukrainian.ShortName));
                        await UserManager.AddToRolesAsync(user, new[] { "User" });
                    }
                }
                await RegisterUser(user1);
                await RegisterUser(user2);
                await RegisterUser(user3);
                await RegisterUser(user4);
                await RegisterUser(user5);
                await RegisterUser(user6);
                await RegisterUser(user7);
                await RegisterUser(user8);
                await RegisterUser(user9);
                await RegisterUser(user10);
            }
            Task.WaitAll(tasks.ToArray(), TimeSpan.FromSeconds(45));
            SeedAfterUsers();
        }

        private void SeedAfterUsers()
        {
            using (var transaction = FlexContext.Database.BeginTransaction())
            {
                var ukrainian = FlexContext.Languages.SingleOrDefault(l => l.ShortName == "UA");
                var english = FlexContext.Languages.SingleOrDefault(l => l.ShortName == "EN");
                //Components&ComponentTemplates
                var blogComments = new Container {Name = "BlogComments"};
                var blogCommentsDefault = new ContainerTemplate {Container = blogComments, TemplateName = "Default"};
                var blogFeed = new Container {Name = "BlogFeed"};
                var blogFeedDefault = new ContainerTemplate {Container = blogFeed, TemplateName = "Default"};
                var breadcrumbs = new Container {Name = "Breadcrumbs"};
                var breadcrumbsDefault = new ContainerTemplate {Container = breadcrumbs, TemplateName = "Default"};
                var capabilities = new Container {Name = "Capabilities"};
                var capabilitiesDefault = new ContainerTemplate {Container = capabilities, TemplateName = "Default"};
                var footer = new Container {Name = "Footer"};
                var footerDefault = new ContainerTemplate {Container = footer, TemplateName = "Default"};
                var header = new Container {Name = "Header"};
                var headerSolid = new ContainerTemplate {Container = header, TemplateName = "Solid"};
                var headerTransparent = new ContainerTemplate {Container = header, TemplateName = "Transparent"};
                var morePlaces = new Container {Name = "MorePlaces"};
                var morePlacesDefault = new ContainerTemplate {Container = morePlaces, TemplateName = "Default"};
                var newCategory = new Container {Name = "NewCategory"};
                var newCategoryDefault = new ContainerTemplate {Container = newCategory, TemplateName = "Default"};
                var newCategoryAlias = new Container {Name = "NewCategoryAlias"};
                var newCategoryAliasDefault =
                    new ContainerTemplate {Container = newCategoryAlias, TemplateName = "Default"};
                var newPageContainer = new Container {Name = "NewPageContainer"};
                var newPageContainerDefault =
                    new ContainerTemplate {Container = newPageContainer, TemplateName = "Default"};
                var newPlaceMenu = new Container {Name = "NewPlaceMenu"};
                var newPlaceMenuDefault = new ContainerTemplate {Container = newPlaceMenu, TemplateName = "Default"};
                var newPlaceProduct = new Container {Name = "NewPlaceProduct"};
                var newPlaceProductDefault =
                    new ContainerTemplate {Container = newPlaceProduct, TemplateName = "Default"};
                var otherCitiesPlaces = new Container
                {
                    Name = "OtherCitiesPlaces",
                    LocalizableStrings = new List<ContainerLocalizableString>
                    {
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "TitleLabelCaption",
                            Text = @"<span dataId='0' contenteditable='true'>Інші популярні міста</span>"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "SubtitleLabelCaption",
                            Text = @"<p dataId='0' contenteditable='true'>Mist is highly flexible, and will be able to adjust to any of your customization’s. Get your projects to a new level. Included is animation on view, Parallax block, counters and charts, high resolution graphics etc.</p>"
                        }
                    }
                };
                var otherCitiesPlacesDefault =
                    new ContainerTemplate {Container = otherCitiesPlaces, TemplateName = "Default"};
                var otherCityPlaces = new Container {Name = "OtherCityPlaces"};
                var otherCityPlacesDefault =
                    new ContainerTemplate {Container = otherCityPlaces, TemplateName = "Default"};
                var placesHeader = new Container {Name = "PlacesHeader"};
                var placesHeaderDefault = new ContainerTemplate {Container = placesHeader, TemplateName = "Default"};
                var placesFilters = new Container {Name = "PlacesFilters"};
                var placesFiltersDefault = new ContainerTemplate {Container = placesFilters, TemplateName = "Default"};
                var placesList = new Container {Name = "PlacesList"};
                var placesListGrid = new ContainerTemplate {Container = placesList, TemplateName = "Grid"};
                var placesListList = new ContainerTemplate {Container = placesList, TemplateName = "List"};
                var placesSorting = new Container {Name = "PlacesSorting"};
                var placesSortingDefault = new ContainerTemplate {Container = placesSorting, TemplateName = "Default"};
                var search = new Container
                {
                    Name = "Search",
                    LocalizableStrings = new List<ContainerLocalizableString>
                    {
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "TitleFirstLabelCaption",
                            Text = @"<h1 dataId='0' tag='TitleFirstLabelCaption' contenteditable='true'>Знайдіть найкращі ресторани за привабливою ціною!</h1>"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "EndLabelCaption",
                            Text = @"<p dataId='0' tag='SubtitleLabelCaption' contenteditable='true'>Більш ніж 5000 ресторанів по всій Україні</p>"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "FindButtonCaption",
                            Text = @"<button dataId='0' class='btn btn-primary btn-form' tag='FindButtonCaption' contenteditable='true'>Знайти</button>"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "SubtitleLabelCaption",
                            Text = @"<p dataId='0' class='around-you' tag='EndLabelCaption' contenteditable='true'>Або погляньте на інші 128 ресторанів вашого міста</p>"
                        }
                    }
                };
                var searchCenterShort = new ContainerTemplate {Container = search, TemplateName = "CenterShort"};
                var searchCenterWide = new ContainerTemplate {Container = search, TemplateName = "CenterWide"};
                var searchLeftShort = new ContainerTemplate {Container = search, TemplateName = "LeftShort"};
                var searchLeftShortAnimated =
                    new ContainerTemplate {Container = search, TemplateName = "LeftShortAnimated"};
                var searchLeftShortVideo = new ContainerTemplate {Container = search, TemplateName = "LeftShortVideo"};
                var searchLeftWide = new ContainerTemplate {Container = search, TemplateName = "LeftWide"};
                var searchLeftWideAnimated =
                    new ContainerTemplate {Container = search, TemplateName = "LeftWideAnimated"};
                var searchSlider = new Container
                {
                    Name = "SearchSlider",
                    LocalizableStrings = new List<ContainerLocalizableString>
                    {
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "TitleFirstLabelCaption",
                            Text = @"<h1 dataId='0' contenteditable='true'>Знайдіть найкращі ресторани за привабливою ціною!</h1>"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "SubtitleLabelCaption",
                            Text = @"<p dataId='0' contenteditable='true'>Більш ніж 5000 ресторанів по всій Україні</p>"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "FindButtonCaption",
                            Text = @"<button dataId='0' class='btn btn-primary btn-form' contenteditable='true'>Знайти</button>"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Tag = "EndLabelCaption",
                            Text = @"<p dataId='0' class='around-you' contenteditable='true'>Або погляньте на інші 128 ресторанів вашого міста</p>"
                        }
                    }
                };
                var searchSliderCenterWide =
                    new ContainerTemplate {Container = searchSlider, TemplateName = "SliderCenterWide"};
                var searchSliderLeftWide = new ContainerTemplate {Container = searchSlider, TemplateName = "LeftWide"};
                var searchSliderRightWide =
                    new ContainerTemplate {Container = searchSlider, TemplateName = "RightWide"};
                var suggestions = new Container {Name = "Suggestions"};
                var suggestionsDefault = new ContainerTemplate {Container = suggestions, TemplateName = "Default"};
                var cityPlace = new Container {Name = "CityPlace"};
                var cityPlaceDefault = new ContainerTemplate {Container = cityPlace, TemplateName = "Default"};
                var cityPlaces = new Container
                {
                    Name = "CityPlaces",
                    LocalizableStrings = new List<ContainerLocalizableString>
                    {
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Text = @"<span dataId='0' contenteditable='true'>Ресторани у твоєму місті</span>",
                            Tag = "TitleLabelCaption"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Text = @"<p dataId='0' contenteditable='true'>Найкращі ресторани міста на будь-який смак</p>",
                            Tag = "SubtitleLabelCaption"
                        },
                        new ContainerLocalizableString
                        {
                            Language = ukrainian,
                            Text = @"<span id='loadmoreplaces_btn' dataId='0' class='btn btn-primary' contenteditable='true'>Більше ресторанів</span>",
                            Tag = "MorePlacesButtonCaption"
                        }
                    }
                };
                var cityPlacesDefault = new ContainerTemplate {Container = cityPlaces, TemplateName = "Default"};
                var placeLocation = new Container {Name = "PlaceLocation"};
                var placeLocationDefault = new ContainerTemplate {Container = placeLocation, TemplateName = "Default"};
                var placeMenu = new Container {Name = "PlaceMenu"};
                var placeMenuDefault = new ContainerTemplate {Container = placeMenu, TemplateName = "Default"};
                var placeOverview = new Container {Name = "PlaceOverview"};
                var placeOverviewDefault = new ContainerTemplate {Container = placeOverview, TemplateName = "Default"};
                var placePhoto = new Container {Name = "PlacePhoto"};
                var placePhotoDefault = new ContainerTemplate {Container = placePhoto, TemplateName = "Default"};
                var placeReview = new Container {Name = "PlaceReview"};
                var placeReviewDefault = new ContainerTemplate {Container = placeReview, TemplateName = "Default"};
                var placeReviews = new Container {Name = "PlaceReviews"};
                var placeReviewsDefault = new ContainerTemplate {Container = placeReviews, TemplateName = "Default"};
                var youMayAlsoLike = new Container {Name = "YouMayAlsoLike"};
                var youMayAlsoLikeDefault =
                    new ContainerTemplate {Container = youMayAlsoLike, TemplateName = "Default"};
                //Pages
                var index = new Page
                {
                    Name = "Index",
                    Title = "Index",
                    Containers = new List<PageContainerTemplate>
                    {
                        new PageContainerTemplate {ContainerTemplate = searchCenterShort, Position = 1},
                        new PageContainerTemplate {ContainerTemplate = otherCitiesPlacesDefault, Position = 2},
                        new PageContainerTemplate {ContainerTemplate = cityPlacesDefault, Position = 3},
                        new PageContainerTemplate {ContainerTemplate = capabilitiesDefault, Position = 4},
                        new PageContainerTemplate {ContainerTemplate = suggestionsDefault, Position = 5}
                    }
                };
                FlexContext.AddRange
                (
                    otherCityPlacesDefault,
                    searchCenterWide,
                    placesListGrid,
                    searchLeftShort,
                    newPlaceProductDefault,
                    placesHeaderDefault,
                    placesListList,
                    placesSortingDefault,
                    searchLeftShortAnimated,
                    searchLeftShortVideo,
                    placesFiltersDefault,
                    newPlaceMenuDefault,
                    searchLeftWide,
                    searchLeftWideAnimated,
                    blogCommentsDefault,
                    blogFeedDefault,
                    breadcrumbsDefault,
                    footerDefault,
                    headerSolid,
                    headerTransparent,
                    morePlacesDefault,
                    newCategoryDefault,
                    placeLocationDefault,
                    newCategoryAliasDefault,
                    newPageContainerDefault,
                    cityPlaceDefault,
                    youMayAlsoLikeDefault,
                    index,
                    placeReviewsDefault,
                    placeReviewDefault,
                    placePhotoDefault,
                    placeOverviewDefault,
                    placeMenuDefault,
                    searchSliderRightWide,
                    searchSliderLeftWide,
                    searchSliderLeftWide,
                    searchSliderCenterWide
                );
                //Countries
                var ukraine = new Country {Name = "Україна"};
                //Cities
                var kyiv = new City {Name = "Київ", Country = ukraine};
                var kharkiv = new City {Name = "Харків", Country = ukraine};
                var lviv = new City {Name = "Львів", Country = ukraine};
                var dnipro = new City {Name = "Дніпро", Country = ukraine};
                var odessa = new City {Name = "Одеса", Country = ukraine};
                var frankivsk = new City {Name = "Івано-Франківськ", Country = ukraine};
                //Streets
                var kyivStreet1 = new Street {Name = "", City = kyiv};
                var kyivStreet2 = new Street {Name = "", City = kyiv};
                var kyivStreet3 = new Street {Name = "", City = kyiv};
                var kyivStreet4 = new Street {Name = "", City = kyiv};
                var kharkivStreet1 = new Street {Name = "", City = kharkiv};
                var kharkivStreet2 = new Street {Name = "", City = kharkiv};
                var kharkivStreet3 = new Street {Name = "", City = kharkiv};
                var kharkivStreet4 = new Street {Name = "", City = kharkiv};
                var kharkivStreet5 = new Street {Name = "", City = kharkiv};
                var kharkivStreet6 = new Street {Name = "", City = kharkiv};
                var kharkivStreet7 = new Street {Name = "", City = kharkiv};
                var kharkivStreet8 = new Street {Name = "", City = kharkiv};
                var kharkivStreet9 = new Street {Name = "", City = kharkiv};
                var kharkivStreet10 = new Street {Name = "", City = kharkiv};
                var kharkivStreet11 = new Street {Name = "", City = kharkiv};
                var kharkivStreet12 = new Street {Name = "", City = kharkiv};
                var kharkivStreet13 = new Street {Name = "", City = kharkiv};
                var lvivStreet1 = new Street {Name = "", City = lviv};
                FlexContext.AddRange
                (
                    dnipro,
                    odessa,
                    frankivsk,
                    kharkivStreet1,
                    kharkivStreet3,
                    kharkivStreet10
                );
                //CommunicationTypes
                var emailCommunication = (int)CommunicationType.Email;
                var phoneCommunication = (int)CommunicationType.Phone;
                var facebookCommunication = (int)CommunicationType.Facebook;
                var siteCommunication = (int)CommunicationType.Website;
                //PlaceCategories
                var ukrainianCuisine = new PlaceCategory {Name = "Українська"};
                var asianCuisine = new PlaceCategory {Name = "Азіатська"};
                var italianCuisine = new PlaceCategory {Name = "Італійська"};
                //Places
                var supervisor = FlexContext.Users.SingleOrDefault(u => u.UserName == "Supervisor");
                var user1 = FlexContext.Users.SingleOrDefault(u => u.UserName == "santon");
                var user2 = FlexContext.Users.SingleOrDefault(u => u.UserName == "oand12");
                var user3 = FlexContext.Users.SingleOrDefault(u => u.UserName == "savchuk89");
                var user4 = FlexContext.Users.SingleOrDefault(u => u.UserName == "iryna.shevchenko");
                var user5 = FlexContext.Users.SingleOrDefault(u => u.UserName == "knysh");
                var user6 = FlexContext.Users.SingleOrDefault(u => u.UserName == "santon");
                var user7 = FlexContext.Users.SingleOrDefault(u => u.UserName == "oand12");
                var user8 = FlexContext.Users.SingleOrDefault(u => u.UserName == "savchuk89");
                var user9 = FlexContext.Users.SingleOrDefault(u => u.UserName == "iryna.shevchenko");
                var user10 = FlexContext.Users.SingleOrDefault(u => u.UserName == "knysh");
                FlexContext.AddRange(
                    new Place
                    {
                        User = supervisor,
                        Name = "Пузата хата",
                        Street = kyivStreet1,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Text =
                                    "Спасибо за хороший вечер. Зашли случайно. Согрели, накормили. И цена приемлимая. Будем если в этом районе обязательно зайдем еще раз)",
                                Star = 4,
                                User = supervisor
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Пузата хата",
                        Street = kyivStreet2,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Il molino",
                        Street = kyivStreet1,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Aliases = new List<PlaceAlias>
                        {
                            new PlaceAlias {Language = ukrainian, Text = "Іль моліно"}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Text =
                                    "Решили отметить день рождения с молодым человеком в данном ресторане. Я забронировала столик за неделю, при этом указав, что у спутника праздник. Поздравления с днем рождения устраивают многие заведения, начиная от простых кафе и заканчивая ресторанами класса люкс",
                                Star = 5,
                                User = supervisor
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Banka",
                        Street = kyivStreet3,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Aliases = new List<PlaceAlias>
                        {
                            new PlaceAlias {Language = ukrainian, Text = "Банка"}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Text =
                                    "В январе с друзьями посетили этот ресторан. В Трипе по отзывам искали лучшее место. Придя в 'Банку' несколько не разочаровались.",
                                Star = 4,
                                User = supervisor
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Borjomi",
                        Street = kyivStreet4,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Aliases = new List<PlaceAlias>
                        {
                            new PlaceAlias {Language = ukrainian, Text = "Боржомі"}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Text =
                                    "Вкусно, красиво и спокойно. Мясо действительно очень вкусное и хорошо приготовлено. Рекомендую. Цены конечно высокие, но и ресторан не для каждого дня.",
                                Star = 5,
                                User = supervisor
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Rojo Ojo",
                        Street = kyivStreet1,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Aliases = new List<PlaceAlias>
                        {
                            new PlaceAlias {Language = ukrainian, Text = "Ройо Ойо"}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Text =
                                    "Обслуживание по форме предупредительное, при этом очень неторопливое. Если пришли на обед, имейте в виду, что вряд ли за час управитесь....",
                                Star = 3,
                                User = supervisor
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "KFC",
                        Street = kyivStreet1,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
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
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user10,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Сушия",
                        Street = kyivStreet1,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
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
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user7,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Царьград",
                        Street = kyivStreet2,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
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
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Buddha-bar",
                        Street = kyivStreet2,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
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
                                User = user1,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "MAFIA",
                        Street = kyivStreet2,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
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
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user9,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Хінкалі",
                        Street = kyivStreet4,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user6,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "BEEF",
                        Street = kyivStreet4,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
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
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user3,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Нобу",
                        Street = kyivStreet4,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user6,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user7,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Nikas Restaurant",
                        Street = kharkivStreet4,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "2",
                        Latitude = 49.992792,
                        Longitude = 36.229767,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Настоящий ресторан в Харькове. Новый современный интерьер. Цены выше среднего. Официанты вышколены. Обслуживание супер. Стильно, дорого, вкусно. Есть деньги - советую, нет денег, тогда только бизнес-ланч около 150 грн."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "Был в Харькове у родственников в ноябре, решили провести вечер в этом ресторане. Я там ни разу до этого не был, а они мне расхвалили место, заинтриговав. Порадовало, что заведение находится в самом центре."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user3,
                                Text =
                                    "Обстановка замечательная: интерьер новый, музыка приятная, зал просторный. Цены, к слову, не заоблачные, а средние ресторанные. За такой сервис от меня 5 баллов ресторану! Всё очень понравилось, особенно еда и тёплый приём."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "В Харькове была во многих местах, всё как-то однообразно. В Никасе совсем другие ощущения. Была первый раз с мужем в этом ресторане на событии 'Weekend в Nikas' 03.12. Приехали на такси в 17 часов, дорога к нему не загруженная."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "Замечательный ресторан в центре Харькова. Гостей встречает приятная, комфортная обстановка. Внутри очень чисто и красиво. Здесь вкусно готовят морепродукты, особенно мне понравился тунец. Цены не выше чем у других."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Гармата-Мигдаль",
                        Street = kharkivStreet5,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "31",
                        Latitude = 49.997320,
                        Longitude = 36.238342,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user6,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user7,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user9,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Наша Дача",
                        Street = kharkivStreet6,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "4-А",
                        Latitude = 50.050475,
                        Longitude = 36.271476,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user3,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Мисливський двір",
                        Street = kharkivStreet7,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "85-А",
                        Latitude = 50.023063,
                        Longitude = 36.238607,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user6,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user7,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user9,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Зелений Папуга",
                        Street = kharkivStreet8,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "20",
                        Latitude = 50.012689,
                        Longitude = 36.235175,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user3,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Шарикоff",
                        Street = kharkivStreet9,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "12",
                        Latitude = 49.989468,
                        Longitude = 36.231402,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user6,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user7,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user9,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Osteria il Tartufo",
                        Street = kharkivStreet11,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "10",
                        Latitude = 50.015797,
                        Longitude = 36.233386,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user3,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Шато",
                        Street = kharkivStreet12,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "30",
                        Latitude = 49.998054,
                        Longitude = 36.231981,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user6,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user7,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user9,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "MAFIA",
                        Street = kharkivStreet13,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "48-Г",
                        Latitude = 50.056752,
                        Longitude = 36.204618,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user3,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Абажур",
                        Street = kharkivStreet2,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "19",
                        Latitude = 49.997254,
                        Longitude = 36.232988,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user6,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user7,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user8,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user9,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    },
                    new Place
                    {
                        User = supervisor,
                        Name = "Кафе Кентавр",
                        Street = lvivStreet1,
                        Description =
                            "Опис закладу. Necessary up knowledge it tolerably. Unwilling departure education is be dashwoods.Easy mind life fact with see has bore ten.",
                        Address = "23-A 3-й поверх",
                        Latitude = 48.918503,
                        Longitude = 24.712582,
                        Communications = new List<PlaceCommunication>
                        {
                            new PlaceCommunication {CommunicationType = emailCommunication, Number = "test@gmail.com"},
                            new PlaceCommunication {CommunicationType = phoneCommunication, Number = "+3809755555555"},
                            new PlaceCommunication {CommunicationType = siteCommunication, Number = "https://dou.ua"},
                            new PlaceCommunication
                            {
                                CommunicationType = facebookCommunication,
                                Number = "https://facebook.com"
                            }
                        },
                        PlacePlaceCategories = new List<PlacePlaceCategory>
                        {
                            new PlacePlaceCategory {PlaceCategory = ukrainianCuisine},
                            new PlacePlaceCategory {PlaceCategory = italianCuisine},
                            new PlacePlaceCategory {PlaceCategory = asianCuisine}
                        },
                        Reviews = new List<PlaceReview>
                        {
                            new PlaceReview
                            {
                                Star = 4,
                                User = user1,
                                Text =
                                    "Были в ресторане в начале ноября 2016 года,как я поняла открытие было недавно.Интерьер очень стильный ,официанты приветливые,обслуживающего персонала очень много,места тоже.Есть огромный бар. Охраняемая парковка ,за которой действительно наблюдает охранная служба. При входе гардероб,далее стойка менеджера."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user2,
                                Text =
                                    "Очень красивый интерьер, наверное самый стильный в Харькове, отличное обслуживание и музыка. Да здесь дорого и красиво, когда заходишь, присаживаешься за столик заказываешь очень не дешевое блюдо - уже завышенные ожидания, что тебе принесут что-то подстать интерьеру, что-то из фьюжн кухни.... но тебе приносят обычное ничем ни примечательное блюдо."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user3,
                                Text =
                                    "На днях провели прекрасный вечер в этом ресторане. Там нет никакой суеты, можно отдохнуть и вкусно покушать. Кухня полностью оправдала наши ожидания."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user4,
                                Text =
                                    "Очень хороший ресторан. Вкусная кухня, приятный и отзывчивый персонал, быстрое обслуживание.  Есть зал для курящих."
                            },
                            new PlaceReview
                            {
                                Star = 4,
                                User = user5,
                                Text =
                                    "Интерьер прост и не замысловат, но вполне приличен. Меню, приемлемо и съедобно, но в оформлении 'бомжа', простые помятые листики бумаги без фото или хоть чего-то, ни какой расшифровки продуктов, многого хотите! Обслуживание хорошее, нареканий нет."
                            }
                        },
                        Schedule = new PlaceSchedule
                        {
                            MondayFrom = new TimeSpan(9, 0, 0),
                            MondayTo = new TimeSpan(22, 0, 0),
                            TuesdayFrom = new TimeSpan(9, 0, 0),
                            TuesdayTo = new TimeSpan(22, 0, 0),
                            WednesdayFrom = new TimeSpan(9, 0, 0),
                            WednesdayTo = new TimeSpan(22, 0, 0),
                            ThursdayFrom = new TimeSpan(9, 0, 0),
                            ThursdayTo = new TimeSpan(22, 0, 0),
                            FridayFrom = new TimeSpan(9, 0, 0),
                            FridayTo = new TimeSpan(22, 0, 0),
                            SaturdayFrom = new TimeSpan(9, 0, 0),
                            SaturdayTo = new TimeSpan(20, 0, 0),
                            SundayFrom = new TimeSpan(0, 0, 0),
                            SundayTo = new TimeSpan(0, 0, 0)
                        }
                    }
                );
                //BlogCategories
                var blogCategory1 = new BlogCategory
                {
                    Name = "Рецепти",
                    Aliases = new List<BlogCategoryAlias>
                    {
                        new BlogCategoryAlias
                        {
                            Language = english,
                            CreatedOn = DateTime.Now,
                            Text = "Recipes"
                        }
                    },
                    CreatedOn = DateTime.Now
                };
                var blogCategory2 = new BlogCategory
                {
                    Name = "Акції",
                    Aliases = new List<BlogCategoryAlias>
                    {
                        new BlogCategoryAlias
                        {
                            Language = english,
                            CreatedOn = DateTime.Now,
                            Text = "Actions"
                        }
                    },
                    CreatedOn = DateTime.Now
                };
                var blogCategory3 = new BlogCategory
                {
                    Name = "Новини",
                    Aliases = new List<BlogCategoryAlias>
                    {
                        new BlogCategoryAlias
                        {
                            Language = english,
                            CreatedOn = DateTime.Now,
                            Text = "News"
                        }
                    },
                    CreatedOn = DateTime.Now
                };
                //BlogTags
                var blogTag1 = new Tag
                {
                    CreatedOn = DateTime.Now,
                    Name = "Знижки",
                    TagAliases = new List<TagAlias>
                    {
                        new TagAlias
                        {
                            Language = english,
                            CreatedOn = DateTime.Now,
                            Text = "Discounts"
                        }
                    }
                };
                var blogTag2 = new Tag
                {
                    CreatedOn = DateTime.Now,
                    Name = "Українська кухня",
                    TagAliases = new List<TagAlias>
                    {
                        new TagAlias
                        {
                            Language = english,
                            CreatedOn = DateTime.Now,
                            Text = "Ukrainian cuisine"
                        }
                    }
                };
                var blogTag3 = new Tag
                {
                    CreatedOn = DateTime.Now,
                    Name = "Італійська кухня",
                    TagAliases = new List<TagAlias>
                    {
                        new TagAlias
                        {
                            Language = english,
                            CreatedOn = DateTime.Now,
                            Text = "Italian cuisine"
                        }
                    }
                };
                //Blogs
                FlexContext.AddRange
                (
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    },
                    new Blog
                    {
                        IsModerated = true,
                        CreatedOn = DateTime.Now,
                        BlogBlogCategories = new List<BlogBlogCategory>
                        {
                            new BlogBlogCategory {BlogCategory = blogCategory1},
                            new BlogBlogCategory {BlogCategory = blogCategory2},
                            new BlogBlogCategory {BlogCategory = blogCategory3}
                        },
                        BlogTags = new List<BlogTag>
                        {
                            new BlogTag {Tag = blogTag1},
                            new BlogTag {Tag = blogTag2},
                            new BlogTag {Tag = blogTag3},
                        },
                        Caption = "Назва тестової статті",
                        Comments = new List<BlogComment>
                        {
                            new BlogComment {User = user1, Text = "тестовий коментар"},
                            new BlogComment {User = user2, Text = "тестовий коментар"},
                            new BlogComment {User = user3, Text = "тестовий коментар"},
                            new BlogComment {User = user4, Text = "тестовий коментар"}
                        },
                        Text = "тестова стаття",
                        User = supervisor,
                        ViewsCount = 0
                    }
                );
                FlexContext.SaveChanges();
                transaction.Commit();
            }
        }
    }
}