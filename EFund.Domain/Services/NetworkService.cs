using Api.Service;
using EFund.Database.Entities;
using EFund.Database.Repositories.Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Services
{
    public interface INetworkService
    {
        Task<Network> GetNetworkByChainId(int chainId);
    }

    public class NetworkService : INetworkService
    {
        private readonly ImageService _imageService;

        private readonly string _connectionString;


        public NetworkService(IConfiguration configuration, ImageService imageService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<Network> GetNetworkByChainId(int chainId)
        {
            using var nRepo = new NetworkRepository(_connectionString);
            return await nRepo.GetNetworkByChainIdAsync(chainId);
        }
    }
}
