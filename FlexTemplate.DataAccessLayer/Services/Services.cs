using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.DataAccessLayer.DataAccessObjects;
using FlexTemplate.DataAccessLayer.Entities;
using FlexTemplate.DataAccessLayer.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace FlexTemplate.DataAccessLayer.Services
{
    public class Services
    {
        private FlexContext Context { get; }
        private UserManager<User> UserManager { get; }
        private SignInManager<User> SignInManager { get; }

        public Services(FlexContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            Context = context;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public async Task<IEnumerable<CityChecklistItemDao>> GetCityChecklistItemsAsync(
            ClaimsPrincipal claimsPrincipal, IEnumerable<int> checkedCities)
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
            var checkedCitiesList = checkedCities.ToList();
            var result = Context.Cities.Select(c => 
                new CityChecklistItemDao
                {
                    Id = c.Id,
                    Name = aliases.Where(a => a.Key == c.Id).Select(kvp => kvp.Value).FirstOrDefault() ?? c.Name,
                    Checked = checkedCitiesList.Contains(c.Id),
                    CitiesWithoutThisIds = checkedCitiesList.Contains(c.Id) 
                        ? checkedCitiesList.Where(checkedCity => checkedCity != c.Id) 
                        : checkedCitiesList.Concat(new List<int>{c.Id})
                });
            return result;
        }

        public async Task<IEnumerable<PlaceCategoryChecklistItemDao>> GetPlaceCategoriesChecklistItemsAsync(
            ClaimsPrincipal claimsPrincipal, IEnumerable<int> checkedCategories)
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
            var checkedCategoriesList = checkedCategories.ToList();
            var result = Context.PlaceCategories.Select(c => new PlaceCategoryChecklistItemDao
            {
                Id = c.Id,
                Name = aliases.Where(a => a.Key == c.Id).Select(kvp => kvp.Value).FirstOrDefault() ?? c.Name,
                Checked = checkedCategoriesList.Contains(c.Id),
                CategoriesWithoutThisIds = checkedCategoriesList.Contains(c.Id) 
                    ? checkedCategoriesList.Where(checkedCategory => checkedCategory != c.Id) 
                    : checkedCategoriesList.Concat(new List<int>{c.Id})
            });
            return result;
        }

        public async Task<IEnumerable<PlaceListItemDao>> GetPlacesListAsync(ClaimsPrincipal claimsPrincipal, 
            IEnumerable<int> placesIds)
        {
            var userLanguage = await GetUserLanguageAsync(claimsPrincipal);
            var defaultLanguage = await GetDefaultLanguageAsync();
            return await Context.Places
                .Include(p => p.Aliases)
                .Include(p => p.Reviews)
                .Include(p => p.Menus).ThenInclude(m => m.Products)
                .Include(p => p.PlacePlaceCategories).ThenInclude(ppc => ppc.PlaceCategory)
                .ThenInclude(pc => pc.Aliases)
                .Include(p => p.Street).ThenInclude(p => p.City).ThenInclude(c => c.Aliases)
                .Include(p => p.Street).ThenInclude(s => s.Aliases)
                .Where(p => placesIds.Contains(p.Id))
                .Select(p => 
                new PlaceListItemDao
                {
                    Id = p.Id,
                    Name = GetProperAlias(p.Aliases, p.Name, defaultLanguage, userLanguage),
                    Categories = p.PlacePlaceCategories.Select(ppc => new KeyValuePair<int, string> 
                        (ppc.PlaceCategoryId, GetProperAlias(ppc.PlaceCategory.Aliases, ppc.PlaceCategory.Name, 
                            defaultLanguage, userLanguage))),
                    Stars = GetPlaceStars(p.Reviews),
                    ReviewsCount = p.Reviews.Count,
                    Address = GetAddress(GetProperAlias(p.Street.City.Aliases, p.Street.City.Name, defaultLanguage, 
                        userLanguage), GetProperAlias(p.Street.Aliases, p.Street.Name, defaultLanguage, userLanguage), p.Address),
                    HeadPhoto = "",//TODO получить фото
                    AveragePrice = p.Menus.Any() 
                        ? p.Menus.Where(m => m.Products.Any()).Average(m => m.Products.Average(prod => prod.Price)) 
                        : 0,
                    Description = p.Description
                }).ToListAsync();
        }

        private IQueryable<ContainerLocalizableString> GetLocalizableStrings(Container container, 
            Language defaultLanguage, Language userLanguage)
        {
            return Context.ContainerLocalizableStrings
                .Where(cls => cls.Container == container && (cls.Language == defaultLanguage || 
                    (userLanguage != null && cls.Language == userLanguage)))
                .GroupBy(cls => cls.Tag)
                .Select(cls => userLanguage != null && cls.Any(c => c.Language == userLanguage) 
                    ? cls.FirstOrDefault(c => c.Language == userLanguage) 
                    : cls.FirstOrDefault(c => c.Language == defaultLanguage));
        }

        #region Views

        public async Task<EditPlacePageDao> GetEditPlaceAsync(ClaimsPrincipal claims, int id)
        {
            var userLanguage = await GetUserLanguageAsync(claims);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var placeCategories = await Context.PlacePlaceCategories.Where(ppc => ppc.PlaceId == id)
                .Select(ppc => ppc.PlaceCategoryId).ToListAsync();
            var categories = await Context.PlaceCategories.Include(pc => pc.Aliases)
                .Select(pc => 
                new EditPlacePageCategoryDao
                {
                    Id= pc.Id,
                    Name = GetProperAlias(pc.Aliases, pc.Name, defaultLanguage, userLanguage),
                    Checked = placeCategories.Contains(pc.Id)
                }).ToListAsync();
            var placeHasSchedule = await Context.PlaceSchedules.AnyAsync(ps => ps.PlaceId == id);
            const string zero = "00:00";
            return await Context.Places
                .Include(p => p.Aliases)
                .Include(p => p.Schedule)
                .Include(p => p.Street).ThenInclude(s => s.Aliases)
                .Include(p => p.Street).ThenInclude(s => s.City).ThenInclude(c => c.Aliases)
                .Include(p => p.FeatureColumns).ThenInclude(fc => fc.Features)
                .Include(p => p.Menus).ThenInclude(m => m.Products)
                .Include(p => p.Communications)
                .Where(p => p.Id == id)
                .Select(p => 
                new EditPlacePageDao
                {
                    HasNoMenus = !p.Menus.Any(),
                    Name = p.Name,
                    Features = p.FeatureColumns.OrderBy(fc => fc.Position)
                        .Select(fc => fc.Features.OrderBy(f => f.Row).Select(f => f.Name).ToArray()).ToArray(),
                    Phone = GetCommunicationNumber(p.Communications, CommunicationType.Phone),
                    Email = GetCommunicationNumber(p.Communications, CommunicationType.Email),
                    Website = GetCommunicationNumber(p.Communications, CommunicationType.Website),
                    City = GetProperAlias(p.Street.City.Aliases, p.Street.City.Name, defaultLanguage, userLanguage),
                    Categories = categories,
                    Description = p.Description,
                    MondayFrom = placeHasSchedule 
                        ? p.Schedule.MondayFrom.ToString(@"hh\:mm") 
                        : zero,
                    MondayTo = placeHasSchedule
                        ? p.Schedule.MondayTo.ToString(@"hh\:mm")
                        : zero,
                    TuesdayFrom = placeHasSchedule
                        ? p.Schedule.TuesdayFrom.ToString(@"hh\:mm")
                        : zero,
                    TuesdayTo = placeHasSchedule
                        ? p.Schedule.TuesdayTo.ToString(@"hh\:mm")
                        : zero,
                    WednesdayFrom = placeHasSchedule
                        ? p.Schedule.WednesdayFrom.ToString(@"hh\:mm")
                        : zero,
                    WednesdayTo = placeHasSchedule
                        ? p.Schedule.WednesdayTo.ToString(@"hh\:mm")
                        : zero,
                    ThursdayFrom = placeHasSchedule
                        ? p.Schedule.ThursdayFrom.ToString(@"hh\:mm")
                        : zero,
                    ThursdayTo = placeHasSchedule
                        ? p.Schedule.ThursdayTo.ToString(@"hh\:mm")
                        : zero,
                    FridayFrom = placeHasSchedule
                        ? p.Schedule.FridayFrom.ToString(@"hh\:mm")
                        : zero,
                    FridayTo = placeHasSchedule
                        ? p.Schedule.FridayTo.ToString(@"hh\:mm")
                        : zero,
                    SaturdayFrom = placeHasSchedule
                        ? p.Schedule.SaturdayFrom.ToString(@"hh\:mm")
                        : zero,
                    SaturdayTo = placeHasSchedule
                        ? p.Schedule.SaturdayTo.ToString(@"hh\:mm")
                        : zero,
                    SundayFrom = placeHasSchedule
                        ? p.Schedule.SundayFrom.ToString(@"hh\:mm")
                        : zero,
                    SundayTo = placeHasSchedule
                        ? p.Schedule.SundayTo.ToString(@"hh\:mm")
                        : zero,
                    Menus = p.Menus.Select(m => 
                    new EditPlacePageMenuDao
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Products = m.Products.Select(prod => 
                        new EditPlacePageProductDao
                        {
                            Id = prod.Id,
                            Name = prod.Title,
                            Description = prod.Description,
                            Price = prod.Price,
                            HasPhoto = false//TODO добавить проверку фото
                        }).ToList()
                    }).ToList(),
                    Street = GetProperAlias(p.Street.Aliases, p.Street.Name, defaultLanguage, userLanguage),
                    Longitude = p.Longitude.ToString("R"),
                    Latitude = p.Latitude.ToString("R"),
                    Address = p.Address
                }).SingleOrDefaultAsync();
        }

        public async Task<EditBlogPageDao> GetEditBlogAsync(ClaimsPrincipal claims, int id)
        {
            var userLanguage = await GetUserLanguageAsync(claims);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = await Context.Blogs
                .Include(b => b.BlogTags).ThenInclude(bt => bt.Tag).ThenInclude(t => t.TagAliases)
                .Where(b => b.Id == id).Select(b => 
                new EditBlogPageDao
                {
                    BannerPhotoPath = "",
                    Name = b.Caption,
                    Text = b.Text,
                    Id = b.Id,
                    Tags = string.Join(",", b.BlogTags.Select(bt => GetProperAlias(bt.Tag.TagAliases, bt.Tag.Name, defaultLanguage, userLanguage)))
                }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<BlogPageDao> GetBlogAsync(ClaimsPrincipal claims, int id)
        {
            var user = await UserManager.GetUserAsync(claims);
            var userLanguage = await GetUserLanguageAsync(claims);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var isAuthor = await IsAuthorAsync<Blog>(claims, id);
            var tags = await Context.BlogTags.Include(bt => bt.Tag).ThenInclude(t => t.TagAliases)
                .Where(bt => bt.BlogId == id).Select(bt =>
                    new TagBlogPageDao
                    {
                        Id = bt.TagId,
                        Name = GetProperAlias(bt.Tag.TagAliases, bt.Tag.Name, defaultLanguage, userLanguage)
                    }).ToListAsync();
            return await Context.Blogs.Select(b =>
                new BlogPageDao
                {
                    AuthorDisplayName = GetUsername(user),
                    AuthorPhotoPath = "",//TODO реализовать получение фотографии
                    BannerPath = "",//TODO реализовать получение фотографии
                    CreatedOn = b.CreatedOn,
                    Id = b.Id,
                    IsAuthor = isAuthor,
                    IsModerated = b.IsModerated,
                    IsModeratedText = "",//TODO добавить текст модерации
                    Name = b.Caption,
                    Tags = tags,
                    Text = b.Text
                }).SingleOrDefaultAsync(b => b.Id == id && (b.IsModerated || isAuthor));
        }

        #endregion

        #region ViewComponents

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
            var tagsList = tags.ToList();
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = await Context.Tags.Include(t => t.TagAliases)
                .Select(t =>
                    new BlogsFeedComponentTagDao
                    {
                        Name = GetProperAlias(t.TagAliases, t.Name, defaultLanguage, userLanguage),
                        WithoutThisIds = tagsList.Contains(t.Id)
                            ? tagsList.Where(tag => tag != t.Id)
                            : tagsList.Concat(new List<int> { t.Id })
                    }).ToListAsync();
            return result;
        }

        public async Task<List<BlogsFeedComponentCategoryDao>> GetBlogsFeedCategories(ClaimsPrincipal httpContextUser, IEnumerable<int> categories, string input)
        {
            var categoriesList = categories.ToList();
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            var result = await Context.BlogCategories.Include(bc => bc.Aliases)
                .Select(bc =>
                    new BlogsFeedComponentCategoryDao
                    {
                        Caption = GetProperAlias(bc.Aliases, bc.Name, defaultLanguage, userLanguage),
                        BlogsCount = bc.BlogBlogCategories.Count,
                        WithoutThisIds = categoriesList.Contains(bc.Id) ? categoriesList.Where(tag => tag != bc.Id) : categoriesList.Concat(new List<int> { bc.Id })
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

        public async Task<BlogCommentsComponentDao> GetBlogCommentsAsync(ClaimsPrincipal claims, int blogId)
        {
            return new BlogCommentsComponentDao
            {
                CommentsCount = await Context.BlogComments.Where(bc => bc.BlogId == blogId).CountAsync(),
                Comments = await Context.BlogComments.Where(bc => bc.BlogId == blogId).Select(bc =>
                    new BlogCommentsComponentCommentDao
                    {
                        AuthorPhotoPath = "",//TODO получить фото
                        AuthorUsername = GetUsername(bc.User),
                        CreatedOn = bc.CreatedOn,
                        Text = bc.Text
                    }).ToListAsync()
            };
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

        public async Task<HeaderViewComponentDao> GetHeaderViewComponentDaoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = Context.Languages.Single(l => l.IsDefault);
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
                .Include(p => p.PlacePlaceCategories)
                    .ThenInclude(ppc => ppc.PlaceCategory)
                        .ThenInclude(pc => pc.Aliases)
                .Include(p => p.FeatureColumns)
                    .ThenInclude(fc => fc.Features)
                .Include(p => p.Schedule)
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
                        Email = GetCommunicationNumber(p.Communications, CommunicationType.Email),
                        HasSchedule = p.Schedule != null &&
                            !(p.Schedule.MondayFrom == TimeSpan.Zero && 
                            p.Schedule.MondayTo == TimeSpan.Zero &&
                            p.Schedule.TuesdayFrom == TimeSpan.Zero && 
                            p.Schedule.TuesdayTo == TimeSpan.Zero &&
                            p.Schedule.WednesdayFrom == TimeSpan.Zero && 
                            p.Schedule.WednesdayTo == TimeSpan.Zero &&
                            p.Schedule.ThursdayFrom == TimeSpan.Zero && 
                            p.Schedule.ThursdayTo == TimeSpan.Zero &&
                            p.Schedule.FridayFrom == TimeSpan.Zero && 
                            p.Schedule.FridayTo == TimeSpan.Zero &&
                            p.Schedule.SaturdayFrom == TimeSpan.Zero && 
                            p.Schedule.SaturdayTo == TimeSpan.Zero &&
                            p.Schedule.SundayFrom == TimeSpan.Zero && 
                            p.Schedule.SundayTo == TimeSpan.Zero),
                        Phone = GetCommunicationNumber(p.Communications, CommunicationType.Phone),
                        PlaceCategoriesEnumerated = string.Join(",", p.PlacePlaceCategories
                            .Select(ppc =>
                                GetProperAlias(ppc.PlaceCategory.Aliases,
                                    ppc.PlaceCategory.Name,
                                    defaultLanguage,
                                    userLanguage))),
                        RowsOfFeatures = p.FeatureColumns.OrderBy(fc => fc.Position)
                            .Select(fc => string.Join(",", fc.Features.OrderBy(f => f.Row).Select(f => f.Name)))
                            .ToArray(),
                        Website = GetCommunicationNumber(p.Communications, CommunicationType.Website)
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

        #endregion

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
            return new PageContainersHierarchyDao { Containers = containers };
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
            return new PageContainersHierarchyDao {Containers = containers};
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

        public Task<int> GetBlogsCountAsync(ClaimsPrincipal claims, int[] tags, int[] categories, string input)
        {
            var blogs = Context.Blogs.Include(b => b.BlogTags)
                .Include(b => b.BlogBlogCategories).Where(b => b.IsModerated);
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

        public async Task<List<CachedBlogItemDao>> GetBlogsAsync(ClaimsPrincipal claims, int[] tags, int[] categories, string input, int page, int blogsPerPage)
        {
            var userIsAdmin = await IsUserAdmin(claims);
            var result = Context.Blogs.Include(b => b.BlogTags).Include(b => b.BlogBlogCategories)
                .Where(b => b.IsModerated || userIsAdmin);
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
            return await result.Select(b =>
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

        public async Task<NewPlacePageDao> GetNewPlaceDaoAsync(ClaimsPrincipal httpContextUser)
        {
            var userLanguage = await GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await GetDefaultLanguageAsync();
            return new NewPlacePageDao
            {
                BannerPhotoPath = "",//TODO получить фото
                NewPlaceGuid = Guid.NewGuid(),
                Categories = Context.PlaceCategories.Include(pc => pc.Aliases).Select(pc => 
                    new NewPlacePageCategoryDao
                    {
                        Id = pc.Id,
                        Name = GetProperAlias(pc.Aliases, pc.Name, defaultLanguage, userLanguage)
                    })
            };
        }

        public async Task<NewBlogPageDao> GetNewBlogDaoAsync()
        {
            return new NewBlogPageDao
            {
                BannerPhotoPath = "",//TODO получить фото
                NewBlogGuid = Guid.NewGuid()
            };
        }

        #region Update Services

        public async Task<bool> EditBlogAsync(EditBlogDao model)
        {
            try
            {
                var blog = await Context.Blogs.SingleOrDefaultAsync(b => b.Id == model.Id);
                var tagsNormalized = model.Tags.Select(t => t.Trim().ToUpperInvariant()).ToList();
                var tags = await Context.Tags.Where(t => tagsNormalized.Contains(t.Name.Trim().ToUpperInvariant())
                    || tagsNormalized.Intersect(t.TagAliases.Select(ta => ta.Text.Trim().ToUpperInvariant())).Any())
                    .Select(t => new BlogTag { Tag = t }).ToListAsync();
                if (blog == null)
                {
                    return false;
                }
                blog.Caption = model.Name;
                blog.Text = model.Text;
                blog.BlogTags = tags;
                using (var transaction = Context.Database.BeginTransaction())
                {
                    Context.SaveChanges();
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> EditPlaceAsync(EditPlaceDao model)
        {
            try
            {
                var place = Context.Places.SingleOrDefault(p => p.Id == model.Id);
                if (place == null)
                {
                    return false;
                }
                var city = await Context.Cities
                    .FirstOrDefaultAsync(c => c.Name.Trim().ToUpperInvariant().Contains(model.City)
                    || c.Aliases.Select(a => a.Text.Trim().ToUpperInvariant()).Any(a => a.Contains(model.City)));
                var street = await Context.Streets.FirstOrDefaultAsync(s => (city == null || s.City.Id == city.Id)
                    && (s.Name.Trim().ToUpperInvariant().Contains(model.Street)
                    || s.Aliases.Select(a => a.Text.Trim().ToUpperInvariant()).Any(a => a.Contains(model.Street))));
                var communications = new List<PlaceCommunication>();
                if (string.IsNullOrEmpty(model.Website))
                {
                    communications.Add(new PlaceCommunication
                    {
                        CommunicationType = (int)CommunicationType.Website,
                        Number = model.Website
                    });
                }
                if (string.IsNullOrEmpty(model.Email))
                {
                    communications.Add(new PlaceCommunication
                    {
                        CommunicationType = (int)CommunicationType.Email,
                        Number = model.Email
                    });
                }
                if (string.IsNullOrEmpty(model.Phone))
                {
                    communications.Add(new PlaceCommunication
                    {
                        CommunicationType = (int)CommunicationType.Phone,
                        Number = model.Phone
                    });
                }
                var schedule = new PlaceSchedule
                {
                    MondayFrom = model.MondayFrom,
                    MondayTo = model.MondayTo,
                    TuesdayFrom = model.TuesdayFrom,
                    TuesdayTo = model.TuesdayTo,
                    WednesdayFrom = model.WednesdayFrom,
                    WednesdayTo = model.WednesdayTo,
                    ThursdayFrom = model.ThursdayFrom,
                    ThursdayTo = model.ThursdayTo,
                    FridayFrom = model.FridayFrom,
                    FridayTo = model.FridayTo,
                    SaturdayFrom = model.SaturdayFrom,
                    SaturdayTo = model.SaturdayTo,
                    SundayFrom = model.SundayFrom,
                    SundayTo = model.SundayTo
                };
                //TODO нормализировать фичи (удалить элементы с пустым текстом)
                var columns = new List<PlaceFeatureColumn>();
                for (var col = 0; col < model.Features.Length; col++)
                {
                    var column = new PlaceFeatureColumn
                    {
                        Features = new List<PlaceFeature>(),
                        Position = col
                    };
                    for (var row = 0; row < model.Features[col].Length; row++)
                    {
                        column.Features.Add(new PlaceFeature { Name = model.Features[col][row], Row = row });
                    }
                    columns.Add(column);
                }
                place.Address = model.Address;
                place.Communications = communications;
                place.Description = model.Description;
                place.FeatureColumns = columns;
                place.Latitude = model.Latitude;
                place.Longitude = model.Longitude;
                place.Menus = model.Menus.Select(m =>
                    new Menu
                    {
                        Name = m.Name,
                        Products = m.Products.Select(p =>
                            new Product
                            {
                                Title = p.Name,
                                Description = p.Description,
                                Price = p.Price
                            }).ToList()
                    }).ToList();
                place.ModifiedOn = DateTime.Now;
                place.Name = model.Name;
                place.PlacePlaceCategories = await Context.PlaceCategories
                    .Where(pc => model.Categories.Contains(pc.Id))
                    .Select(pc => new PlacePlaceCategory {PlaceCategory = pc}).ToListAsync();
                place.Schedule = schedule;
                place.Street = street;
                Context.Places.Add(place);
                using (var transaction = Context.Database.BeginTransaction())
                {
                    Context.SaveChanges();
                    transaction.Commit();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Create Services

        public async Task<int> CreateBlogAsync(ClaimsPrincipal claims, CreateBlogDao blogDao)
        {
            var tagsNormalized = blogDao.Tags
                .Select(bdt => bdt.Trim().ToUpperInvariant()).ToList();
            var tags = Context.Tags.Where(t => tagsNormalized.Contains(t.Name.Trim().ToUpperInvariant())
                || tagsNormalized.Intersect(t.TagAliases.Select(ta => ta.Text.Trim().ToUpperInvariant())).Any())
                    .Select(t => new BlogTag { Tag = t }).ToList();
            var blog = new Blog
            {
                Text = blogDao.Text,
                Caption = blogDao.Name,
                CreatedOn = DateTime.Now,
                User = await GetUserAsync(claims),
                BlogTags = tags,
                //TODO BlogBlogCategories = ,
                ModifiedOn = DateTime.Now,
                ViewsCount = 0,
                IsModerated = false
            };
            Context.Blogs.Add(blog);
            using (var transaction = Context.Database.BeginTransaction())
            {
                Context.SaveChanges();
                transaction.Commit();
            }
            return blog.Id;
        }

        public async Task<bool> AddCommentAsync(ClaimsPrincipal claims, NewBlogCommentDao model)
        {
            try
            {
                var blog = new BlogComment
                {
                    User = await GetUserAsync(claims),
                    Text = model.Text,
                    BlogId = model.BlogId,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsModerated = true
                };
                Context.BlogComments.Add(blog);
                using (var transaction = Context.Database.BeginTransaction())
                {
                    Context.SaveChanges();
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> AddReviewAsync(ClaimsPrincipal claims, NewPlaceReviewDao model)
        {
            try
            {
                var review = new PlaceReview
                {
                    User = await GetUserAsync(claims),
                    Text = model.Text,
                    PlaceId = model.PlaceId,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    IsModerated = true
                };
                Context.PlaceReviews.Add(review);
                using (var transaction = Context.Database.BeginTransaction())
                {
                    Context.SaveChanges();
                    transaction.Commit();
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<int> CreatePlaceAsync(ClaimsPrincipal claims, NewPlaceDao model)
        {
            try
            {
                var city = await Context.Cities
                    .FirstOrDefaultAsync(c => c.Name.Trim().ToUpperInvariant().Contains(model.City)
                    || c.Aliases.Select(a => a.Text.Trim().ToUpperInvariant()).Any(a => a.Contains(model.City)));
                var user = await GetUserAsync(claims);
                var street = await Context.Streets.FirstOrDefaultAsync(s => (city == null || s.City.Id == city.Id)
                     && (s.Name.Trim().ToUpperInvariant().Contains(model.Street)
                     || s.Aliases.Select(a => a.Text.Trim().ToUpperInvariant()).Any(a => a.Contains(model.Street))));
                var communications = new List<PlaceCommunication>();
                if (!string.IsNullOrEmpty(model.Website))
                {
                    communications.Add(new PlaceCommunication
                    {
                        CommunicationType = (int)CommunicationType.Website,
                        Number = model.Website
                    });
                }
                city = city ?? new City{ Name = model.City };
                if (!string.IsNullOrEmpty(model.Email))
                {
                    communications.Add(new PlaceCommunication
                    {
                        CommunicationType = (int)CommunicationType.Email,
                        Number = model.Email
                    });
                }
                if (!string.IsNullOrEmpty(model.Phone))
                {
                    communications.Add(new PlaceCommunication
                    {
                        CommunicationType = (int)CommunicationType.Phone,
                        Number = model.Phone
                    });
                }
                var schedule = new PlaceSchedule
                {
                    MondayFrom = model.MondayFrom,
                    MondayTo = model.MondayTo,
                    TuesdayFrom = model.TuesdayFrom,
                    TuesdayTo = model.TuesdayTo,
                    WednesdayFrom = model.WednesdayFrom,
                    WednesdayTo = model.WednesdayTo,
                    ThursdayFrom = model.ThursdayFrom,
                    ThursdayTo = model.ThursdayTo,
                    FridayFrom = model.FridayFrom,
                    FridayTo = model.FridayTo,
                    SaturdayFrom = model.SaturdayFrom,
                    SaturdayTo = model.SaturdayTo,
                    SundayFrom = model.SundayFrom,
                    SundayTo = model.SundayTo
                };
                //TODO нормализировать фичи (удалить элементы с пустым текстом)
                var columns = new List<PlaceFeatureColumn>();
                for (var col = 0; col < model.Features.Length; col++)
                {
                    var column = new PlaceFeatureColumn
                    {
                        Features = new List<PlaceFeature>(),
                        Position = col
                    };
                    for (var row = 0; row < model.Features[col].Length; row++)
                    {
                        column.Features.Add(new PlaceFeature { Name = model.Features[col][row], Row = row });
                    }
                    columns.Add(column);
                }
                street = street ?? new Street { Name = model.Street, City = city };
                var place = new Place
                {
                    User = user,
                    Address = model.Address,
                    Aliases = new List<PlaceAlias>(),
                    Communications = communications,
                    CreatedOn = DateTime.Now,
                    Description = model.Description,
                    FeatureColumns = columns,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    Menus = model.Menus.Select(m => 
                    new Menu
                    {
                        Name = m.Name,
                        Products = m.Products.Select(p => 
                        new Product
                        {
                            Title = p.Name,
                            Description = p.Description,
                            Price = p.Price
                        }).ToList()
                    }).ToList(),
                    ModifiedOn = DateTime.Now,
                    Name = model.Name,
                    PlacePlaceCategories = await Context.PlaceCategories.Where(pc => model.Categories.Contains(pc.Id))
                        .Select(pc => new PlacePlaceCategory { PlaceCategory = pc }).ToListAsync(),
                    Reviews = new List<PlaceReview>(),
                    Schedule = schedule,
                    Street = street,
                    ViewsCount = 0
                };
                Context.Places.Add(place);
                using (var transaction = Context.Database.BeginTransaction())
                {
                    Context.SaveChanges();
                    transaction.Commit();
                }
                return place.Id;
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        #region Auth Services
        public Task LogoutAsync()
        {
            return SignInManager.SignOutAsync();
        }

        public async Task<bool> LoginAsync(string username, string password, bool remember, bool lockOnFailure)
        {
            var guest = await Context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            await SignInManager.SignOutAsync();
            if (guest == null)
            {
                return false;
            }
            var result = await SignInManager.PasswordSignInAsync(guest, password, remember, lockOnFailure);
            return result.Succeeded;
        }
        #endregion

        #region Common Services

        public string GetCommunicationNumber(List<PlaceCommunication> communications, CommunicationType type)
        {
            var comm = communications.FirstOrDefault(c => c.CommunicationType == (int)type);
            return comm?.Number;
        }

        public async Task<User> GetUserAsync(ClaimsPrincipal claims)
        {
            var user = await UserManager.GetUserAsync(claims);
            if (user == null)
            {
                return null;
            }
            return await Context.Users.SingleAsync(u => u.UserName == user.UserName);
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

        public async Task<bool> IsUserAdmin(ClaimsPrincipal httpContextUser)
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            return user != null && await UserManager.IsInRoleAsync(user, "Supervisor");
        }

        public async Task<bool> IsUserModerator(ClaimsPrincipal httpContextUser)
        {
            var user = await UserManager.GetUserAsync(httpContextUser);
            return user != null && await UserManager.IsInRoleAsync(user, "Moderator");
        }

        public async Task<Language> GetUserLanguageAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await UserManager.GetUserAsync(claimsPrincipal);
            if (user == null)
                return null;
            var claims = await UserManager.GetClaimsAsync(user);
            var languageClaim = claims.FirstOrDefault(c => c.Type == "language");
            if (languageClaim == null)
                return null;
            var userLanguage = Context.Languages.SingleOrDefault(l => l.ShortName == languageClaim.Value);
            return userLanguage;
        }

        public async Task<Language> GetDefaultLanguageAsync()
        {
            var result = await Context.Languages.SingleOrDefaultAsync(l => l.IsDefault);
            return result;
        }

        public int GetPlaceStars(IEnumerable<PlaceReview> reviews)
        {
            var reviewsList = reviews.ToList();
            return reviews != null && reviewsList.Any() ? (int)Math.Ceiling(reviewsList.Average(r => r.Star)) : 0;
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

        #endregion

        #region Aliases Services
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
            var aliasesList = aliases.ToList();
            result.AddRange(aliasesList.Where(a => a.LanguageId == userLanguageId).Select(a => a.Text));
            if (!result.Any())
            {
                result.AddRange(aliasesList.Where(a => a.LanguageId == defaultLanguageId).Select(a => a.Text));
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
