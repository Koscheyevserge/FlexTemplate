namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class PlaceHeaderComponentDto
    {
        public bool CanEdit { get; set; }
        public string PlaceBannerPath { get; set; }
        public string PlaceName { get; set; }
        public string PlaceLocation { get; set; }
        public int Stars { get; set; }
        public int ReviewsCount { get; set; }
        public int PlaceId { get; set; }
    }
}
