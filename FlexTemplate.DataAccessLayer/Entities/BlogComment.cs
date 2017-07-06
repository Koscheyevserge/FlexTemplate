namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogComment : BaseAuthorfullEntity
    {
        public Blog Blog { get; set; }
        public int BlogId { get; set; }
        public string Text { get; set; }
        public bool IsModerated { get; set; }
    }
}
