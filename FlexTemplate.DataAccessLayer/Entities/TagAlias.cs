namespace FlexTemplate.DataAccessLayer.Entities
{
    public class TagAlias : BaseAlias
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
