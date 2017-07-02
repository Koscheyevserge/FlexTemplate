using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class BlogsFeedComponentCategoryDto
    {
        public string Caption { get; set; }
        public int BlogsCount { get; set; }
        public IEnumerable<int> WithoutThisIds { get; set; }
    }
}
