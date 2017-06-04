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
                DalServices.GetPageContainersHierarchy(pageContainerTemplateId).To<PageContainersHierarchyDto>();
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
    }
}
