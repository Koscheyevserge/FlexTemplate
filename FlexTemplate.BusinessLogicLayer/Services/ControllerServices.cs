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

        public async Task<IEnumerable<int>> GetPlacesAsync(ClaimsPrincipal httpContextUser, int[] cities,
            int[] categories, string input, int page = 1, int orderBy = 1, bool isDescending = false)
        {
            var placesPerPageCount = await CommonServices.GetPlacesPerPageCountAsync();
            var inputNormalized = input?.Trim().ToUpper();
            var userDao = await DalServices.GetUserAsync(httpContextUser);
            if (!_memoryCache.TryGetValue(PlacesCacheKey, out IEnumerable<CachedPlaceDto> places) || places == null)
            {
                var placesDao = await DalServices.GetPlacesAsync();
                places = placesDao.To<IEnumerable<CachedPlaceDto>>();
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
            var user = userDao.To<UserDto>();
            var userLanguage = user?.UserLanguageId ?? -1;
            var defaultLanguage = user?.DefaultLanguageId ?? -1;
            foreach (var place in places)
            {
                var placesCityNames = place.City.Names
                    .Where(n => n.LanguageId == userLanguage)
                    .ToList();
                if (!placesCityNames.Any())
                {
                    placesCityNames.AddRange(place.City.Names
                        .Where(n => n.LanguageId == defaultLanguage));
                }
                if (!placesCityNames.Any())
                {
                    placesCityNames.Add(new CachedCityNameDto {Name = place.City.Name});
                }
                place.City.Names = placesCityNames;

                var placesNames = place.Names
                    .Where(n => n.LanguageId == userLanguage)
                    .ToList();
                if (!placesNames.Any())
                {
                    placesNames.AddRange(place.Names
                        .Where(n => n.LanguageId == defaultLanguage));
                }
                if (!placesNames.Any())
                {
                    placesNames.Add(new CachedPlaceNameDto { Name = place.Name });
                }
                place.Names = placesNames;

                foreach (var category in place.Categories)
                {
                    var categoryNames = category.Names
                        .Where(n => n.LanguageId == userLanguage)
                        .ToList();
                    if (!categoryNames.Any())
                    {
                        categoryNames.AddRange(category.Names
                            .Where(n => n.LanguageId == defaultLanguage));
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

        public async Task<bool> IsAuthorAsync<T>(ClaimsPrincipal httpContextUser, int placeId) where T: BaseAuthorfullEntity
        {
            var isPlaceAuthorTask = await DalServices.IsAuthorAsync<T>(httpContextUser, placeId);
            return isPlaceAuthorTask;
        }
    }
}
