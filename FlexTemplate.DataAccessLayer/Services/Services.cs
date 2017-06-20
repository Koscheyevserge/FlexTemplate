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
                var alias = aliasesGroup.FirstOrDefault(a => userLanguage != null && a.Language == userLanguage) 
                    ?? aliasesGroup.FirstOrDefault(a => a.Language == defaultLanguage);
                aliases.Add(new KeyValuePair<int, string>(alias.CityId, alias.Text));
            }
            var result = Context.Cities.Select(c => 
                new CityChecklistItemDao
                {
                    Id = c.Id,
                    Name = aliases.Where(a => a.Key == c.Id).Select(kvp => kvp.Value).FirstOrDefault() ?? c.Name,
                    Checked = checkedCities.Contains(c.Id),
                    CitiesWithoutThisIds = checkedCities.Where(checkedCity => checkedCity != c.Id)
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
                var alias = aliasesGroup.FirstOrDefault(a => userLanguage != null && a.Language == userLanguage) ?? aliasesGroup.FirstOrDefault(a => a.Language == defaultLanguage);
                aliases.Add(new KeyValuePair<int, string>(alias.PlaceCategoryId, alias.Text));
            }
            var result = Context.PlaceCategories.Select(c => new PlaceCategoryChecklistItemDao
            {
                Id = c.Id,
                Name = aliases.Where(a => a.Key == c.Id).Select(kvp => kvp.Value).FirstOrDefault() ?? c.Name,
                Checked = checkedCategories.Contains(c.Id),
                CategoriesWithoutThisIds = checkedCategories.Where(checkedCategory => checkedCategory != c.Id)
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
                var alias = aliasesGroup.FirstOrDefault(a => userLanguage != null && a.Language == userLanguage) 
                    ?? aliasesGroup.FirstOrDefault(a => a.Language == defaultLanguage);
                aliases.Add(new KeyValuePair<int, string>(alias.PlaceId, alias.Text));
            }
            var categories = Context.Places
                .Include(p => p.PlacePlaceCategories)
                .ThenInclude(ppc => ppc.PlaceCategory)
                .ThenInclude(pc => pc.Aliases)
                .ThenInclude(a => a.Language)
                .Select(p => 
                new GetPlacesListItemPlaceDao
                {
                    PlaceId = p.Id,
                    Categories = p.PlacePlaceCategories.Select(ppc => 
                    new GetPlacesListItemPlaceCategoryDao
                    {
                        CategoryId = ppc.PlaceCategoryId,
                        CategoryAlias = ppc.PlaceCategory.Aliases.FirstOrDefault(a => userLanguage != null && a.Language == userLanguage)
                                        ?? ppc.PlaceCategory.Aliases.FirstOrDefault(a => a.Language == defaultLanguage),
                        CategoryName = ppc.PlaceCategory.Name
                    })
                })
                .Select(getPlacesListItemPlaceDao => 
                    new KeyValuePair<int, IEnumerable<KeyValuePair<int, string>>>
                    (
                        getPlacesListItemPlaceDao.PlaceId, 
                        getPlacesListItemPlaceDao.Categories.Select
                        (
                            cat => 
                            new KeyValuePair<int, string>
                            (
                                cat.CategoryId,
                                cat.CategoryAlias != null ? cat.CategoryAlias.Text : cat.CategoryName
                            )
                        )
                    )
                );
            var getPlacesListItemAddressDaos = Context.Places
                .Include(p => p.Street)
                .ThenInclude(p => p.City)
                .ThenInclude(c => c.Aliases)
                .Include(p => p.Street)
                .ThenInclude(s => s.Aliases)
                .Select(p => 
                    new GetPlacesListItemAddressDao
                    { 
                        PlaceId = p.Id,
                        Address = p.Address,
                        CityName = p.Street.City.Name,
                        StreetName = p.Street.Name,
                        CityAlias = p.Street.City.Aliases
                            .FirstOrDefault(a => userLanguage != null && a.Language == userLanguage)
                                ?? p.Street.City.Aliases.FirstOrDefault(a => a.Language == defaultLanguage),
                        StreetAlias = p.Street.Aliases
                            .FirstOrDefault(a => userLanguage != null && a.Language == userLanguage)
                                ?? p.Street.Aliases.FirstOrDefault(a => a.Language == defaultLanguage)
                    }
                );
            var addresses = getPlacesListItemAddressDaos.Select(a => new KeyValuePair<int, string>
                (
                    a.PlaceId,
                    string.Format("{0} {1}, {2}", 
                    a.Address, 
                    a.StreetAlias != null ? a.StreetAlias.Text : a.StreetName,
                    a.CityAlias != null ? a.CityAlias.Text : a.CityName)
                )
            );
            var result = Context.Places
                .Include(p => p.Reviews)
                .Include(p => p.Menus).ThenInclude(m => m.Products)
                .Select(p => 
                new PlaceListItemDao
                {
                    Id = p.Id,
                    Name = aliases.Where(a => a.Key == p.Id).Select(kvp => kvp.Value).FirstOrDefault() 
                        ?? p.Name,
                    Categories = categories.Any() 
                        ? categories.SingleOrDefault(a => a.Key == p.Id).Value
                        : new List<KeyValuePair<int, string>>(),
                    Stars = p.Reviews.Any() 
                        ? (int) Math.Ceiling(p.Reviews.Average(r => r.Star))
                        : 0,
                    ReviewsCount = p.Reviews.Count,
                    //TODO Address = addresses.Single(a => a.Key == p.Id).Value,
                    HeadPhoto = "",
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
            var userName = string.Empty;
            if (isLogined)
            {
                userName = user.Name != null && user.Surname != null 
                    ? $"{user.Name} {user.Surname}" 
                    : user.UserName;
            }
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
                .Include(p => p.PlacePlaceCategories).ThenInclude(ppc => ppc.PlaceCategory).ThenInclude(pc => pc.Aliases)
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
            if (user == null)
            {
                return false;
            }
            return Context.GetSet<T>().Any(p => p.User == user && p.Id == placeId);
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
    }
}
