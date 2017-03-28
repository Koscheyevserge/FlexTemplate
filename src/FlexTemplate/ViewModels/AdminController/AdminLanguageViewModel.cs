using FlexTemplate.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.ViewModels.AdminController
{
    public class AdminLanguageViewModel : BaseAdminViewModel
    {
        public IEnumerable<Language> Languages { get; set; }
    }
}
