using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Алиас роли пользователя
    /// </summary>
    public class UserRoleAlias : BaseAlias
    {
        /// <summary>
        /// Идентификатор роли пользователя
        /// </summary>
        public int UserRoleId { get; set; }
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public UserRole UserRole { get; set; }
    }
}
