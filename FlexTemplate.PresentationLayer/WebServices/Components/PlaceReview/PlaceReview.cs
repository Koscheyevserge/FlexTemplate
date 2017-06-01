using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceReview
{
    public class PlaceReview : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View(); //return View(new ThisPlaceReviewViewModel {Review = item});
        }
    }
}
