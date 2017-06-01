namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogComment : BaseEntity
    {
        public User Author { get; set; }
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; }
    }
}
