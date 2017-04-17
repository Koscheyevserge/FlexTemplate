using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Entities;

namespace FlexTemplate.ViewModels.HomeController
{
    public class YouMayAlsoLikeViewModel
    {
        [MaxLength(4)]
        public IEnumerable<Place> Places { get; set; }
    }
}
