using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    [Table("BlogPhotos")]
    public class BlogPhoto : BasePhoto
    {
        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
