using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class YouMayAlsoLikeComponentDao
    {
        public IEnumerable<YouMayAlsoLikeComponentPlaceDao> Places { get; set; }
    }
}