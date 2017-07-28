using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.NewPlaceMenu
{
    public class NewPlaceMenu : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public NewPlaceMenu(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public IViewComponentResult Invoke(int position)
        {
            var model = ComponentsServices.GetNewPlaceMenu(position);
            return View(model.To<ViewModel>());
        }
    }
}
