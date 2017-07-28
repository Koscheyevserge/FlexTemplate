using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FlexTemplate.DataAccessLayer.Entities
{
    [Table("UserPhotos")]
    public class UserPhoto : BasePhoto
    {
        public int? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
