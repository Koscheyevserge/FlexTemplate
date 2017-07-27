using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.DataAccessLayer.Entities;
using FlexTemplate.BusinessLogicLayer.Enums;
using FlexTemplate.DataAccessLayer.DataAccessObjects;
using Microsoft.Extensions.Caching.Memory;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public class ControllerServices
    {
        private readonly IMemoryCache _memoryCache;
        private DataAccessLayer.Services.Services DalServices { get; }
        private const string PlacesCacheKey = "PLACES_CACHE";
        private const string BlogsCacheKey = "BLOGS_CACHE";

        public ControllerServices(DataAccessLayer.Services.Services dalServices, IMemoryCache memoryCache)
        {
            DalServices = dalServices;
            _memoryCache = memoryCache;
        }

        public async Task<PageContainersHierarchyDto> GetPageContainersHierarchyAsync(string pageName)
        {
            var pageContainersHierarchyDto =
                await DalServices.GetPageContainersHierarchy(pageName);
            return pageContainersHierarchyDto.To<PageContainersHierarchyDto>();
        }

        public async Task<IEnumerable<int>> GetPlacesAsync(ClaimsPrincipal httpContextUser, int[] cities,
            int[] categories, string input, int page = 1, int orderBy = 1, bool isDescending = false)
        {
            var placesPerPageCount = await CommonServices.GetPlacesPerPageCountAsync();
            var inputNormalized = input?.Trim().ToUpper();
            if (!_memoryCache.TryGetValue(PlacesCacheKey, out List<CachedPlaceDto> places) || places == null)
            {
                var placesDao = await DalServices.GetPlacesAsync();
                places = placesDao.To<List<CachedPlaceDto>>();
                _memoryCache.Set(PlacesCacheKey, places, TimeSpan.FromMinutes(1));
            }
            if (cities.Any())
            {
                places = places.Where(p => cities.Contains(p.City.Id)).ToList();
            }
            if (categories.Any())
            {
                places = places.Where(p => p.Categories.Select(c => c.Id).Intersect(categories).Any()).ToList();
            }
            if (!string.IsNullOrEmpty(inputNormalized))
            {
                places = places.Where(p => p.Names.Any(a => a.Name.ToUpperInvariant().Contains(inputNormalized)) 
                || p.Name.ToUpperInvariant().Contains(inputNormalized)).ToList();
            }
            var userLanguage = await DalServices.GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await DalServices.GetDefaultLanguageAsync();
            foreach (var place in places)
            {
                if (place.Categories == null)
                {
                    place.Categories = new List<CachedCategoryDto>();
                }
                place.City.Name = 
                    CommonServices.GetProperAlias(place.City.Names, place.City.Name, defaultLanguage, userLanguage);
                place.Name = CommonServices.GetProperAlias(place.Names, place.Name, defaultLanguage, userLanguage);
                foreach (var category in place.Categories)
                {
                    category.Name = 
                        CommonServices.GetProperAlias(category.Names, category.Name, defaultLanguage, userLanguage);
                }
            }
            switch ((PlaceOrderByEnum)orderBy)
            {
                case PlaceOrderByEnum.Category:
                    places = isDescending
                        ? places.OrderByDescending(p => p.Categories.OrderByDescending(c => c.Name)
                            .FirstOrDefault()?.Name).ToList()
                        : places.OrderBy(p => p.Categories.OrderBy(c => c.Name).FirstOrDefault()?.Name).ToList();
                    break;
                case PlaceOrderByEnum.Location:
                    places = isDescending
                        ? places.OrderByDescending(p => p.City.Name).ToList()
                        : places.OrderBy(p => p.City.Name).ToList();
                    break;
                case PlaceOrderByEnum.Popularity:
                    places = isDescending
                        ? places.OrderByDescending(p => p.ViewsCount).ToList()
                        : places.OrderBy(p => p.ViewsCount).ToList();
                    break;
                case PlaceOrderByEnum.Rating:
                    places = isDescending
                        ? places.OrderByDescending(p => p.Rating).ToList()
                        : places.OrderBy(p => p.Rating).ToList();
                    break;
                //TODO PlaceOrderByEnum.Price
                case PlaceOrderByEnum.Name:
                default:
                    places = isDescending
                        ? places.OrderByDescending(p => p.Name).ToList()
                        : places.OrderBy(p => p.Name).ToList();
                    break;
            }
            return places.Skip((page - 1) * placesPerPageCount)
                .Take(placesPerPageCount).Select(p => p.Id).ToList();    
        }

        public async Task<BlogPageDto> GetBlogAsync(ClaimsPrincipal user, int id)
        {
            var result = await DalServices.GetBlogAsync(user, id);
            return result.To<BlogPageDto>();
        }

        public async Task<int> GetPlacesCountAsync(int[] cities, int[] categories, string input)
        {
            var inputNormalized = input?.Trim().ToUpper();
            if (!_memoryCache.TryGetValue(PlacesCacheKey, out List<CachedPlaceDto> places))
            {
                places = (await DalServices.GetPlacesAsync()).To<List<CachedPlaceDto>>();
                _memoryCache.Set(PlacesCacheKey, places);
            }
            if (cities.Any())
            {
                places = places.Where(p => cities.Contains(p.City.Id)).ToList();
            }
            if (categories.Any())
            {
                places = places.Where(p => p.Categories.Select(c => c.Id)
                    .Intersect(categories).Any()).ToList();
            }
            if (!string.IsNullOrEmpty(inputNormalized))
            {
                places = places.Where(p => p.Names.Any(n => n.Name.ToUpper().Contains(inputNormalized))).ToList();
            }
            return places.Count;
        }

        public async Task<bool> CanEditVisualsAsync(ClaimsPrincipal httpContextUser)
        {
            var canEditVisualsTask = await DalServices.CanEditVisualsAsync(httpContextUser);
            return canEditVisualsTask;
        }

        public async Task<bool> IsAuthorAsync<T>(ClaimsPrincipal httpContextUser, int placeId) where T : BaseAuthorfullEntity
        {
            var isPlaceAuthorTask = await DalServices.IsAuthorAsync<T>(httpContextUser, placeId);
            return isPlaceAuthorTask;
        }

        public async Task<CachedBlogDto> GetBlogsAsync(ClaimsPrincipal httpContextUser, int[] tags, int[] categories, string input, int currentPage = 1)
        {
            var blogs = await DalServices.GetBlogsAsync(httpContextUser, tags, categories, input, currentPage, await CommonServices.GetBlogsPerPageCountAsync());
            var result = new CachedBlogDto
            {
                HeaderPhotoPath = "",//TODO получить фото
                BlogsCount = await DalServices.GetBlogsCountAsync(httpContextUser, tags, categories, input),
                Blogs = blogs.To<IEnumerable<CachedBlogItemDto>>()
            };
            return result;
        }

        public async Task<NewPlacePageDto> GetNewPlaceDtoAsync(ClaimsPrincipal httpContextUser)
        {
            var result = await DalServices.GetNewPlaceDaoAsync(httpContextUser);
            return result.To<NewPlacePageDto>();
        }

        public async Task<NewBlogPageDto> GetNewBlogDtoAsync(ClaimsPrincipal httpContextUser)
        {
            var result = await DalServices.GetNewBlogDaoAsync(httpContextUser);
            return result.To<NewBlogPageDto>();
        }

        public async Task<int> CreateBlogAsync(ClaimsPrincipal claims, CreateBlogDto blogDto)
        {
            var blogDao = new CreateBlogDao
            {
                Name = blogDto.Name,
                Text = blogDto.Text,
                Tags = blogDto.Tags != null && blogDto.Tags.Any() 
                    ? blogDto.Tags.Split(',').Select(tag => tag.Trim()).ToList()
                    : new List<string> {""},
                BannersKey = blogDto.BannersKey,
                Categories = blogDto.Categories
            };
            return await DalServices.CreateBlogAsync(claims, blogDao);
        }

        public Task<bool> AddCommentAsync(ClaimsPrincipal claims, NewBlogCommentDto model)
        {
            return DalServices.AddCommentAsync(claims, model.To<NewBlogCommentDao>());
        }

        public Task<bool> LoginAsync(string username, string password, bool remember, bool lockOnFailure)
        {
            return DalServices.LoginAsync(username, password, remember, lockOnFailure);
        }

        public Task LogoutAsync()
        {
            return DalServices.LogoutAsync();
        }

        public Task<bool> AddReviewAsync(ClaimsPrincipal claims, NewPlaceReviewDto model)
        {
            return DalServices.AddReviewAsync(claims, model.To<NewPlaceReviewDao>());
        }

        public Task<int> CreatePlaceAsync(ClaimsPrincipal claims, NewPlaceDto model)
        {
            model.City = string.IsNullOrEmpty(model.City) 
                ? model.City.Trim().ToUpperInvariant() 
                : string.Empty;
            model.Street = string.IsNullOrEmpty(model.Street) 
                ? model.Street.Trim().ToUpperInvariant() 
                : string.Empty;
            model.Categories = model.Categories ?? new List<int>();
            return DalServices.CreatePlaceAsync(claims, model.To<NewPlaceDao>());
        }

        public async Task<EditBlogPageDto> GetEditBlogAsync(ClaimsPrincipal httpContextUser, int id)
        {
            var result = await DalServices.GetEditBlogAsync(httpContextUser, id);
            return result.To<EditBlogPageDto>();
        }

        public Task<bool> EditBlogAsync(EditBlogDto model)
        {
            return DalServices.EditBlogAsync(model.To<EditBlogDao>());
        }

        public async Task<EditPlacePageDto> GetEditPlaceAsync(ClaimsPrincipal claims, int id)
        {
            var result = await DalServices.GetEditPlaceAsync(claims, id);
            return result.To<EditPlacePageDto>();
        }

        public Task<bool> EditPlaceAsync(EditPlaceDto model)
        {
            return DalServices.EditPlaceAsync(model.To<EditPlaceDao>());
        }

        public async Task<IEnumerable<string>> GetCitiesAsync()
        {
            var result = await DalServices.GetCitiesAsync();
            return result;
        }

        public async Task<IEnumerable<string>> GetTagsAsync()
        {
            var result = await DalServices.GetTagsAsync();
            return result;
        }

        public async Task<bool> DeclineBlogAsync(ClaimsPrincipal claims, int id)
        {
            var result = await DalServices.DeclineBlogAsync(claims, id);
            return result;
        }

        public async Task<bool> AcceptBlogAsync(ClaimsPrincipal claims, int id)
        {
            var result = await DalServices.AcceptBlogAsync(claims, id);
            return result;
        }
    }
}
