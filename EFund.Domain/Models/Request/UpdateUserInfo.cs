using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Models.Request
{
    public class UpdateUserInfoRequest : RequestBase
    {
        public string Address { get; set; }

        public string Username { get; set; }

        public string Description { get; set; }
    }
}
