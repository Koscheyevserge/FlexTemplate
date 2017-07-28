using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class PlacePhoto : BasePhoto
    {
        public int? PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
