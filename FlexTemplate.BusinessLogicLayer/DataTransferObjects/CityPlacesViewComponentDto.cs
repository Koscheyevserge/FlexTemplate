using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class CityPlacesViewComponentDto
    {
        public IEnumerable<int> ThisCityPlaceIds { get; set; }
        public string MorePlacesButtonCaption { get; set; }
        public string TitleLabelCaption { get; set; }
        public string SubtitleLabelCaption { get; set; }
    }
}
