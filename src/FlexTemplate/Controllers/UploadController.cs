using FlexTemplate.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FlexTemplate.Services;

namespace FlexTemplate.Controllers
{
    public class UploadController : BaseController
    {
        public UploadController(Context Context) : base(Context)
        {
            context = Context;
        }

        #region Place

        #region PlaceHead
        [HttpPost]
        [Route("/api/upload/placehead/{placeId}")]
        public void UploadPlaceHeadPhoto(string placeId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            FilesProvider.SaveFile(HttpContext.Request.Form.Files[0], $@"wwwroot\Resources\Places\{placeId}\", "head.jpg");
        }
        [HttpDelete]
        [Route("/api/upload/placehead/{placeId}")]
        public void DeletePlaceHeadPhoto(string placeId)
        {
            var path = $@"wwwroot\Resources\Places\{placeId}\head.jpg";
            FilesProvider.DeleteFile(path);
        }
        #endregion

        #region PlaceBanner
        [HttpPost]
        [Route("/api/upload/placebanner/{placeId}")]
        public void UploadBannerPhoto(string placeId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            FilesProvider.SaveFile(HttpContext.Request.Form.Files[0], $@"wwwroot\Resources\Places\{placeId}\", "banner.jpg");
        }
        [HttpDelete]
        [Route("/api/upload/placebanner/{placeId}")]
        public void DeleteBannerPhoto(string placeId)
        {
            var path = $@"wwwroot\Resources\Places\{placeId}\banner.jpg";
            FilesProvider.DeleteFile(path);
        }
        #endregion

        #region PlaceGallery
        [HttpPost]
        [Route("/api/upload/newplace/{fileDescriptor}")]
        public string UploadNewPlacePhoto(string fileDescriptor)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return null;
            }
            var filename = Guid.NewGuid().ToString();
            var filenameExtention = filename + ".jpg";
            FilesProvider.SaveFile(HttpContext.Request.Form.Files[0], $@"wwwroot\Resources\Places\{fileDescriptor}\", filenameExtention);
            return filename;
        }
        [HttpDelete]
        [Route("/api/upload/newplace/{placeId}/{fileDescriptor}")]
        public void DeletePlacePhoto(string placeId, string fileDescriptor)
        {
            var path = $@"wwwroot\Resources\Places\{placeId}\{fileDescriptor}.jpg";
            FilesProvider.DeleteFile(path);
            path = $@"wwwroot\Resources\Places\{placeId}\{fileDescriptor}.tmp";
            FilesProvider.DeleteFile(path);
        }
        #endregion

        #endregion

        #region Blog

        #region BlogHead
        [HttpPost]
        [Route("/api/upload/bloghead/{blogId}")]
        public void UploadBlogHeadPhoto(string blogId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            FilesProvider.SaveFile(HttpContext.Request.Form.Files[0], $@"wwwroot\Resources\Blogs\{blogId}\", "head.jpg");
        }
        [HttpDelete]
        [Route("/api/upload/bloghead/{blogId}")]
        public void DeleteBlogHeadPhoto(string blogId)
        {
            var path = $@"wwwroot\Resources\Blogs\{blogId}\head.jpg";
            FilesProvider.DeleteFile(path);
        }
        #endregion

        #region BlogBanner
        [HttpPost]
        [Route("/api/upload/blogbanner/{blogId}")]
        public void UploadBlogBannerPhoto(string blogId)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            FilesProvider.SaveFile(HttpContext.Request.Form.Files[0], $@"wwwroot\Resources\Blogs\{blogId}\", "banner.jpg");
        }
        [HttpDelete]
        [Route("/api/upload/bloghead/{blogId}")]
        public void DeleteBlogBannerPhoto(string blogId)
        {
            var path = $@"wwwroot\Resources\Blogs\{blogId}\banner.jpg";
            FilesProvider.DeleteFile(path);
        }
        #endregion

        #endregion

        #region Product

        #region ProductHead
        [HttpPost]
        [Route("/api/upload/producthead/{fileDescriptor}")]
        public void UploadProductHeadPhoto(string fileDescriptor)
        {
            if (!HttpContext.Request.Form.Files.Any())
            {
                return;
            }
            FilesProvider.SaveFile(HttpContext.Request.Form.Files[0], @"wwwroot\Resources\Products\", $"{fileDescriptor}.tmp");
        }
        [HttpDelete]
        [Route("/api/upload/producthead/{fileDescriptor}")]
        public void DeleteProductHead(string fileDescriptor)
        {
            var path = $@"wwwroot\Resources\Products\{fileDescriptor}.jpg";
            FilesProvider.DeleteFile(path);
            path = $@"wwwroot\Resources\Products\{fileDescriptor}.tmp";
            FilesProvider.DeleteFile(path);
        }
        #endregion

        #endregion
    }
}
