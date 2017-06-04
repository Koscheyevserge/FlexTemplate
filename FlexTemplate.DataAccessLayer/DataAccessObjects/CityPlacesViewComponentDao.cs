using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class CityPlacesViewComponentDao
    {
        public IEnumerable<int> ThisCityPlaceIds { get; set; }
        public string MorePlacesButtonCaption { get; set; }
        public string TitleLabelCaption { get; set; }
        public string SubtitleLabelCaption { get; set; }
    }
}
