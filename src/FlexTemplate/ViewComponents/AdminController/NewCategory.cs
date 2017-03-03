using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FlexTemplate.ViewComponents.AdminController
{
    public class NewCategory : ViewComponent
    {
        private readonly Context _context;
        public NewCategory(Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var newCategory = new Category();
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
            return View(newCategory);
        }
    }
}
