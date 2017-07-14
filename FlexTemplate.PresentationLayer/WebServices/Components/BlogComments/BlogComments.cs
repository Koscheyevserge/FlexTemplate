using System.Collections.Generic;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogComments
{
    public class BlogComments : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; set; }

        public BlogComments(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int.TryParse(Request.Query["id"], out int blogId);
            var model = await ComponentsServices.GetBlogCommentsAsync(HttpContext.User, blogId);
            return View(model.To<ViewModel>());
        }
    }
}
