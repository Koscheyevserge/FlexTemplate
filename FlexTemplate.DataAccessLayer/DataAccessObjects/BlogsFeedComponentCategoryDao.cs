using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class BlogsFeedComponentCategoryDao
    {
        public string Caption { get; set; }
        public int BlogsCount { get; set; }
        public IEnumerable<int> WithoutThisIds { get; set; }
    }
}
