﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Entities
{
    /// <summary>
    /// Расширения для страницы
    /// </summary>
    public class Page : BaseEntity
    {
        /// <summary>
        /// Название страницы
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Классы, что будут подставлятся в <body classes="">
        /// </summary>
        public string BodyClasses { get; set; }
        /// <summary>
        /// Титул страницы
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Развязочная таблица компонентов на странице
        /// </summary>
        public virtual List<PageContainerTemplate> PageContainerTemplates { get; set; }
        /// <summary>
        /// Доступные контейнеры
        /// </summary>
        public virtual List<AvailableContainer> AvailableContainers { get; set; }
    }
}
