using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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
                places = places.Where(p => p.Categories.Select(c => c.Id)
                    .Intersect(categories).Any()).ToList();
            }
            if (!string.IsNullOrEmpty(inputNormalized))
            {
                places = places.Where(p => p.Names.Any(a => a.Name.ToUpper().Contains(inputNormalized))).ToList();
            }
            var userLanguage = await DalServices.GetUserLanguageAsync(httpContextUser);
            var defaultLanguage = await DalServices.GetDefaultLanguageAsync();
            foreach (var place in places)
            {
                var placesCityNames = place.City.Names
                    .Where(n => n.LanguageId == userLanguage.Id)
                    .ToList();
                if (!placesCityNames.Any())
                {
                    placesCityNames.AddRange(place.City.Names
                        .Where(n => n.LanguageId == defaultLanguage.Id));
                }
                if (!placesCityNames.Any())
                {
                    placesCityNames.Add(new CachedCityNameDto {Name = place.City.Name});
                }
                place.City.Names = placesCityNames;

                var placesNames = place.Names
                    .Where(n => n.LanguageId == userLanguage.Id)
                    .ToList();
                if (!placesNames.Any())
                {
                    placesNames.AddRange(place.Names
                        .Where(n => n.LanguageId == defaultLanguage.Id));
                }
                if (!placesNames.Any())
                {
                    placesNames.Add(new CachedPlaceNameDto { Name = place.Name });
                }
                place.Names = placesNames;

                foreach (var category in place.Categories)
                {
                    var categoryNames = category.Names
                        .Where(n => n.LanguageId == userLanguage.Id)
                        .ToList();
                    if (!categoryNames.Any())
                    {
                        categoryNames.AddRange(category.Names
                            .Where(n => n.LanguageId == defaultLanguage.Id));
                    }
                    if (!categoryNames.Any())
                    {
                        categoryNames.Add(new CachedCategoryNameDto { Name = category.Name });
                    }
                    category.Names = categoryNames;
                }
            }
            switch ((PlaceOrderByEnum)orderBy)
            {
                case PlaceOrderByEnum.Category:
                    places = isDescending
                        ? places.OrderByDescending(p =>
                            p.Categories.SelectMany(c => c.Names.Select(n => n.Name))
                                .OrderByDescending(n => n).FirstOrDefault()).ToList()
                        : places.OrderBy(p => 
                            p.Categories.SelectMany(c => c.Names.Select(n => n.Name))
                                .OrderBy(n => n).FirstOrDefault()).ToList();
                    break;
                case PlaceOrderByEnum.Location:
                    places = isDescending
                        ? places.OrderByDescending(p => p.City.Names.OrderByDescending(n => n.Name).First().Name).ToList()
                        : places.OrderBy(p => p.City.Names.OrderBy(n => n.Name).First().Name).ToList();
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
                        ? places.OrderByDescending(p => p.Names.Select(n => n.Name).OrderByDescending(n => n).FirstOrDefault()).ToList()
                        : places.OrderBy(p => p.Names.Select(n => n.Name).OrderBy(n => n).FirstOrDefault()).ToList();
                    break;
            }
            places = places.Skip((page - 1) * placesPerPageCount).Take(placesPerPageCount).ToList();
            return places.Select(p => p.Id);
        }

        public async Task<BlogPageDto> GetBlogAsync(ClaimsPrincipal user, int id)
        {
            var result = await DalServices.GetBlogAsync(user, id);
            return result.To<BlogPageDto>();
        }

        public async Task<int> GetPlacesCountAsync(int[] cities, int[] categories, string input)
        {
            var inputNormalized = input?.Trim().ToUpper();
            if (!_memoryCache.TryGetValue(PlacesCacheKey, out IEnumerable<CachedPlaceDto> places))
            {
                places = (await DalServices.GetPlacesAsync()).To<IEnumerable<CachedPlaceDto>>();
                _memoryCache.Set(PlacesCacheKey, places);
            }
            if (cities.Any())
            {
                places = places.Where(p => cities.Contains(p.City.Id)).ToList();
            }
            if (categories.Any())
            {
                places = places.Where(p => p.Categories.Select(c => c.Id)
                    .Intersect(categories).Any());
            }
            if (!string.IsNullOrEmpty(inputNormalized))
            {
                places = places.Where(p => p.Names.Any(n => n.Name.ToUpper().Contains(inputNormalized)));
            }
            return places.Count();
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

        public async Task<NewBlogPageDto> GetNewBlogDtoAsync()
        {
            var result = await DalServices.GetNewBlogDaoAsync();
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
                    : new List<string> {""}
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

        public async Task<EditBlogPageDao> GetEditBlogAsync(ClaimsPrincipal httpContextUser, int id)
        {
            var result = await DalServices.GetEditBlogAsync(httpContextUser, id);
            return result.To<EditBlogPageDao>();
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
    }
}
