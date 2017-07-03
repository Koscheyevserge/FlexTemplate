using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.BusinessLogicLayer.Services;
using FlexTemplate.PresentationLayer.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using FlexTemplate.DataAccessLayer.Entities;
using FlexTemplate.DataAccessLayer;
using FlexTemplate.PresentationLayer.WebServices.Admin.Tags;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.PresentationLayer.WebServices.Admin
{
    public class AdminController : FlexController
    {
        private readonly FlexContext _context;
        public AdminController(ControllerServices services, FlexContext context) : base(services)
        {
            _context = context;
        }
        public IActionResult Tags()
        {
            return View();
        }
        [HttpGet]
        public async Task<List<TagViewModel>> Get()
        {
            return await _context.Tags.Select(t => 
            new TagViewModel
            {
                Id = t.Id, 
                Name = t.Name
            }).ToListAsync();
        }

        [HttpPost]
        [Route("api/tags/create")]
        public IActionResult Create([FromBody]TagPostModel model)
        {
            var tag = new Tag { Name = model.Name };
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return Ok(tag.Id);
        }
        /*
        [HttpDelete("{Name}")]
        public IActionResult Delete(string Name)
        {
            Tag tag = data.Where(x => x.Name == Name);
            if (tag == null)
            {
                return NotFound();
            }
            data.Remove(tag);
            return Ok(tag);
        }*/
    }
}