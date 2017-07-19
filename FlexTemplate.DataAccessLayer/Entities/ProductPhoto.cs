using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    [Table("ProductPhotos")]
    public class ProductPhoto : BasePhoto
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
