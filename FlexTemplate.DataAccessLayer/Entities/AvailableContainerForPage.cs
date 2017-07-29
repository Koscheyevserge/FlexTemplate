using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class AvailableContainerForPage : IEntity
    {
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
        public int Id { get; set; }
    }
}
