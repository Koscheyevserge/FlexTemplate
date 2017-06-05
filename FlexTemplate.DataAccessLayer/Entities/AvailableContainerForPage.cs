﻿namespace FlexTemplate.DataAccessLayer.Entities
{
    public class AvailableContainerForPage : BaseEntity
    {
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
    }
}