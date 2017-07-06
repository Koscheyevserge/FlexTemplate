using System;

namespace FlexTemplate.DataAccessLayer.DataAccessObjects
{
    public class BlogCommentsComponentCommentDao
    {
        public DateTime CreatedOn { get; set; }
        public string AuthorPhotoPath { get; set; }
        public string Text { get; set; }
        public string AuthorUsername { get; set; }
    }
}
