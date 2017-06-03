using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }
        public virtual List<Blog> Blogs { get; set; }
        public virtual List<Place> Places { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
    }
}
