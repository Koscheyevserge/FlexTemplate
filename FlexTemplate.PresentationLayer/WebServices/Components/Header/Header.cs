using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Header
{
    public class Header : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public Header(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var dto = await ComponentsServices.GetHeaderViewComponentDtoAsync(HttpContext.User, GetType().Name);
            return View(dto.TemplateName, dto.To<ViewModel>());
        }
    }
}
