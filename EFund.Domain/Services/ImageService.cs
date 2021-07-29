using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFund.Domain.Extensions;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Http;

namespace Api.Service
{
    public class ImageService
    {
        public async Task<string> SaveImage(IFormFile file, string address)
        {
            new DirectoryInfo("Images")
                .GetFiles()
                .FirstOrDefault(fileInfo => fileInfo.Name.Contains(address))
                ?.Delete();

            var name = $"{RandomStringGenerator.Generate(5)}-{address}";
            var path = Path.Combine("Images", $"{name}.jpeg");
            
            await using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Flush();

            return $"images/{name}.jpeg";
        }

        public async Task<byte[]> GetBytesArrayFromFileName(string name) =>
            await File.ReadAllBytesAsync(Path.Combine("Images", name));
    }
}
