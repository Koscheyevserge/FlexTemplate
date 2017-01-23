using System.Collections.Generic;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// Название роли пользователя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<UserRoleAlias> Aliases { get; set; } 
        /// <summary>
        /// Пользователи с этой ролью
        /// </summary>
        public virtual List<User> Users { get; set; } 
    }
}
