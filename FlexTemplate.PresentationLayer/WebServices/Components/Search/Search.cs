using System.Collections.Generic;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Search
{
    public class Search : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public Search(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(string templateName)
        {
            var dto = await ComponentsServices.GetSearchViewComponentDtoAsync(HttpContext.User, GetType().Name);
            var model = dto.To<ViewModel>();
            return View(templateName, model);
        }
    }
}