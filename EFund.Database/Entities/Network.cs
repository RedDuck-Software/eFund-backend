using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Database.Entities
{
    public class Network : ChainDependModelBase
    {
        public string PlatformContractAddress { get; set; }
    }
}
