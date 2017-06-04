using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Capabilities
{
    public class Capabilities : FlexViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
