using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Зашифрованный пароль пользователя
        /// </summary>
        public string EncryptedPassword { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя
        /// </summary>
        public int UserRoleId { get; set; }
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public virtual UserRole UserRole { get; set; }
    }
}
