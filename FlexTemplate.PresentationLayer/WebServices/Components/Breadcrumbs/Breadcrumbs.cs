using System.Collections.Generic;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.Breadcrumbs
{
    public class Breadcrumbs : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public Breadcrumbs(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }
        
        public IViewComponentResult Invoke(string templateName)
        {
            var model = new ViewModel
            {
                Breadcrumbs = new List<BreadcrumbViewModel>()
            };
            return View(templateName, model);
        }
    }
}
