using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.PresentationLayer.WebServices.Components.PlaceReviews
{
    public class PlaceReviews : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();//return View(new ThisPlaceReviewsViewModel {Place = item});
        }
    }
}
