using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFund.Domain.Models.Repositories.Abstract
{
    public interface IHedgeFundInfosRepository
    {
        Task<IEnumerable<HedgeFundInfo>> GetHedgeFundInfos();
        
        Task<HedgeFundInfo> GetHedgeFundInfoByContractAddress(string contractId);
        
        Task SaveHedgeFundInfo(HedgeFundInfo hedgeFundInfo);
        
        Task DeleteHedgeFundInfoByContractId(string contractId);
    }
}