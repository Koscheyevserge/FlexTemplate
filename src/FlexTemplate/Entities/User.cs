using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FlexTemplate.Entities
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
    }
}
