using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.ViewModels;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlexTemplate.Controllers
{
    public class ResourcesController : BaseController
    {
        public ResourcesController(Context Context) : base(Context)
        {
            context = Context;
        }

        [Route("api/resources/photo-detail/{id}")]
        public IEnumerable<string> PhotoDetail(int id)
        {
            var path = $@"wwwroot\Resources\Places\{id}\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var result = Directory.GetFiles(path).Except(new [] { $@"wwwroot\Resources\Places\{id}\head.jpg" , $@"wwwroot\Resources\Places\{id}\banner.jpg" });
            return result;
        }

        [Route("api/resources/cities")]
        public IEnumerable<string> GetCities()
        {
            return context.Cities.Select(c => c.Name).Union(context.CityAliases.Select(ca => ca.Text));
        }

        [Route("api/resources/tags")]
        public IEnumerable<string> GetTags()
        {
            return context.Tags.Select(c => c.Name).Union(context.TagAliases.Select(ca => ca.Text));
        }
    }
}
