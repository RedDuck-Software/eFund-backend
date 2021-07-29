using Dapper;
using EFund.Database.Entities;
using EFund.Domain.Models.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Database.Repositories.Dapper
{
    public class NetworkRepository : RepositoryBase
    {
        public NetworkRepository(string connectionString) : base(connectionString) { }

        public async Task<Network> GetNetworkByChainIdAsync(int chainId)
        {
            return await SqlConnection.QueryFirstOrDefaultAsync<Network>($"SELECT * FROM nerworks WHERE ChainId={chainId}");
        }
    }
}
