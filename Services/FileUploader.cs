using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RayanStore.Services
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _environment;
        private const string _uploadsMainFolder = "uploads";

        public FileUploader(IWebHostEnvironment environment)
            => _environment = environment;

        public async Task<string> Upload(IFormFile file, string uploadToFolder = null)
        {
            string fileUrl = constructFileUrl(_uploadsMainFolder, uploadToFolder, file.FileName);
            string filePhysicalPath = mapToServerPath(fileUrl);
            
            //Ensure folder is created
            ensureFolderExistence(Path.GetDirectoryName(filePhysicalPath));
            
            using (var fileStream = new FileStream(filePhysicalPath, FileMode.Create, FileAccess.Write))
                await file.CopyToAsync(fileStream);

            return fileUrl;
        }

        public void Delete(string url)
        {
            var filePath = mapToServerPath(url);
            var file = new FileInfo(filePath);
            if (file.Exists)
                file.Delete();
        }

        #region Helper Methods

        private void ensureFolderExistence(string folderPath)
        {
            var uploadsDir = new DirectoryInfo(folderPath);
            if (!uploadsDir.Exists)
                uploadsDir.Create();
        }

        private string mapToServerPath(string relativeUrl)
        {
            return Path.Combine(_environment.WebRootPath,
                relativeUrl.Replace('/', '\\'));
        }

        private string constructFileUrl(params string[] name)
        {
            StringBuilder urlBuilder = new StringBuilder();

            foreach (var n in name)
                if (!string.IsNullOrEmpty(n))
                    urlBuilder.Append(n + "/");
            
            return urlBuilder
            .Remove(urlBuilder.Length - 1, 1).ToString();
        }

        #endregion
    }
}