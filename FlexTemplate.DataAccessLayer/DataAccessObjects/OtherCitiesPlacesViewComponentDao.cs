using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class OtherCitiesPlacesViewComponentDao
    {
        public IEnumerable<int> OtherCitiesPlacesIds { get; set; }
        public string TitleLabelCaption { get; set; }
        public string SubtitleLabelCaption { get; set; }
    }
}
