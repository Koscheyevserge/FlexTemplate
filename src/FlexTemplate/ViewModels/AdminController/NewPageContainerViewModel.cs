using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.AdminController
{
    public class NewPageContainerViewModel
    {
        public PageContainerTemplate PageContainerTemplate { get; set; }
        public IEnumerable<Container> Containers { get; set; }
    }
}
