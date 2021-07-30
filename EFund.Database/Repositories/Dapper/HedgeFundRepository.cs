using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EFund.Domain.Models.Repositories.Dapper;
using EFund.Domain.Models.Repositories.Abstract;
using Dapper;
using EFund.Database.Entities;

namespace EFund.Domain.Models.Repositories.Dapper
{
    public class HedgeFundRepository : RepositoryBase, IHedgeFundInfosRepository
    {
        private readonly int _chainId;

        public HedgeFundRepository(string connectionString, int chainId) : base(connectionString)
        {
            _chainId = chainId;
        }

        public async Task<HedgeFundInfo> GetHedgeFundInfoByContractAddress(string contractAddress)
        {
            return await SqlConnection.QueryFirstOrDefaultAsync<HedgeFundInfo>(
                $"SELECT " +
                    $"convert(varchar(42),h.ContractAddress,1) as ContractAddress, " +
                    $"ChainId, " +
                    $"Name, " +
                    $"Description, " +
                    $"ImageUrl " +
                $" FROM hedgefund_infos h WHERE ContractAddress=CONVERT(binary(20),'{contractAddress}',1) and ChainId={_chainId}");
        }

        public async Task SaveHedgeFundInfo(HedgeFundInfo hedgeFundInfo)
        {
            await SqlConnection.ExecuteAsync(
                $"INSERT INTO hedgefund_infos VALUES(CONVERT(binary(20),'{hedgeFundInfo.ContractAddress}',1),{_chainId}, @Name, @Description, @ImageUrl);", hedgeFundInfo);
        }

        public async Task DeleteHedgeFundInfoByContractId(string contractAddress)
        {
            await SqlConnection.ExecuteAsync(
                $"DELETE FROM hedgefund_infos WHERE ContractAddress=CONVERT(binary(20),{contractAddress},1) and ChainId={_chainId}");
        }
    }
}