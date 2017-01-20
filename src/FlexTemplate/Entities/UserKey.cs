using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Ключ пользователя
    /// </summary>
    public class UserKey : BaseEntity
    {
        /// <summary>
        /// Ключ, идентифицирующий сессию пользователя
        /// </summary>
        public Guid Key { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }
    }
}
