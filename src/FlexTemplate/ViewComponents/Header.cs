using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.Entities;
using FlexTemplate.Services;
using FlexTemplate.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.ViewComponents
{
    public class Header : ViewComponent
    {
        private Context _context { get; set; }
        private UserManager<User> _userManager { get; set; }
        public Header(Context context, UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string template)
        {
            return View(template,
                new HeaderViewModel
                {
                    Languages = await _context.Languages.ToListAsync(),
                    CurrentLanguage = await _context.Languages
                        .FirstOrDefaultAsync(uc => uc.ShortName == CookieProvider.GetLanguage(HttpContext))
                            ?? _context.Languages.FirstOrDefault(l => l.IsDefault) 
                            ?? new Language(),
                    CurrentUser = await _userManager.GetUserAsync(HttpContext.User)
        });
        }
    }
}
