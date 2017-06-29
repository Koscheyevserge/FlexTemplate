using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.BlogsPagination
{
    public class BlogsPagination : FlexViewComponent
    {
        private ComponentsServices ComponentsServices { get; }

        public BlogsPagination(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int blogsCount)
        {
            int.TryParse(Request.Query["currentPage"], out int currentPage);
            var serviceResult = await ComponentsServices.GetBlogsPaginationAsync(blogsCount, currentPage);
            var input = Request.Query["input"].ToString();
            var tags = Request.Query["tags"].ToArray().Select(int.Parse);
            var categories = Request.Query["categories"].ToArray().Select(int.Parse);
            var paginationVM = serviceResult.To<PaginationViewModel>();
            var model = new ViewModel
            {
                Categories = categories,
                Tags = tags,
                Input = input,
                CurrentPage = currentPage,
                HasNextPage = paginationVM.HasNextPage,
                HasPreviousPage = paginationVM.HasPreviousPage,
                Pages = paginationVM.TotalPagesCount
            };
            return View(model);
        }
    }
}
