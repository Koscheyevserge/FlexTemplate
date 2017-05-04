using FlexTemplate.Database;
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
        [Route("/api/upload/placehead/{id}")]
        public void UploadHeadPhoto(string id)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
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
        [Route("/api/upload/bloghead/{id}")]
        public void UploadBlogHeadPhoto(string id)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            var file = HttpContext.Request.Form.Files[0];
            var path = $@"wwwroot\Resources\Blogs\{id}\";
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
        [Route("/api/upload/placebanner/{id}")]
        public void UploadBannerPhoto(string id)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            var file = HttpContext.Request.Form.Files[0];
            var path = $@"wwwroot\Resources\Places\{id}\";
            var filename = "banner.jpg";
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
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            var file = HttpContext.Request.Form.Files[0];
            var path = $@"wwwroot\Resources\Products\{fileDescriptor}.tmp";
            if (!System.IO.File.Exists(@"wwwroot\Resources\Products\"))
                Directory.CreateDirectory(@"wwwroot\Resources\Products\");
            if (file.Length > 0)
            {
                using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(stream);
                }
            }
        }

        [HttpPost]
        [Route("/api/upload/newplace/{fileDescriptor}")]
        public void UploadNewPlacePhoto(string fileDescriptor)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
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

        [HttpPost]
        [Route("/api/upload/newblog/{fileDescriptor}")]
        public void UploadNewBlogPhoto(string fileDescriptor)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            var file = HttpContext.Request.Form.Files[0];
            var filename = Guid.NewGuid() + ".jpg";
            var path = $@"wwwroot\Resources\Blogs\{fileDescriptor}\";
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

        [HttpDelete]
        [Route("/api/upload/newplace/{fileDescriptor}")]
        public void DeletePlacePhoto(string fileDescriptor)
        {
            var path = $@"wwwroot\Resources\Places\{fileDescriptor}.jpg";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            path = $@"wwwroot\Resources\Places\{fileDescriptor}.tmp";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        [HttpDelete]
        [Route("/api/upload/newblog/{fileDescriptor}")]
        public void DeleteBlogPhoto(string fileDescriptor)
        {
            var path = $@"wwwroot\Resources\Blogs\{fileDescriptor}.jpg";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            path = $@"wwwroot\Resources\Blogs\{fileDescriptor}.tmp";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        [HttpDelete]
        [Route("/api/upload/producthead/{fileDescriptor}")]
        public void DeleteProductHead(string fileDescriptor)
        {
            var path = $@"wwwroot\Resources\Products\{fileDescriptor}.jpg";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
            path = $@"wwwroot\Resources\Products\{fileDescriptor}.tmp";
            if (System.IO.File.Exists(path))
                System.IO.File.Delete(path);
        }

        [HttpPost]
        [Route("/api/upload/blogbanner/{id}")]
        public void UploadBlogBannerPhoto(string id)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            var file = HttpContext.Request.Form.Files[0];
            var path = $@"wwwroot\Resources\Blogs\{id}\";
            var filename = "banner.jpg";
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
