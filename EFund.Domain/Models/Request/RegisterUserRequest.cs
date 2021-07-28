using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Models.Request
{
    public class RegisterUserRequest : RequestBase
    {
        public string Address { get; set; }
        
        public string SignedGenericNonce { get; set; }
    }
}
