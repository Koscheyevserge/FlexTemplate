using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using FlexTemplate.BlobAccessLayer.Services;
using System;

namespace FlexTemplate.PresentationLayer.Core
{
    public class FilesProvider
    {
        private ImagesServices Images { get; set; }

        public FilesProvider(ImagesServices images)
        {
            Images = images;
        }

        public async Task<Uri> SaveFileAsync(IFormFile file, string folder, string filename)
        {
            return await Images.UploadBlobAsync(file, folder, filename);
        }
    }
}
