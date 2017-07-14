using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class BlogCommentsComponentDao
    {
        public int CommentsCount { get; set; }
        public IEnumerable<BlogCommentsComponentCommentDao> Comments { get; set; }
    }
}
