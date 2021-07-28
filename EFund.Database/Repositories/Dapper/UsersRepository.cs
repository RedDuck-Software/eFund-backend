using EFund.Domain.Models.Repositories.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Database.Repositories.Dapper
{
    public class UsersRepository : RepositoryBase
    {
        public UsersRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
