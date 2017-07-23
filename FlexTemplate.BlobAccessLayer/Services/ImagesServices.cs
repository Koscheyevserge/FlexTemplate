using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public async Task<List<Uri>> UploadBlob(string blobContainer, IFormFile file, string directoryName)
        {
            var container = BlobClient.GetContainerReference(blobContainer);
            await container.CreateIfNotExistsAsync();
            var permissions = new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob};
            await container.SetPermissionsAsync(permissions);
            BlobContinuationToken continuationToken= null;
            var blobItems = new List<Uri>();
            do
            {
                var response = await container.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = response.ContinuationToken;
                blobItems.AddRange(response.Results.OfType<CloudBlockBlob>().Select(cbb => cbb.Uri));
            }
            while (continuationToken != null);
            return blobItems;
        }
    }
}
