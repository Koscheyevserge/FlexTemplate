using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BaseAuthorfullViewableEntity : BaseAuthorfullEntity
    {
        public int ViewsCount { get; set; }
    }
}
