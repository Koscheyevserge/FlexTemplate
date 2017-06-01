using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace FlexTemplate.PresentationLayer.Core
{
    public static class FilesProvider
    {
        public static async Task<bool> SaveFileAsync(IFormFile file, string folder, string filename)
        {
            var result = true;
            if (!folder.EndsWith("/") && !folder.EndsWith("\\") && !filename.StartsWith("/") && !filename.StartsWith("\\"))
            {
                folder += "\\";
            }
            try
            {
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(folder + filename, FileMode.Create, FileAccess.Write))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool DeleteFile(string path)
        {
            var result = true;
            try
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool DeleteFolder(string path)
        {
            var result = true;
            try
            {
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool MoveFolder(string from, string to)
        {
            var result = true;
            try
            {
                if (Directory.Exists(from))
                {
                    if (Directory.Exists(to))
                    {
                        Directory.Delete(to, true);
                    }
                    Directory.Move(from, to);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public static bool MoveFile(string from, string to)
        {
            var result = true;
            try
            {
                if (File.Exists(from))
                {
                    if (File.Exists(to))
                    {
                        Directory.Delete(to, true);
                    }
                    File.Move(from, to);
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
