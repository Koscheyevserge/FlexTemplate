using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlacePhoto
{
    public class PlacePhoto : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; set; }

        public PlacePhoto(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
