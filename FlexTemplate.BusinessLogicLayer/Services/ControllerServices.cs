using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.DataAccessLayer.Entities;
using FlexTemplate.BusinessLogicLayer.Enums;
using Microsoft.Extensions.Caching.Memory;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public class ControllerServices
    {
        private readonly IMemoryCache _memoryCache;
        private DataAccessLayer.Services.Services DalServices { get; }
        private const string PlacesCacheKey = "PLACES_CACHE";

        private int GetPlacesPerPageCount()
        {
            return 12;//TODO реализовать как системную настройку
        }

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
            var inputNormalized = input.Trim().ToUpper();
            var userDao = await DalServices.GetUserAsync(httpContextUser);
            if (!_memoryCache.TryGetValue(PlacesCacheKey, out IEnumerable<CachedPlaceDto> places))
            {
                var placesDao = await DalServices.GetPlacesAsync();
                places = placesDao.To<IEnumerable<CachedPlaceDto>>();
                _memoryCache.Set(PlacesCacheKey, places);
            }
            //TODO оставить лишь алиасы с нужными языками
            if (cities.Any())
            {
                places = places.Where(p => cities.Contains(p.City.Id)).ToList();
            }
            if (categories.Any())
            {
                places = places.Where(p => p.Categories.Select(c => c.Id)
                    .Intersect(categories).Any()).ToList();
            }
            if (string.IsNullOrEmpty(inputNormalized))
            {
                places = places.Where(p => p.Names.Any(a => a.Name.ToUpper().Contains(inputNormalized))).ToList();
            }
            var user = userDao.To<UserDto>();
            foreach (var place in places)
            {
                place.City.Names = place.City.Names
                    .Where(n => n.LanguageId == user.DefaultLanguageId || n.LanguageId == user.UserLanguageId);
                place.Names = place.Names
                    .Where(n => n.LanguageId == user.DefaultLanguageId || n.LanguageId == user.UserLanguageId);
                foreach (var category in place.Categories)
                {
                    category.Names = category.Names
                        .Where(n => n.LanguageId == user.DefaultLanguageId || n.LanguageId == user.UserLanguageId);
                }
            }
            switch ((PlaceOrderByEnum)orderBy)
            {
                case PlaceOrderByEnum.Name:
                    places = isDescending
                        ? places.OrderByDescending(p => p.Names.OrderByDescending(n => n).FirstOrDefault()).ToList()
                        : places.OrderBy(p => p.Names.OrderBy(n => n).FirstOrDefault()).ToList();
                    break;
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
            }
            var placesPerPageCount = GetPlacesPerPageCount();
            places = places.Skip((page - 1) * placesPerPageCount).Take(placesPerPageCount).ToList();
            return places.Select(p => p.Id);
        }

        public async Task<int> GetPlacesCountAsync(int[] cities, int[] categories, string input)
        {
            var inputNormalized = input.Trim().ToUpper();
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
            if (string.IsNullOrEmpty(inputNormalized))
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

        public async Task<bool> IsAuthorAsync<T>(ClaimsPrincipal httpContextUser, int placeId) where T: BaseAuthorfullEntity
        {
            var isPlaceAuthorTask = await DalServices.IsAuthorAsync<T>(httpContextUser, placeId);
            return isPlaceAuthorTask;
        }
    }
}
