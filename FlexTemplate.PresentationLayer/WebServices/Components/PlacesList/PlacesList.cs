using System.Collections.Generic;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Enums;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacesList
{
    public class PlacesList : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public PlacesList(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync([FromQuery] int listType, IEnumerable<int> placesIds)
        {
            var places = await ComponentsServices.GetPlacesListAsync(HttpContext.User, placesIds);
            var model = new ViewModel
            {
                ListType = listType,
                Places = places.To<IEnumerable<PlaceViewModel>>()
            };
            string template;
            switch ((ListTypeEnum)listType)
            {
                case ListTypeEnum.Grid:
                    template = "Grid";
                    break;
                case ListTypeEnum.List:
                    template = "List";
                    break;
                default:
                    template = "List";
                    break;
            }
            return View(template, model);
        }
    }
}
