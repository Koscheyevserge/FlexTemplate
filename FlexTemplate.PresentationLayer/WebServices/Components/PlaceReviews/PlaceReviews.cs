using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceReviews
{
    public class PlaceReviews : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; set; }

        public PlaceReviews(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int.TryParse(Request.Query["id"], out int placeId);
            var model = await ComponentsServices.GetPlaceReviewsAsync(placeId);
            var viewModel = model.To<ViewModel>();
            viewModel.PlaceId = placeId;
            return View(viewModel);
        }
    }
}
