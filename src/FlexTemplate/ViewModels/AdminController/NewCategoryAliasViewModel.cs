using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.AdminController
{
    public class NewCategoryAliasViewModel
    {
        public IEnumerable<Language> Languages { get; set; }
        public CategoryAlias Alias { get; set; }
    }
}
