using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class BlogCommentsComponentDto
    {
        public int CommentsCount { get; set; }
        public IEnumerable<BlogCommentsComponentCommentDto> Comments { get; set; }
    }
}
