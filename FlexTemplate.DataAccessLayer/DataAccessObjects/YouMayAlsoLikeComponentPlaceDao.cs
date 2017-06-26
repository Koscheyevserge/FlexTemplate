using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class YouMayAlsoLikeComponentPlaceDao
    {
        public IEnumerable<YouMayAlsoLikeComponentCategoryDao> Categories { get; set; }
        public int ReviewsCount { get; set; }
        public string Address { get; set; }
        public int Stars { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public int Id { get; set; }
    }
}
