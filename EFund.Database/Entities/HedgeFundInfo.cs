namespace EFund.Database.Entities
{
    public class HedgeFundInfo : ChainDependModelBase
    {
        public string ContractAddress { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
    }
}