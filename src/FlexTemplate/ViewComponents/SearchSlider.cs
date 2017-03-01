using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents
{
    public class SearchSlider : ViewComponent
    {
        private readonly Context _context;

        public SearchSlider(Context context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(string template)
        {
            return View(template);
        }
    }
}
