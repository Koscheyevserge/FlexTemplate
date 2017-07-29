using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class Product : IPhotofull<ProductPhoto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int MenuId { get; set; }
        public Menu Menu { get; set; }
        public Guid BlobKey { get; set; }
        public virtual List<ProductPhoto> Photos { get; set; }
        public int Id { get; set; }
    }
}
