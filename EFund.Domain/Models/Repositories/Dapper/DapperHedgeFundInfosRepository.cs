using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using EFund.Domain.Models.Repositories.Abstract;

namespace EFund.Domain.Models.Repositories.Dapper
{
    public class DapperHedgeFundInfosRepository : IHedgeFundInfosRepository
    {
        private readonly string _connectionString;

        public DapperHedgeFundInfosRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<HedgeFundInfo> GetHedgeFundInfos()
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<HedgeFundInfo>("SELECT * FROM hedgefund_infos").AsQueryable();
        }

        public HedgeFundInfo? GetHedgeFundInfoByContractId(string contractId)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            return db.Query<HedgeFundInfo>($"SELECT * FROM hedgefund_infos WHERE ContractId = {contractId}")
                .FirstOrDefault();
        }

        
        
        public void SaveHedgeFundInfo(HedgeFundInfo hedgeFundInfo)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var affectedRows = db.Execute(
                "UPDATE hedgefund_infos SET Name = @Name, Description = @Description WHERE ContractId = @ContractId", hedgeFundInfo);
            if (affectedRows == 0)
                db.Execute("INSERT INTO hedgefund_infos VALUES(@ContractId, @Name, @Description)", hedgeFundInfo);
        }

        public void DeleteHedgeFundInfoByContractId(string contractId)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            db.Execute($"DELETE FROM hedgefund_infos WHERE ContractId = {contractId}");
        }
    }
}