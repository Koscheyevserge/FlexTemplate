﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class AvailableContainerForContainer : IEntity
    {
        public int PageId { get; set; }
        public Page Page { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
        public int Id { get; set; }
    }
}
