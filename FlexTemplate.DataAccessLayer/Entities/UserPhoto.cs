using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    [Table("UserPhotos")]
    public class UserPhoto : IPhoto
    {
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public Guid BlobKey { get; set; }
        public string Uri { get; set; }
        public bool IsActive { get; set; }
        public int Id { get; set; }
    }
}
