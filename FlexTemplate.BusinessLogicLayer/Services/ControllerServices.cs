using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.DataAccessLayer.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public class ControllerServices
    {
        private readonly IMemoryCache _memoryCache;
        private DataAccessLayer.Services.Services DalServices { get; }
        private const string PlacesCacheKey = "PLACES_CACHE";

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

        public async Task<IEnumerable<int>> GetPlacesAsync(int[] cities, int[] categories, string input,
            int currentPage = 1, int orderBy = 1, bool isDescending = false)
        {
            var placesPerPage = 12;//TODO сохранить как системную настройку
            var inputNormalized = input.Trim().ToUpper();
            if (!_memoryCache.TryGetValue(PlacesCacheKey, out IEnumerable<CachedPlaceDto> places))
            {
                places = (await DalServices.GetPlacesAsync()).To<IEnumerable<CachedPlaceDto>>();
                _memoryCache.Set(PlacesCacheKey, places);
            }
            if (cities.Any())
            {
                places = places.Where(p => cities.Contains(p.CityId)).ToList();
            }
            if (categories.Any())
            {
                places = places.Where(p => p.CategoriesIds
                    .Intersect(categories).Any());
            }
            if (string.IsNullOrEmpty(inputNormalized))
            {
                places = places.Where(p => p.Name.ToUpper().Contains(inputNormalized) ||
                                           p.Aliases.Any(a => a.ToUpper().Contains(inputNormalized)));
            }
            //TODO добавить сортировку по orderBy и isDescending
            places = places.Take(placesPerPage);
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
                places = places.Where(p => cities.Contains(p.CityId)).ToList();
            }
            if (categories.Any())
            {
                places = places.Where(p => p.CategoriesIds
                    .Intersect(categories).Any());
            }
            if (string.IsNullOrEmpty(inputNormalized))
            {
                places = places.Where(p => p.Name.ToUpper().Contains(inputNormalized) ||
                                           p.Aliases.Any(a => a.ToUpper().Contains(inputNormalized)));
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
