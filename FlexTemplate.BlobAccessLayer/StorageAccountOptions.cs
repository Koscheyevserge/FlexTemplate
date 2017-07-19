using System;
using System.Collections.Generic;
using System.Text;

namespace FlexTemplate.BlobAccessLayer
{
    public class StorageAccountOptions
    {
        public string StorageAccountName { get; set; }
        public string StorageAccountKey { get; set; }
        public string ImagesContainerName { get; set; }
        public string ThumbnailContainerName { get; set; }
    }
}
