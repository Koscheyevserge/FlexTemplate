using System.Collections.Generic;

namespace FlexTemplate.BusinessLogicLayer.DataTransferObjects
{
    public class BlogCommentsComponentDto
    {
        public int CommentsCount { get; set; }
        public List<BlogCommentsComponentCommentDto> Comments { get; set; }
    }
}
