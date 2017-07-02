using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class BlogsFeedComponentTagDto
    {
        public string Name { get; set; }
        public IEnumerable<int> WithoutThisIds { get; set; }
    }
}
