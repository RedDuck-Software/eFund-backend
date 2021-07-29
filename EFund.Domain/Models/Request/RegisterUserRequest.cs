using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Models.Request
{
    public class RegisterUserRequest
    {
        [RegularExpression("^0x[a-fA-F0-9]{40}$")]
        public string Address { get; set; }
        
        public string SignedGenericNonce { get; set; }
    }
}
