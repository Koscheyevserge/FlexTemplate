using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsFeed
{
    public class BlogsFeed : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public BlogsFeed(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var input = Request.Query["input"].ToString();
            var tags = Request.Query["tags"].ToArray().Select(int.Parse);
            var categories = Request.Query["categories"].ToArray().Select(int.Parse);
            var dto = await ComponentsServices.GetBlogsFeedAsync(HttpContext.User, tags, categories, input);
            var model = dto.To<ViewModel>();
            model.CategoriesIds = categories;
            model.Input = input;
            model.TagsIds = tags;
            return View(model);
        }
    }
}
