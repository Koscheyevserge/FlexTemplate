﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Database;
using FlexTemplate.ViewModels;
using FlexTemplate.ViewModels.HomeController;
using Microsoft.AspNetCore.Mvc;

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
            var result = Directory.GetFiles(path).Except(new [] { $@"wwwroot\Resources\Places\{id}\head.jpg" });
            return result;
        }

        [Route("api/placelocation/{id}")]
        public GeolocationViewModel PlaceLocation(int id)
        {
            var result = context.Places.Where(p => p.Id == id)
                .Select(p => new GeolocationViewModel { Latitude = p.Latitude, Longitude = p.Longitude })
                .SingleOrDefault();
            return result;
        }
    }
}
