using Dapper;
using EFund.Database.Entities;
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
        private readonly int _chainId;

        public UsersRepository(string connectionString, int chainId) : base(connectionString)
        {
            _chainId = chainId;
        }

        public async Task AddNewUser(string userAddress, string signNonce)
        {
            await SqlConnection.ExecuteAsync(
                    $"Insert into users(Address,ChainId,SignNonce) VALUES(CONVERT(binary(20),{userAddress},1),{_chainId},{signNonce})");
        }

        public async Task UpdateUser(User user)
        {
            await SqlConnection.ExecuteAsync(
                $"Update users WHERE Address=CONVERT(binary(20),{user.Address},1) and ChainId={_chainId} SET SignNonce={user.SignNonce}, ImageUrl={user.ImgUrl}, Description={user.Description}, Username={user.Username}");
        }

        public async Task UpdateUserNonce(User user, string newNonce)
        {
            await SqlConnection.ExecuteAsync(
                $"Update users WHERE Address=CONVERT(binary(20),{user.Address},1) and ChainId={_chainId} SET SignNonce={newNonce}");
        }


        public async Task<User> GetUserByAddress(string address)
        {
            return await SqlConnection.QueryFirstOrDefaultAsync<User>(
                $"SELECT * FROM users WHERE Address=CONVERT(binary(20),{address},1) and ChainId={_chainId}");
        }
    }
}
