using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public abstract class PlacePhoto : IPhoto
    {
        public int? PlaceId { get; set; }
        public virtual Place Place { get; set; }
        public Guid BlobKey { get; set; }
        public string Uri { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }
}
