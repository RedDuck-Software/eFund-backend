using System.Collections.Generic;

namespace EFund.Domain.Models.Repositories.Abstract
{
    public interface IHedgeFundInfosRepository
    {
        IEnumerable<HedgeFundInfo> GetHedgeFundInfos();
        
        HedgeFundInfo? GetHedgeFundInfoByContractId(string contractId);
        
        void SaveHedgeFundInfo(HedgeFundInfo hedgeFundInfo);
        
        void DeleteHedgeFundInfoByContractId(string contractId);
    }
}