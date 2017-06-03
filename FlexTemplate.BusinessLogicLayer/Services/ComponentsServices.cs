using FlexTemplate.BusinessLogicLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;

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

        public SearchViewComponentDto GetSearchViewComponentDto()
        {
            var result = DalServices
        }
    }
}
