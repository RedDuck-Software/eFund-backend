using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFund.Domain.Extensions;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Api.Service
{
    public class ImageService
    {
        public readonly string _imgFolderPath;

        public ImageService(IConfiguration configuration)
        {
            _imgFolderPath = configuration["ImageFolderPath"];
        }

        public async Task<string> SaveImage(IFormFile file, string address, int chainId)
        {
            var name = $"{RandomStringGenerator.Generate(5)}-{address}-{chainId}.jpeg";

            new DirectoryInfo(_imgFolderPath)
                .GetFiles()
                .FirstOrDefault(fileInfo => fileInfo.Name.Contains(name))
                ?.Delete();

            await using var stream = new FileStream(Path.Combine(_imgFolderPath, name), FileMode.Create);
            await file.CopyToAsync(stream);
            stream.Flush();

            return name;
        }

        public async Task<byte[]> GetBytesArrayFromFileName(string name)
        {
            try
            {
                return await File.ReadAllBytesAsync(Path.Combine(_imgFolderPath, name));
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
