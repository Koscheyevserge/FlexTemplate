using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogComment : IComment
    {
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; }
        public bool IsModerated { get; set; }
        public User User { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public int Id { get; set; }
    }
}
