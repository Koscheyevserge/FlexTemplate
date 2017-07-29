using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    [Table("ProductPhotos")]
    public class ProductPhoto : IPhoto
    {
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid BlobKey { get; set; }
        public string Uri { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }
}
