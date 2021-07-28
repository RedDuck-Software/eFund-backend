using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using EFund.Domain.Models.Repositories.Dapper;
using EFund.Domain.Models.Repositories.Abstract;
using Dapper;

namespace EFund.Domain.Models.Repositories.Dapper
{
    public class HedgeFundRepository : RepositoryBase, IHedgeFundInfosRepository
    {
        public HedgeFundRepository(string connectionString) : base(connectionString) { }

        public async Task<IEnumerable<HedgeFundInfo>> GetHedgeFundInfos()
        {
            return await SqlConnection.QueryAsync<HedgeFundInfo>("SELECT * FROM hedgefund_infos");
        }

        public async Task<HedgeFundInfo> GetHedgeFundInfoByContractAddress(string contractAddress)
        {
            return await SqlConnection.QueryFirstOrDefaultAsync<HedgeFundInfo>(
                $"SELECT * FROM hedgefund_infos WHERE ContractId=CONVERT(binary(20),{contractAddress},1)");
        }

        public async Task SaveHedgeFundInfo(HedgeFundInfo hedgeFundInfo)
        {
            await SqlConnection.ExecuteAsync(
                $"INSERT INTO hedgefund_infos VALUES(CONVERT(binary(20),{hedgeFundInfo.ContractAddress},1), {hedgeFundInfo.Name}, {hedgeFundInfo.Description}, {hedgeFundInfo.ImageUrl});");
        }

        public async Task DeleteHedgeFundInfoByContractId(string contractAddress)
        {
            await SqlConnection.ExecuteAsync(
                $"DELETE FROM hedgefund_infos WHERE ContractId=CONVERT(binary(20),{contractAddress},1)");
        }
    }
}