using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class BlogsFeedComponentTagDao
    {
        public string Name { get; set; }
        public IEnumerable<int> WithoutThisIds { get; set; }
    }
}
