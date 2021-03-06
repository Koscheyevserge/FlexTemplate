﻿using System;
using System.Collections.Generic;

namespace FlexTemplate.DataAccessLayer.Entities
{
    /// <summary>
    /// Категория
    /// </summary>
    public class PlaceCategory : IAliasfull<PlaceCategoryAlias>
    {
        /// <summary>
        /// Название категории
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Категория заведения
        /// </summary>
        public virtual List<PlacePlaceCategory> PlacePlaceCategories { get; set; } 
        /// <summary>
        /// Алиасы
        /// </summary>
        public virtual List<PlaceCategoryAlias> Aliases { get; set; }
        public int Id { get; set; }
    }
}
