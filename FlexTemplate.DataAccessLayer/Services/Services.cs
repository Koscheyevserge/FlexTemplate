using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.DataAccessLayer.DataAccessObjects;
using FlexTemplate.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace FlexTemplate.DataAccessLayer.Services
{
    public class Services
    {
        private FlexContext Context { get; }
        private UserManager<User> UserManager { get; }

        public Services(FlexContext context, UserManager<User> userManager)
        {
            Context = context;
            UserManager = userManager;
        }

        public async Task<IEnumerable<CityChecklistItemDao>> GetCityChecklistItemsAsync(ClaimsPrincipal claimsPrincipal, IEnumerable<int> checkedCities)
        {
            var userLanguage = await GetUserLanguageAsync(claimsPrincipal);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var aliasesGroups = Context.CityAliases.GroupBy(ca => ca.CityId);
            var aliases = new List<KeyValuePair<int, string>>();
            foreach (var aliasesGroup in aliasesGroups)
            {
                aliases.Add(new KeyValuePair<int, string>
                (
                    aliasesGroup.Key, 
                    GetProperAlias(aliasesGroup.Select(ag => ag), defaultLanguage, userLanguage))
                );
            }
            var result = Context.Cities.Select(c => 
                new CityChecklistItemDao
                {
                    Id = c.Id,
                    Name = aliases.Where(a => a.Key == c.Id).Select(kvp => kvp.Value).FirstOrDefault() ?? c.Name,
                    Checked = checkedCities.Contains(c.Id),
                    CitiesWithoutThisIds = checkedCities.Contains(c.Id) 
                        ? checkedCities.Where(checkedCity => checkedCity != c.Id) 
                        : checkedCities.Concat(new List<int>{c.Id})
                });
            return result;
        }

        public async Task<IEnumerable<PlaceCategoryChecklistItemDao>> GetPlaceCategoriesChecklistItemsAsync(ClaimsPrincipal claimsPrincipal, IEnumerable<int> checkedCategories)
        {
            var userLanguage = await GetUserLanguageAsync(claimsPrincipal);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var aliasesGroups = Context.PlaceCategoryAliases.GroupBy(ca => ca.PlaceCategoryId);
            var aliases = new List<KeyValuePair<int, string>>();
            foreach (var aliasesGroup in aliasesGroups)
            {
                aliases.Add(new KeyValuePair<int, string>
                (
                    aliasesGroup.Key,
                    GetProperAlias(aliasesGroup.Select(ag => ag), defaultLanguage, userLanguage))
                );
            }
            var result = Context.PlaceCategories.Select(c => new PlaceCategoryChecklistItemDao
            {
                Id = c.Id,
                Name = aliases.Where(a => a.Key == c.Id).Select(kvp => kvp.Value).FirstOrDefault() ?? c.Name,
                Checked = checkedCategories.Contains(c.Id),
                CategoriesWithoutThisIds = checkedCategories.Contains(c.Id) ? checkedCategories.Where(checkedCategory => checkedCategory != c.Id) : checkedCategories.Concat(new List<int>{c.Id})
            });
            return result;
        }

        public async Task<IEnumerable<PlaceListItemDao>> GetPlacesListAsync(ClaimsPrincipal claimsPrincipal, IEnumerable<int> placesIds)
        {
            var userLanguage = await GetUserLanguageAsync(claimsPrincipal);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var aliasesGroups = Context.PlaceAliases.GroupBy(ca => ca.PlaceId);
            var aliases = new List<KeyValuePair<int, string>>();
            foreach (var aliasesGroup in aliasesGroups)
            {
                aliases.Add(new KeyValuePair<int, string>
                (
                    aliasesGroup.Key,
                    GetProperAlias(aliasesGroup.Select(ag => ag), defaultLanguage, userLanguage))
                );
            }
            var categories = Context.Places
                .Include(p => p.PlacePlaceCategories)
                .ThenInclude(ppc => ppc.PlaceCategory)
                .ThenInclude(pc => pc.Aliases)
                .ThenInclude(a => a.Language)
                .Select(p => 
                    new KeyValuePair<int, IEnumerable<KeyValuePair<int, string>>>
                    (
                        p.Id, 
                        p.PlacePlaceCategories.Select
                        (
                            ppc => 
                            new KeyValuePair<int, string>
                            (
                                ppc.PlaceCategoryId,
                                GetProperAlias(ppc.PlaceCategory.Aliases, ppc.PlaceCategory.Name, defaultLanguage, userLanguage)
                            )
                        )
                    )
                );
            var addresses = Context.Places
                .Include(p => p.Street)
                .ThenInclude(p => p.City)
                .ThenInclude(c => c.Aliases)
                .Include(p => p.Street)
                .ThenInclude(s => s.Aliases)
                .Select(p => 
                    new KeyValuePair<int, string>
                    (
                        p.Id,
                        GetAddress(
                            GetProperAlias(p.Street.City.Aliases, p.Street.City.Name, defaultLanguage, userLanguage), 
                            GetProperAlias(p.Street.Aliases, p.Street.Name, defaultLanguage, userLanguage), 
                            p.Address)
                    )
                );
            var result = Context.Places
                .Include(p => p.Reviews)
                .Include(p => p.Menus).ThenInclude(m => m.Products)
                .Select(p => 
                new PlaceListItemDao
                {
                    Id = p.Id,
                    Name = aliases.Where(a => a.Key == p.Id).Select(kvp => kvp.Value).FirstOrDefault() ?? p.Name,
                    Categories = categories.Any() 
                        ? categories.SingleOrDefault(a => a.Key == p.Id).Value
                        : new List<KeyValuePair<int, string>>(),
                    Stars = GetPlaceStars(p.Reviews),
                    ReviewsCount = p.Reviews.Count,
                    //TODO Address = addresses.Single(a => a.Key == p.Id).Value,
                    HeadPhoto = "",//TODO получить фото
                    AveragePrice = p.Menus.Any() 
                        ? p.Menus.Where(m => m.Products.Any()).Average(m => m.Products.Average(prod => prod.Price)) 
                        : 0,
                    Description = p.Description
                });
            return result;
        }

        private IQueryable<ContainerLocalizableString> GetLocalizableStrings(Container container, Language defaultLanguage, Language userLanguage)
        {
            var strings = Context.ContainerLocalizableStrings
                .Where(cls => cls.Container == container && (cls.Language == defaultLanguage || (userLanguage != null && cls.Language == userLanguage)))
                .GroupBy(cls => cls.Tag)
                .Select(cls => userLanguage != null && cls.Any(c => c.Language == userLanguage) 
                    ? cls.FirstOrDefault(c => c.Language == userLanguage) 
                    : cls.FirstOrDefault(c => c.Language == defaultLanguage));
            return strings;
        }

        private async Task<Language> GetUserLanguageAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await UserManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                return null;
            var claims = await UserManager.GetClaimsAsync(user);
            var languageClaim = claims.FirstOrDefault(c => c.Type == "language");
            var userLanguage = Context.Languages.SingleOrDefault(l => l.ShortName == languageClaim.Value);
            return userLanguage;
        }

        private async Task<Language> GetDefaultLanguageAsync()
        {
            var result = await Context.Languages.SingleOrDefaultAsync(l => l.IsDefault);
            return result;
        }

        public async Task<HeaderViewComponentDao> GetHeaderViewComponentDaoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            Language userLanguage = null;
            if (user != null)
            {
                var claims = await UserManager.GetClaimsAsync(user);
                var languageClaim = claims.FirstOrDefault(c => c.Type == "language");
                userLanguage = Context.Languages.SingleOrDefault(l => l.ShortName == languageClaim.Value);
            }
            var container = Context.Containers.SingleOrDefault(c => c.Name == componentName);
            var defaultLanguage = Context.Languages.Single(l => l.IsDefault);
            var strings = GetLocalizableStrings(container, defaultLanguage, userLanguage);
            var languages = Context.Languages.Select(l => new KeyValuePair<int, string>(l.Id, l.Name));
            var isLogined = user != null;
            var userName = isLogined ? GetUsername(user) : string.Empty;
            //TODO реализовать вычитку текущего типа хедера из настроек
            var templateName = "Solid";
            var result = new HeaderViewComponentDao
            {
                Languages = languages,
                CurrentLanguageName = userLanguage != null ? userLanguage.Name : defaultLanguage.Name,
                IsLogined = isLogined,
                UserName = userName,
                TemplateName = templateName
            };
            return result;
        }

        public async Task<SearchViewComponentDao> GetSearchViewComponentDaoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var categories = Context.PlaceCategories.Select(pc => new KeyValuePair<int, string>(pc.Id, pc.Name))
                .Future();
            var cities = Context.Cities.Select(pc => new KeyValuePair<int, string>(pc.Id, pc.Name))
                .Future();
            var container = Context.Containers.SingleOrDefault(c => c.Name == componentName);
            var defaultLanguage = Context.Languages.Single(l => l.IsDefault);
            var strings = GetLocalizableStrings(container, defaultLanguage, userLanguage);
            var titleFirstLabelCaption = strings.SingleOrDefault(s => s.Tag == "TitleFirstLabelCaption")?.Text;
            var endLabelCaption  = strings.SingleOrDefault(s => s.Tag == "EndLabelCaption")?.Text;
            var findButtonCaption  = strings.SingleOrDefault(s => s.Tag == "FindButtonCaption")?.Text;
            var subtitleLabelCaption = strings.SingleOrDefault(s => s.Tag == "SubtitleLabelCaption")?.Text;
            var result = new SearchViewComponentDao
            {
                Categories = categories,
                Cities = cities,
                //TODO реализовать получение фотографии
                BackgroundImagePath = "",
                TitleFirstLabelCaption = titleFirstLabelCaption,
                EndLabelCaption = endLabelCaption,
                FindButtonCaption = findButtonCaption,
                SubtitleLabelCaption = subtitleLabelCaption
            };
            return result;
        }

        public async Task<OtherCitiesPlacesViewComponentDao> GetOtherCitiesPlacesViewComponentDaoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var cities = Context.Places.GroupBy(c => c.Street.CityId)
                .OrderByDescending(ig => ig.Count()).Select(ig => ig.Key).Take(4);
            var container = Context.Containers.SingleOrDefault(c => c.Name == componentName);
            var defaultLanguage = Context.Languages.Single(l => l.IsDefault);
            var strings = GetLocalizableStrings(container, defaultLanguage, userLanguage);
            var subtitleLabelCaption = strings.SingleOrDefault(s => s.Tag == "SubtitleLabelCaption")?.Text;
            var titleLabelCaption = strings.SingleOrDefault(s => s.Tag == "TitleLabelCaption")?.Text;
            var result = new OtherCitiesPlacesViewComponentDao
            {
                OtherCitiesPlacesIds = cities,
                SubtitleLabelCaption = subtitleLabelCaption,
                TitleLabelCaption = titleLabelCaption
            };
            return result;
        }

        public async Task<OtherCityPlacesViewComponentDao> GetOtherCityPlacesViewComponentDaoAsync(ClaimsPrincipal httpContextUser, string componentName, int cityId)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var container = Context.Containers.SingleOrDefault(c => c.Name == componentName);
            var defaultLanguage = Context.Languages.Single(l => l.IsDefault);
            var strings = GetLocalizableStrings(container, defaultLanguage, userLanguage);
            var placeDescriptor = strings.SingleOrDefault(s => s.Tag == "PlaceDescriptor")?.Text;
            var cityName = Context.Cities.Where(c => c.Id == cityId).Select(c => c.Name).SingleOrDefault();
            var placesCount = Context.Places.Count(c => c.Street.CityId == cityId);
            var result = new OtherCityPlacesViewComponentDao
            {
                CityId = cityId,
                CityName = cityName,
                //TODO реализовать получение фотографии
                PhotoPath = "",
                PlaceDescriptor = placeDescriptor,
                PlacesCount = placesCount
            };
            return result;
        }

        public async Task<CityPlacesViewComponentDao> GetCityPlacesViewComponentDaoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var container = Context.Containers.SingleOrDefault(c => c.Name == componentName);
            var defaultLanguage = Context.Languages.Single(l => l.IsDefault);
            var strings = GetLocalizableStrings(container, defaultLanguage, userLanguage);
            //TODO реализовать получение текущего города пользователя
            var thisCityId = Context.Places.GroupBy(c => c.Street.City)
                .OrderByDescending(ig => ig.Count()).Select(ig => ig.Key.Id).FirstOrDefault();
            //TODO реализовать сортировку заведений по популярности
            var thisCityPlaceIds = Context.Places.Include(p => p.Street)
                .Where(p => p.Street.CityId == thisCityId).Select(p => p.Id).Take(8);
            var subtitleLabelCaption = strings.SingleOrDefault(s => s.Tag == "SubtitleLabelCaption")?.Text;
            var titleLabelCaption = strings.SingleOrDefault(s => s.Tag == "TitleLabelCaption")?.Text;
            var morePlacesButtonCaption = strings.SingleOrDefault(s => s.Tag == "MorePlacesButtonCaption")?.Text;
            var result = new CityPlacesViewComponentDao
            {
                ThisCityPlaceIds = thisCityPlaceIds,
                SubtitleLabelCaption = subtitleLabelCaption,
                TitleLabelCaption = titleLabelCaption,
                MorePlacesButtonCaption = morePlacesButtonCaption
            };
            return result;
        }

        public async Task<CityPlaceViewComponentDao> GetCityPlaceViewComponentDaoAsync(ClaimsPrincipal httpContextUser, string componentName, int placeId)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var container = Context.Containers.SingleOrDefault(c => c.Name == componentName);
            var defaultLanguage = Context.Languages.Single(l => l.IsDefault);
            var strings = GetLocalizableStrings(container, defaultLanguage, userLanguage);
            var result = Context.Places
                .Include(p => p.Reviews)
                .Include(p => p.PlacePlaceCategories).ThenInclude(ppc => ppc.PlaceCategory)
                .Select(p => 
                new CityPlaceViewComponentDao
                {
                    Name = p.Name,
                    Address = p.Address,
                    PlaceId = placeId,
                    ReviewsCount = p.Reviews.Count,
                    Stars = Math.Ceiling(p.Reviews.Average(r => r.Star)),
                    Categories = p.PlacePlaceCategories.Select(ppc => 
                        new KeyValuePair<int, string>(ppc.PlaceCategory.Id, ppc.PlaceCategory.Name))
                })
                //TODO понять почему не работает SingleOrDefault()
                .FirstOrDefault(p => p.PlaceId == placeId);
            var reviewsDescriptor = strings.SingleOrDefault(s => s.Tag == "ReviewsDescriptor")?.Text;
            result.ReviewsDescriptor = reviewsDescriptor;
            //TODO реализовать получение фотографии
            result.PhotoPath = "";
            return result;
        }

        public async Task<PageContainersHierarchyDao> GetPageContainersHierarchyAsync(int pageContainerTemplateId)
        {
            var containers = await Context.PageContainerTemplates
                .Include(pct => pct.ContainerTemplate).ThenInclude(ct => ct.Container).Include(pct => pct.Page)
                .Where(pct => pct.ParentId == pageContainerTemplateId).OrderBy(pct => pct.Position)
                .Select(pct =>
                    new PageContainerElementDao
                    {
                        Id = pct.Id,
                        ContainerName = pct.ContainerTemplate.Container.Name,
                        ContainerTemplateName = pct.ContainerTemplate.TemplateName,
                        ParentId = pct.ParentId
                    }).ToListAsync();
            var hierarchy = new PageContainersHierarchyDao { Containers = containers };
            return hierarchy;
        }

        public async Task<PageContainersHierarchyDao> GetPageContainersHierarchy(string pageName)
        {
            var containers = await Context.PageContainerTemplates
                .Include(pct => pct.ContainerTemplate).ThenInclude(ct => ct.Container).Include(pct => pct.Page)
                .Where(pct => pct.Page.Name == pageName && pct.ParentId == 0).OrderBy(pct => pct.Position)
                .Select(pct =>
                    new PageContainerElementDao
                    {
                        Id = pct.Id,
                        ContainerName = pct.ContainerTemplate.Container.Name,
                        ContainerTemplateName = pct.ContainerTemplate.TemplateName,
                        ParentId = pct.ParentId
                    }).ToListAsync();
            var hierarchy = new PageContainersHierarchyDao {Containers = containers};
            return hierarchy;
        }

        public async Task<List<CachedPlaceDao>> GetPlacesAsync()
        {
            var places = await Context.Places
                .Include(p => p.Street).ThenInclude(s => s.City).ThenInclude(c => c.Aliases)
                .Include(p => p.PlacePlaceCategories)
                .ThenInclude(ppc => ppc.PlaceCategory).ThenInclude(pc => pc.Aliases)
                .Include(p => p.Reviews)
                .Include(p => p.Aliases)
                .Select(p => 
                new CachedPlaceDao
                {
                    Id = p.Id,
                    City = new CachedCityDao
                    {
                        Id = p.Street.CityId,
                        Name = p.Street.City.Name,
                        Names = p.Street.City.Aliases.Select(ca => 
                        new CachedCityNameDao
                        {
                            Name = ca.Text,
                            LanguageId = ca.LanguageId
                        })
                    },
                    Name = p.Name,
                    Names = p.Aliases.Select(pa => 
                    new CachedPlaceNameDao
                    {
                        Name = pa.Text,
                        LanguageId = pa.LanguageId
                    }),
                    Categories = p.PlacePlaceCategories.Select(ppc => 
                    new CachedCategoryDao
                    {
                        Id = ppc.PlaceCategoryId,
                        Name = ppc.PlaceCategory.Name,
                        Names = ppc.PlaceCategory.Aliases.Select(a => 
                        new CachedCategoryNameDao
                        {
                            Name = a.Text,
                            LanguageId = a.LanguageId
                        })
                    }),
                    Rating = p.Reviews.Any() ? p.Reviews.Average(r => r.Star) : 0,
                    ViewsCount = p.ViewsCount
                }).ToListAsync();
            return places;
        }

        public async Task<bool> CanEditVisualsAsync(ClaimsPrincipal httpContextUser)
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            if (user == null)
            {
                return false;
            }
            var isSupervisor = await UserManager.IsInRoleAsync(user, "Supervisor");
            return isSupervisor;
        }

        public async Task<bool> IsAuthorAsync<T>(ClaimsPrincipal httpContextUser, int placeId) where T : BaseAuthorfullEntity
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            return user != null && Context.GetSet<T>().Any(p => p.User == user && p.Id == placeId);
        }

        public async Task<UserDao> GetUserAsync(ClaimsPrincipal httpContextUser)
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            var defaultLanguage = await Context.Languages.SingleAsync(l => l.IsDefault);
            if (user == null)
            {
                return null;
            }
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            return new UserDao { DefaultLanguageId = defaultLanguage.Id, UserLanguageId = userLanguage.Id};
        }

        public async Task<PlaceHeaderComponentDao> GetPlaceHeaderAsync(ClaimsPrincipal httpContextUser, int placeId)
        {
            if (placeId == 0)
            {
                return null;
            }
            var canEdit = await IsAuthorAsync<Place>(httpContextUser, placeId);
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = Context.Places
                .Include(p => p.Reviews)
                .Where(p => p.Id == placeId)
                .Select(p => new PlaceHeaderComponentDao
                {
                    PlaceId = p.Id,
                    Stars = GetPlaceStars(p.Reviews),
                    ReviewsCount = p.Reviews.Count,
                    PlaceName = GetProperAlias(p.Aliases, p.Name, defaultLanguage, userLanguage),
                    PlaceLocation = GetAddress(
                        GetProperAlias(p.Street.City.Aliases, p.Street.City.Name, defaultLanguage, userLanguage),
                        GetProperAlias(p.Street.Aliases, p.Street.Name, defaultLanguage, userLanguage),
                        p.Address),
                    PlaceBannerPath = "",//TODO получить фото
                    CanEdit = canEdit
                }).SingleOrDefault();
            return result;
        }

        public Task<PlaceLocationComponentDao> GetPlaceLocationAsync(int placeId)
        {
            return Context.Places.Where(p => p.Id == placeId).Select(p =>
                new PlaceLocationComponentDao
                {
                    Latitude = p.Latitude,
                    Longitude = p.Longitude
                }).SingleOrDefaultAsync();
        }

        public Task<PlaceReviewComponentDao> GetPlaceReviewAsync(int reviewId)
        {
            return Context.PlaceReviews
                .Where(pr => pr.Id == reviewId)
                .Select(pr =>
                new PlaceReviewComponentDao
                {
                    UserPhotoPath = "",//TODO получить фото
                    Text = pr.Text,
                    Stars = pr.Star,
                    UserName = GetUsername(pr.User),
                    CreatedOn = pr.CreatedOn
                }).SingleOrDefaultAsync();
        }

        public Task<PlaceMenuComponentDao> GetPlaceMenusAsync(int placeId)
        {
            return Context.Places
                .Include(p => p.Menus).ThenInclude(m => m.Products)
                .Where(p => p.Id == placeId).Select(p =>
                new PlaceMenuComponentDao
                {
                    Menus = p.Menus.Select(m =>
                    new PlaceMenuComponentMenuDao
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Products = m.Products.Select(prod =>
                        new PlaceMenuComponentProductDao
                        {
                            Price = prod.Price,
                            Description = prod.Description,
                            PhotoPath = "",//TODO получить фото
                            Title = prod.Title
                        })
                    })
                }).SingleOrDefaultAsync();
        }

        public async Task<PlaceOverviewComponentDao> GetPlaceOverviewAsync(ClaimsPrincipal httpContextUser, int placeId)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = await Context.Places
                .Include(p => p.Communications)
                .ThenInclude(pc => pc.CommunicationType)
                .Include(p => p.PlacePlaceCategories)
                .ThenInclude(ppc => ppc.PlaceCategory)
                .ThenInclude(pc => pc.Aliases)
                .Where(p => p.Id == placeId)
                .Select(p => 
                new PlaceOverviewComponentDao
                {
                    Address = p.Address,
                    Description = p.Description,
                    Schedule = p.Schedule != null 
                    ? new PlaceOverviewComponentScheduleDao
                    {
                        MondayOpenTime = $"{p.Schedule.MondayFrom:hh\\:mm} - {p.Schedule.MondayTo:hh\\:mm}",
                        TuesdayOpenTime = $"{p.Schedule.TuesdayFrom:hh\\:mm} - {p.Schedule.TuesdayTo:hh\\:mm}",
                        WednesdayOpenTime = $"{p.Schedule.WednesdayFrom:hh\\:mm} - {p.Schedule.WednesdayTo:hh\\:mm}",
                        ThursdayOpenTime = $"{p.Schedule.ThursdayFrom:hh\\:mm} - {p.Schedule.ThursdayTo:hh\\:mm}",
                        FridayOpenTime = $"{p.Schedule.FridayFrom:hh\\:mm} - {p.Schedule.FridayTo:hh\\:mm}",
                        SaturdayOpenTime = $"{p.Schedule.SaturdayFrom:hh\\:mm} - {p.Schedule.SaturdayTo:hh\\:mm}",
                        SundayOpenTime = $"{p.Schedule.SundayFrom:hh\\:mm} - {p.Schedule.SundayTo:hh\\:mm}"
                    } 
                    : null,
                    Email = p.Communications.Any(c => c.CommunicationType.Name == "Email") 
                    ? p.Communications.First(c => c.CommunicationType.Name == "Email").Number
                    : null,
                    HasSchedule = p.Schedule != null && 
                    !(p.Schedule.MondayFrom == TimeSpan.Zero && p.Schedule.MondayTo == TimeSpan.Zero &&
                     p.Schedule.TuesdayFrom == TimeSpan.Zero && p.Schedule.TuesdayTo == TimeSpan.Zero &&
                     p.Schedule.WednesdayFrom == TimeSpan.Zero && p.Schedule.WednesdayTo == TimeSpan.Zero &&
                     p.Schedule.ThursdayFrom == TimeSpan.Zero && p.Schedule.ThursdayTo == TimeSpan.Zero &&
                     p.Schedule.FridayFrom == TimeSpan.Zero && p.Schedule.FridayTo == TimeSpan.Zero &&
                     p.Schedule.SaturdayFrom == TimeSpan.Zero && p.Schedule.SaturdayTo == TimeSpan.Zero &&
                     p.Schedule.SundayFrom == TimeSpan.Zero && p.Schedule.SundayTo == TimeSpan.Zero),
                    Phone = p.Communications.Any(c => c.CommunicationType.Name == "Phone") 
                    ? p.Communications.First(c => c.CommunicationType.Name == "Phone").Number
                    : null,
                    PlaceCategoriesEnumerated = string.Join(",", p.PlacePlaceCategories
                        .Select(ppc => 
                            GetProperAlias(ppc.PlaceCategory.Aliases, 
                            ppc.PlaceCategory.Name, 
                            defaultLanguage, 
                            userLanguage))),
                    RowsOfFeatures = p.FeatureColumns.OrderBy(fc => fc.Position).Select(fc => string.Join(",", fc.Features.OrderBy(f => f.Row).Select(f => f.Name))).ToArray(),
                    Website = p.Communications.Any(c => c.CommunicationType.Name == "Website") 
                    ? p.Communications.First(c => c.CommunicationType.Name == "Website").Number
                    : null
                }).SingleOrDefaultAsync();
            return result;
        }

        public Task<PlaceReviewsComponentDao> GetPlaceReviewsAsync(int placeId)
        {
            return Context.Places
                .Where(p => p.Id == placeId)
                .Select(p =>
                    new PlaceReviewsComponentDao
                    {
                        Reviews = p.Reviews.OrderBy(r => r.CreatedOn).Select(r => r.Id)
                    }).SingleOrDefaultAsync();
        }

        public async Task<YouMayAlsoLikeComponentDao> GetYouMayAlsoLikeAsync(ClaimsPrincipal httpContextUser, int placeId)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = new YouMayAlsoLikeComponentDao
            {
                Places = Context.Places.Include(p => p.PlacePlaceCategories)
                    .ThenInclude(ppc => ppc.PlaceCategory).ThenInclude(pc => pc.Aliases)
                    .Include(p => p.Reviews).Include(p => p.Street).ThenInclude(s => s.Aliases)
                    .Include(p => p.Street).ThenInclude(s => s.City).ThenInclude(c => c.Aliases)
                    .Where(p => p.Id != placeId)//TODO сделать фильтр выбора заведений "вам может быть интересно"
                    .Select(p =>
                    new YouMayAlsoLikeComponentPlaceDao
                    {
                        Id = p.Id,
                        Name = GetProperAlias(p.Aliases, p.Name, defaultLanguage, userLanguage),
                        Address = GetAddress(
                            GetProperAlias(p.Street.City.Aliases, p.Street.City.Name, defaultLanguage, userLanguage),
                            GetProperAlias(p.Street.Aliases, p.Street.Name, defaultLanguage, userLanguage),
                            p.Address),
                        Stars = GetPlaceStars(p.Reviews),
                        Categories = p.PlacePlaceCategories.Select(ppc => 
                        new YouMayAlsoLikeComponentCategoryDao
                        {
                            Name = GetProperAlias(ppc.PlaceCategory.Aliases, ppc.PlaceCategory.Name, defaultLanguage, userLanguage)
                        }),
                        ReviewsCount = p.Reviews.Count,
                        PhotoPath = ""//TODO получить фото
                    }).Take(4)
            };
            return result;
        }

        public Task<int> GetBlogsCountAsync(int[] tags, int[] categories, string input)
        {
            var blogs = Context.Blogs.Include(b => b.BlogTags).Include(b => b.BlogBlogCategories).AsQueryable();
            if (tags != null && tags.Any())
            {
                blogs = blogs.Where(b => b.BlogTags.Select(bt => bt.TagId).Intersect(tags).Any());
            }
            if (categories != null && categories.Any())
            {
                blogs = blogs.Where(b => b.BlogBlogCategories.Select(bbc => bbc.BlogCategoryId)
                    .Intersect(categories).Any());
            }
            if (!string.IsNullOrEmpty(input))
            {
                var inputNormalized = input.Trim().ToUpperInvariant();
                blogs = blogs.Where(b => !string.IsNullOrEmpty(b.Caption))
                    .Where(b => b.Caption.Trim().ToUpper().Contains(inputNormalized));
            }
            return blogs.CountAsync();
        }

        public Task<List<CachedBlogItemDao>> GetBlogsAsync(int[] tags, int[] categories, string input, int page, int blogsPerPage)
        {
            var result = Context.Blogs.Include(b => b.BlogTags).Include(b => b.BlogBlogCategories).AsQueryable();
            if (tags != null && tags.Any())
            {
                result = result.Where(b => b.BlogTags.Select(bt => bt.TagId).Intersect(tags).Any());
            }
            if (categories != null && categories.Any())
            {
                result = result.Where(b => b.BlogBlogCategories
                    .Select(bt => bt.BlogCategoryId).Intersect(categories).Any());
            }
            if (!string.IsNullOrEmpty(input))
            {
                var inputNormalized = input.Trim().ToUpperInvariant();
                result = result.Where(b => b.Caption.ToUpperInvariant().Contains(inputNormalized) 
                    || b.Text.ToUpperInvariant().Contains(inputNormalized));
            }
            return result.Select(b =>
                new CachedBlogItemDao
                {
                    Id = b.Id,
                    Caption = b.Caption,
                    CreatedOn = b.CreatedOn,
                    AuthorName = GetUsername(b.User),
                    HeadPhotoPath = "",//TODO получить фото
                    IsModerated = b.IsModerated,
                    Preable = ""//TODO извлечь преамбулу
                }).Skip((page - 1) * blogsPerPage).Take(blogsPerPage).ToListAsync();
        }

        public Task<List<BlogsFeedComponentPopularBlogDao>> GetBlogsFeedPopularBlogsAsync()
        {
            return Context.Blogs.OrderBy(b => b.ViewsCount)
                .Select(b => 
                    new BlogsFeedComponentPopularBlogDao
                    {
                        Id = b.Id,
                        Caption = b.Caption,
                        CommentsCount = b.Comments.Count,
                        HeadPhotoPath = ""//TODO извлечь преамбулу
                    }).Take(4).ToListAsync();
        }

        public async Task<List<BlogsFeedComponentTagDao>> GetBlogsFeedTags(ClaimsPrincipal httpContextUser, IEnumerable<int> tags, string input)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = await Context.Tags.Include(t => t.TagAliases)
                .Select(t =>
                    new BlogsFeedComponentTagDao
                    {
                        Name = GetProperAlias(t.TagAliases, t.Name, defaultLanguage, userLanguage),
                        WithoutThisIds = tags.Contains(t.Id) ? tags.Where(tag => tag != t.Id) : tags.Concat(new List<int>{t.Id})
                    }).ToListAsync();
            return result;
        }

        public async Task<List<BlogsFeedComponentCategoryDao>> GetBlogsFeedCategories(ClaimsPrincipal httpContextUser, IEnumerable<int> categories, string input)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = await Context.BlogCategories.Include(bc => bc.Aliases)
                .Select(bc =>
                    new BlogsFeedComponentCategoryDao
                    {
                        Caption = GetProperAlias(bc.Aliases, bc.Name, defaultLanguage, userLanguage),
                        BlogsCount = bc.BlogBlogCategories.Count,
                        WithoutThisIds = categories.Contains(bc.Id) ? categories.Where(tag => tag != bc.Id) : categories.Concat(new List<int> { bc.Id })
                    }).ToListAsync();
            return result;
        }

        public Task<List<BlogsFeedComponentLatestBlogDao>> GetBlogsFeedLatestBlogs()
        {
            return Context.Blogs.OrderByDescending(b => b.CreatedOn)
                .Select(b =>
                    new BlogsFeedComponentLatestBlogDao
                    {
                        Id = b.Id,
                        Caption = b.Caption,
                        CreatedOn = b.CreatedOn,
                        HeadPhotoPath = ""//TODO извлечь преамбулу
                    }).Take(4).ToListAsync();
        }

        public int GetPlaceStars(IEnumerable<PlaceReview> reviews)
        {
            return reviews != null && reviews.Any() ? (int) Math.Ceiling(reviews.Average(r => r.Star)) : 0;
        }

        public string GetAddress(string cityName, string streetName, string address)
        {
            return $"{streetName} {address}, {cityName}";
        }

        public string GetUsername(User user)
        {
            if (user == null)
            {
                return string.Empty;
            }
            return user.Name != null && user.Surname != null ? $"{user.Name} {user.Surname}" : user.UserName;
        }

        #region AliasesServices
        private IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, Language defaultLanguage, Language userLanguage) where T : BaseAlias
        {
            return GetProperAliases(aliases, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        private IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, string name, Language defaultLanguage, Language userLanguage) where T : BaseAlias
        {
            return GetProperAliases(aliases, name, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        private string GetProperAlias<T>(IEnumerable<T> aliases, string name, Language defaultLanguage, Language userLanguage) where T : BaseAlias
        {
            return GetProperAlias(aliases, name, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        private string GetProperAlias<T>(IEnumerable<T> aliases, Language defaultLanguage, Language userLanguage) where T : BaseAlias
        {
            return GetProperAlias(aliases, defaultLanguage?.Id ?? 0, userLanguage?.Id ?? 0);
        }
        private IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, int defaultLanguageId, int userLanguageId) where T : BaseAlias
        {
            var result = new List<string>();
            result.AddRange(aliases.Where(a => a.LanguageId == userLanguageId).Select(a => a.Text));
            if (!result.Any())
            {
                result.AddRange(aliases.Where(a => a.LanguageId == defaultLanguageId).Select(a => a.Text));
            }
            return result;
        }
        private IEnumerable<string> GetProperAliases<T>(IEnumerable<T> aliases, string name, int defaultLanguageId, int userLanguageId) where T : BaseAlias
        {
            var result = GetProperAliases(aliases, defaultLanguageId, userLanguageId).ToList();
            if (!result.Any())
            {
                result.Add(name);
            }
            return result;
        }
        private string GetProperAlias<T>(IEnumerable<T> aliases, string name, int defaultLanguageId, int userLanguageId) where T : BaseAlias
        {
            return GetProperAliases(aliases, name, defaultLanguageId, userLanguageId).FirstOrDefault();
        }
        private string GetProperAlias<T>(IEnumerable<T> aliases, int defaultLanguageId, int userLanguageId) where T : BaseAlias
        {
            return GetProperAliases(aliases, defaultLanguageId, userLanguageId).FirstOrDefault();
        }
        #endregion
    }
}
