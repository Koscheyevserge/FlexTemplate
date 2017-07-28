using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Options;

namespace FlexTemplate.BlobAccessLayer.Services
{
    public class ImagesServices
    {
        private CloudBlobClient BlobClient { get; set; }
        private StorageAccountOptions Options { get; set; }

        public ImagesServices(IOptions<StorageAccountOptions> options)
        {
            var connectionString = $"DefaultEndpointsProtocol=https;AccountName={options.Value.StorageAccountName};" +
                $"AccountKey={options.Value.StorageAccountKey};EndpointSuffix=core.windows.net;";
            var cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            BlobClient = cloudStorageAccount.CreateCloudBlobClient();
            Options = options.Value;
        }

        public async Task<Uri> GetBlob(string directoryName, string blobName)
        {
            var container = BlobClient.GetContainerReference(Options.ImagesContainerName);
            await container.CreateIfNotExistsAsync();
            var permissions = new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob };
            await container.SetPermissionsAsync(permissions);
            var directory = container.GetDirectoryReference(directoryName);
            var blob = directory.GetBlockBlobReference(blobName);
            return blob?.Uri;
        }

        public async Task<List<Uri>> GetBlobs(string directoryName)
        {
            var container = BlobClient.GetContainerReference(Options.ImagesContainerName);
            await container.CreateIfNotExistsAsync();
            var permissions = new BlobContainerPermissions {PublicAccess = BlobContainerPublicAccessType.Blob};
            await container.SetPermissionsAsync(permissions);
            var directory = container.GetDirectoryReference(directoryName);
            BlobContinuationToken continuationToken = null;
            var blobItems = new List<Uri>();
            do
            {
                var response = await directory.ListBlobsSegmentedAsync(continuationToken);
                continuationToken = response.ContinuationToken;
                blobItems.AddRange(response.Results.OfType<CloudBlockBlob>().Select(cbb => cbb.Uri));
            }
            while (continuationToken != null);
            return blobItems;
        }

        public async Task<bool> BlobExistsAsync(Uri uri)
        {
            var blob = await BlobClient.GetBlobReferenceFromServerAsync(uri);
            return await blob.ExistsAsync();
        }

        public async Task<Uri> UploadBlobAsync(IFormFile file, string directoryName, string fileName)
        {
            if (file == null)
            {
                return null;
            }
            var headers = file.ContentType.Split('/');
            if (!headers.Any())
            {
                return null;
            }
            if (headers[0] != "image")
            {
                return null;
            }
            fileName += $".{headers[1]}";
            var container = BlobClient.GetContainerReference(Options.ImagesContainerName);
            await container.CreateIfNotExistsAsync();
            var permisson = new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob };
            await container.SetPermissionsAsync(permisson);
            var directory = container.GetDirectoryReference(directoryName);
            var blockBlob = directory.GetBlockBlobReference(fileName);
            using (var stream = file.OpenReadStream())
            {
                await blockBlob.UploadFromStreamAsync(stream);                
            }
            return blockBlob.Uri;
        }
    }
}
