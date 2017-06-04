using System.Security.Claims;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.DataAccessLayer.Entities;

namespace FlexTemplate.BusinessLogicLayer.Services
{
    public class ControllerServices
    {
        private DataAccessLayer.Services.Services DalServices { get; }

        public ControllerServices(DataAccessLayer.Services.Services dalServices)
        {
            DalServices = dalServices;
        }

        public PageContainersHierarchyDto GetPageContainersHierarchy(string pageName)
        {
            var pageContainersHierarchyDto =
                DalServices.GetPageContainersHierarchy(pageName).To<PageContainersHierarchyDto>();
            return pageContainersHierarchyDto;
        }

        public Task<bool> CanEditVisuals(ClaimsPrincipal httpContextUser)
        {
            var canEditVisualsTask = DalServices.CanEditVisualsAsync(httpContextUser);
            return canEditVisualsTask;
        }

        public Task<bool> IsAuthor<T>(ClaimsPrincipal httpContextUser, int placeId) where T: BaseAuthorfullEntity
        {
            var isPlaceAuthorTask = DalServices.IsAuthorAsync<T>(httpContextUser, placeId);
            return isPlaceAuthorTask;
        }
    }
}
