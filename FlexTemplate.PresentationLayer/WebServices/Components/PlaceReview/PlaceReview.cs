using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Extentions;
using FlexTemplate.BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceReview
{
    public class PlaceReview : ViewComponent
    {
        private ComponentsServices ComponentsServices { get; set; }

        public PlaceReview(ComponentsServices componentsServices)
        {
            ComponentsServices = componentsServices;
        }

        public async Task<IViewComponentResult> InvokeAsync(int reviewId)
        {
            var model = await ComponentsServices.GetPlaceReviewAsync(reviewId);
            return View(model.To<ViewModel>());
        }
    }
}
