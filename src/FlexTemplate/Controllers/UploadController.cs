﻿using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlexTemplate.Controllers
{
    public class UploadController : BaseController
    {
        public UploadController(Context Context) : base(Context)
        {
            context = Context;
        }

        [HttpPost]
        [Route("/api/upload/head/{id}")]
        public void UploadHeadPhoto(string id)
        {
            var file = HttpContext.Request.Form.Files[0];
            var path = $@"wwwroot\Resources\Places\{id}\";
            var filename = "head.jpg";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (file.Length > 0)
            {
                using (var stream = new FileStream(path + filename, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
        }

        [HttpPost]
        [Route("/api/upload/producthead/{fileDescriptor}")]
        public void UploadProductHeadPhoto(string fileDescriptor)
        {
            var file = HttpContext.Request.Form.Files[0];
            var filename = Guid.NewGuid() + ".jpg";
            var path = $@"wwwroot\Resources\Products\{fileDescriptor}\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (file.Length > 0)
            {
                using (var stream = new FileStream(path + filename, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
        }

        [HttpPost]
        [Route("/api/upload/newplace/{fileDescriptor}")]
        public void UploadNewPlacePhoto(string fileDescriptor)
        {
            var file = HttpContext.Request.Form.Files[0];
            var filename = Guid.NewGuid() + ".jpg";
            var path = $@"wwwroot\Resources\Places\{fileDescriptor}\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (file.Length > 0)
            {
                using (var stream = new FileStream(path + filename, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
        }
    }
}
