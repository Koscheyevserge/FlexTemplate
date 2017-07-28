using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.NewPlaceProduct
{
    public class NewPlaceProduct : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public NewPlaceProduct(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public IViewComponentResult Invoke(int position,int menu)
        {
            var model = ComponentsServices.GetNewPlaceProduct(position, menu);
            return View(model.To<ViewModel>());
        }
    }
}
