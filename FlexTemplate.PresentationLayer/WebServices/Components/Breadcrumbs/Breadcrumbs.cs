using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Breadcrumbs
{
    public class Breadcrumbs : FlexViewComponent
    {
        public IViewComponentResult Invoke(string templateName)
        {
            return View(templateName, new BreadcrumbViewModel());
        }
    }
}
