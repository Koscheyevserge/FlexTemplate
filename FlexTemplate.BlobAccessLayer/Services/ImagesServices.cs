using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FlexTemplate.BlobAccessLayer.Services
{
    public class ImagesServices
    {
        const string blobContainerName = "images";
        private CloudBlobContainer blobContainer;

        public ImagesServices()
        {

        }
    }
}
