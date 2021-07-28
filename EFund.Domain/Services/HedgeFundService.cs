using Api.Service;
using EFund.Database.Entities;
using EFund.Domain.Models;
using EFund.Domain.Models.Repositories.Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Services
{
    public interface IHedgeFundService
    {
        Task CreateNewFundInfo(IFormFile image, HedgeFundInfo info);
        Task<HedgeFundInfo> GetHedgeFundInfoByContractAddress(string contractAddress);
    }

    class HedgeFundService : IHedgeFundService
    {
        private readonly string _connectionString;

        private readonly ImageService _imageService;

        private readonly int _chainId;

        public HedgeFundService(int chainId, IConfiguration configuration, ImageService imageService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _imageService = imageService;
        }

        public async Task CreateNewFundInfo(IFormFile image, HedgeFundInfo info)
        {
            using (var hRepo = new HedgeFundRepository(_connectionString, _chainId))
            {
                var path = await _imageService.SaveImage(image, info.ContractAddress);

                info.ImageUrl = path;

                if (await hRepo.GetHedgeFundInfoByContractAddress(info.ContractAddress) != null)
                    throw new ArgumentException($"Database is already contains information about contract with address {info.ContractAddress}");

                await hRepo.SaveHedgeFundInfo(info);
            }
        }

        public async Task<HedgeFundInfo> GetHedgeFundInfoByContractAddress(string contractAddress)
        {
            using (var hRepo = new HedgeFundRepository(_connectionString, _chainId))
                return await hRepo.GetHedgeFundInfoByContractAddress(contractAddress);
        }
    }
}
