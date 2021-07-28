using EFund.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFund.Domain.Models.Repositories.Abstract
{
    public interface IHedgeFundInfosRepository
    {
        Task<HedgeFundInfo> GetHedgeFundInfoByContractAddress(string contractId);
        
        Task SaveHedgeFundInfo(HedgeFundInfo hedgeFundInfo);
        
        Task DeleteHedgeFundInfoByContractId(string contractId);
    }
}