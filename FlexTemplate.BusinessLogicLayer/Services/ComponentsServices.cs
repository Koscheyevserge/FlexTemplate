using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using System.Security.Claims;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public class ComponentsServices
    {
        private DataAccessLayer.Services.Services DalServices { get; }

        public ComponentsServices(DataAccessLayer.Services.Services dalServices)
        {
            DalServices = dalServices;
        }

        public PageContainersHierarchyDto GetPageContainersHierarchy(int pageContainerTemplateId)
        {
            var result =
                DalServices.GetPageContainersHierarchyAsync(pageContainerTemplateId).To<PageContainersHierarchyDto>();
            return result;
        }

        public async Task<SearchViewComponentDto> GetSearchViewComponentDtoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var result = await DalServices.GetSearchViewComponentDaoAsync(httpContextUser, componentName);
            return result.To<SearchViewComponentDto>();
        }

        public async Task<OtherCitiesPlacesViewComponentDto> GetOtherCitiesPlacesViewComponentDtoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var result = await DalServices.GetOtherCitiesPlacesViewComponentDaoAsync(httpContextUser, componentName);
            return result.To<OtherCitiesPlacesViewComponentDto>();
        }

        public async Task<OtherCityPlacesViewComponentDto> GetOtherCityPlacesViewComponentDtoAsync(ClaimsPrincipal httpContextUser, string componentName, int cityId)
        {
            var result = await DalServices.GetOtherCityPlacesViewComponentDaoAsync(httpContextUser, componentName, cityId);
            return result.To<OtherCityPlacesViewComponentDto>();
        }

        public async Task<CityPlacesViewComponentDto> GetCityPlacesViewComponentDtoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var result = await DalServices.GetCityPlacesViewComponentDaoAsync(httpContextUser, componentName);
            return result.To<CityPlacesViewComponentDto>();
        }

        public async Task<CityPlaceViewComponentDto> GetCityPlaceViewComponentDtoAsync(ClaimsPrincipal httpContextUser, string componentName, int placeId)
        {
            var result = await DalServices.GetCityPlaceViewComponentDaoAsync(httpContextUser, componentName, placeId);
            return result.To<CityPlaceViewComponentDto>();
        }

        public async Task<HeaderViewComponentDto> GetHeaderViewComponentDtoAsync(ClaimsPrincipal httpContextUser, string componentName)
        {
            var result = await DalServices.GetHeaderViewComponentDaoAsync(httpContextUser, componentName);
            return result.To<HeaderViewComponentDto>();
        }

        public async Task<IEnumerable<CityChecklistItemDto>> GetCityChecklistItems(ClaimsPrincipal httpContextUser, IEnumerable<int> checkedCities)
        {
            var result = await DalServices.GetCityChecklistItemsAsync(httpContextUser, checkedCities);
            return result.To<IEnumerable<CityChecklistItemDto>>();
        }

        public async Task<IEnumerable<PlaceCategoryChecklistItemDto>> GetPlaceCategoriesChecklistItems(ClaimsPrincipal httpContextUser, IEnumerable<int> checkedCategories)
        {
            var result = await DalServices.GetPlaceCategoriesChecklistItemsAsync(httpContextUser, checkedCategories);
            return result.To<IEnumerable<PlaceCategoryChecklistItemDto>>();
        }

        public async Task<IEnumerable<PlaceListItemDto>> GetPlacesListAsync(ClaimsPrincipal httpContextUser, IEnumerable<int> placesIds)
        {
            var result = await DalServices.GetPlacesListAsync(httpContextUser, placesIds);
            return result.To<IEnumerable<PlaceListItemDto>>();
        }

        public async Task<PaginationDto> GetPlacesPaginationAsync(int placesCount, int currentPage)
        {
            var placesPerPage = await CommonServices.GetPlacesPerPageCountAsync();
            var totalPages = (int) Math.Ceiling(placesCount / (double)placesPerPage);
            var result = new PaginationDto
            {
                HasNextPage = currentPage < totalPages,
                HasPreviousPage = currentPage > 1,
                TotalPagesCount = totalPages
            };
            return result;
        }

        public async Task<PlaceHeaderComponentDto> GetPlaceHeaderAsync(ClaimsPrincipal httpContextUser, int placeId)
        {
            var result = await DalServices.GetPlaceHeaderAsync(httpContextUser, placeId);
            return result.To<PlaceHeaderComponentDto>();
        }

        public async Task<PlaceLocationComponentDto> GetPlaceLocationAsync(int placeId)
        {
            var result = await DalServices.GetPlaceLocationAsync(placeId);
            return result.To<PlaceLocationComponentDto>();
        }

        public async Task<PlaceReviewComponentDto> GetPlaceReviewAsync(int reviewId)
        {
            var result = await DalServices.GetPlaceReviewAsync(reviewId);
            return result.To<PlaceReviewComponentDto>();
        }

        public async Task<PlaceMenuComponentDto> GetPlaceMenusAsync(int placeId)
        {
            var result = await DalServices.GetPlaceMenusAsync(placeId);
            return result.To<PlaceMenuComponentDto>();
        }

        public async Task<PlaceOverviewComponentDto> GetPlaceOverviewAsync(ClaimsPrincipal httpContextUser, int placeId)
        {
            var result = await DalServices.GetPlaceOverviewAsync(httpContextUser, placeId);
            return result.To<PlaceOverviewComponentDto>();
        }

        public async Task<PlaceReviewsComponentDto> GetPlaceReviewsAsync(int placeId)
        {
            var result = await DalServices.GetPlaceReviewsAsync(placeId);
            return result.To<PlaceReviewsComponentDto>();
        }

        public async Task<YouMayAlsoLikeComponentDto> GetYouMayAlsoLikeAsync(ClaimsPrincipal httpContextUser, int placeId)
        {
            var result = await DalServices.GetYouMayAlsoLikeAsync(httpContextUser, placeId);
            return result.To<YouMayAlsoLikeComponentDto>();
        }

        public async Task<PaginationDto> GetBlogsPaginationAsync(int blogsCount, int currentPage)
        {
            var blogsPerPage = await CommonServices.GetBlogsPerPageCountAsync();
            var totalPages = (int)Math.Ceiling(blogsCount / (double)blogsPerPage);
            var result = new PaginationDto
            {
                HasNextPage = currentPage < totalPages,
                HasPreviousPage = currentPage > 1,
                TotalPagesCount = totalPages
            };
            return result;
        }

        public async Task<BlogsFeedComponentDto> GetBlogsFeedAsync(ClaimsPrincipal user, IEnumerable<int> tags, IEnumerable<int> categories, string input)
        {
            var blogsDto = await DalServices.GetBlogsFeedPopularBlogsAsync();
            var tagsDto = await DalServices.GetBlogsFeedTags(user, tags, input);
            var categoriesDto = await DalServices.GetBlogsFeedCategories(user, categories, input);
            var latestBlogsDto = await DalServices.GetBlogsFeedLatestBlogs();
            var result = new BlogsFeedComponentDto
            {
                Blogs = blogsDto.To<IEnumerable<BlogsFeedComponentPopularBlogDto>>(),
                Categories = categoriesDto.To<IEnumerable<BlogsFeedComponentCategoryDto>>(),
                Tags = tagsDto.To<IEnumerable<BlogsFeedComponentTagDto>>(),
                LatestBlogs = latestBlogsDto.To<IEnumerable<BlogsFeedComponentLatestBlogDto>>()
            };
            return result;
        }
    }
}
