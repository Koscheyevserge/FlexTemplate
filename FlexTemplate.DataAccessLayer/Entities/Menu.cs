using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class Menu : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<Product> Products { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
