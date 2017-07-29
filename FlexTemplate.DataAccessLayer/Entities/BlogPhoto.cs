using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    [Table("BlogPhotos")]
    public class BlogPhoto : IPhoto
    {
        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }
        public Guid BlobKey { get; set; }
        public string Uri { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }
}
