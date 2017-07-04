using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Сущность с автором
    /// </summary>
    public class BaseAuthorfullEntity : BaseEntity
    {
        public User User { get; set; }
    }
}
