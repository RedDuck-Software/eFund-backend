namespace EFund.Database.Entities
{
    public class User : ChainDependModelBase
    {
        public string Address { get; set; }

        public string SignNonce { get; set; }

        public string ImgUrl { get; set; }

        public string Username { get; set; }

        public string Description { get; set; }
    }
}
