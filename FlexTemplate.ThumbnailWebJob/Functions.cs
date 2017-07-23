using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using ImageResizer;

namespace FlexTemplate.ThumbnailWebJob
{
    public class Functions
    {
        public static void CreateThumbnail([BlobTrigger("images/{name}.{ext}")] Stream input,
            [Blob("thumbnails/{name}.{ext}", FileAccess.Write)] Stream output)
        {
            ResizeImage(input, output, 800);
        }

        private static void ResizeImage(Stream input, Stream output, int width)
        {
            var instructions = new Instructions
            {
                Width = width,
                Mode = FitMode.Carve,
                Scale = ScaleMode.Both
            };
            ImageBuilder.Current.Build(new ImageJob(input, output, instructions));
        }
    }
}