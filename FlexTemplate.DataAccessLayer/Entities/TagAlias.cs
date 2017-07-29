using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class TagAlias : IAlias
    {
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public string Text { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int Id { get; set; }
    }
}
