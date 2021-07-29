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

        public async Task AddNewUser(User user)
        {
            await SqlConnection.ExecuteAsync(
                    $"Insert into users(Address,ChainId,SignNonce,Username,Description,ImageUrl) " +
                    $"VALUES(CONVERT(binary(20),'{user.Address}',1),{_chainId},'{user.SignNonce}',{user.Username},{user.Description},{user.ImageUrl})");
        }

        public async Task UpdateUser(User user)
        {
            await SqlConnection.ExecuteAsync(
                $"Update users SET SignNonce=@SignNonce, ImageUrl=@ImageUrl, Description=@Description, Username=@Username " +
                    $"WHERE Address=CONVERT(binary(20),'{user.Address}',1) and ChainId={_chainId}", user);
        }

        public async Task UpdateUserNonce(User user, string newNonce)
        {
            await SqlConnection.ExecuteAsync(
                $"Update users WHERE Address=CONVERT(binary(20),{user.Address},1) and ChainId={_chainId} SET SignNonce={newNonce}");
        }


        public async Task<User> GetUserByAddress(string address)
        {
            return await SqlConnection.QueryFirstOrDefaultAsync<User>(
                $"SELECT " +
                $"convert(varchar(42),u.Address,1) as [Address] ," +
                $"u.ChainId, " +
                $"u.SignNonce, " +
                $"u.Username, " +
                $"u.ImageUrl ," +
                $"u.Description " +
                $"FROM users u WHERE u.Address=CONVERT(binary(20),{address},1) and u.ChainId={_chainId}");
        }
    }
}
