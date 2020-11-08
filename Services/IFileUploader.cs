using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RayanStore.Services
{
    public interface IFileUploader
    {
         Task<string> Upload(IFormFile file, string uploadToFolder = null);

         void Delete(string url);
    }
}