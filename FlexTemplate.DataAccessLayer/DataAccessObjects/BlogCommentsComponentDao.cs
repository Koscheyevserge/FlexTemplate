using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class BlogCommentsComponentDao
    {
        public int CommentsCount { get; set; }
        public List<BlogCommentsComponentCommentDao> Comments { get; set; }
    }
}
