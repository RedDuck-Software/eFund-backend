using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EFund.Database.Entities;
using EFund.Database.Repositories.Dapper;
using EFund.Domain.Extensions;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Api.Service
{
    public class ImageService
    {
        public readonly string _connectionString;

        public ImageService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<string> SaveImage(IFormFile file, string address, int chainId, string type = "jpeg")
        {
            var bytes = new byte[file.Length];
            await file.OpenReadStream().ReadAsync(bytes);
            
            var name = $"{ RandomStringGenerator.Generate(5) }-{ address }-{ chainId }.{type}";

            var dataImage = new DataImage
            {
                Id = name,
                Image = bytes,
            };

            using (var iRepo = new ImageRepository(_connectionString))
            {
                await iRepo.AddAsync(dataImage);
            }

            return name;
        }

        public async Task<byte[]> GetBytesArrayById(string id)
        {
            using var iRepo = new ImageRepository(_connectionString);
            var dataImage = await iRepo.GetByKeyAsync(id);
            return dataImage == null ? null : Convert.FromBase64String(Convert.ToBase64String(dataImage.Image));
        }
    }
}
