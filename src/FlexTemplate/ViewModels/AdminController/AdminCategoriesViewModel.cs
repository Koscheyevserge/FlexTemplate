﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Controllers;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.AdminController
{
    public class AdminCategoriesViewModel : BaseAdminViewModel
    {
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<Category> Categories { get; set; } 
    }
}
