using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace FlexTemplate.BlobAccessLayer.Services
{
    public class ImagesServices
    {
        private CloudBlobClient BlobClient { get; set; }
        public ImagesServices(StorageAccountOptions options)
        {
            var cloudStorageAccount = CloudStorageAccount.Parse(
                $"DefaultEndpointsProtocol=https;" +
                $"AccountName={options.StorageAccountName};" +
                $"AccountKey={options.StorageAccountKey};" +
                $"EndpointSuffix=core.windows.net;");
            BlobClient = cloudStorageAccount.CreateCloudBlobClient();
        }
    }
}
