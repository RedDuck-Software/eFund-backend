using EFund.Domain.Services;

namespace EFund.Api.Service
{
    public class Config
    {
        public static string ConnectionString { get; set; }

        public static string GenericSingNonce => RandomStringGenerator.Generate(100);
    }
}