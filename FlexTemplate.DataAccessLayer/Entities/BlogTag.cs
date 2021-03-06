﻿using System;

namespace FlexTemplate.DataAccessLayer.Entities
{
    public class BlogTag : IEntity
    {
        /// <summary>
        /// Идентификатор блога
        /// </summary>
        public int BlogId { get; set; }
        /// <summary>
        /// Блог
        /// </summary>
        public virtual Blog Blog { get; set; }

        /// <summary>
        /// Идентификатор тэга
        /// </summary>
        public int TagId { get; set; }
        /// <summary>
        /// Тэг
        /// </summary>
        public virtual Tag Tag { get; set; }
        public int Id { get; set; }
    }
}
