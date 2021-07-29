using Api.Service;
using EFund.Api.Service;
using EFund.Database.Entities;
using EFund.Database.Repositories.Dapper;
using EFund.Domain.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Nethereum.Signer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Services
{
    public interface IUserService
    {
        Task<string> UpdateUserInfo(IFormFile image, UpdateUserInfoRequest req);
        Task<string> RegisterUser(RegisterUserRequest model);
        Task<User> GetUserByAddress(string userAddress);
    }


    public class UserService : IUserService
    {
        private readonly string _connectionString;

        private readonly ImageService _imageService;

        private readonly int _chainId;


        public UserService(int chainId, IConfiguration configuration, ImageService imageService)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _imageService = imageService;
            _chainId = chainId;
        }


        public async Task<string> UpdateUserInfo(IFormFile image, UpdateUserInfoRequest req)
        {
            using (var uRepo = new UsersRepository(_connectionString, _chainId))
            {
                if (await uRepo.GetUserByAddress(req.Address) != null)
                {
                    var imgPath = await _imageService.SaveImage(image, req.Address, _chainId);
                    var nonce = GenerateNonce();

                    await uRepo.UpdateUser(new User { Address = req.Address, Description = req.Description, SignNonce = nonce, ImageUrl = imgPath, Username = req.Username });

                    return nonce;
                }
                else
                    return null;
            }
        }

        public async Task<User> GetUserByAddress(string userAddress)
        {
            using (var uRepo = new UsersRepository(_connectionString, _chainId))
                return await uRepo.GetUserByAddress(userAddress);
        }

        private string GenerateNonce()
        {
            return RandomStringGenerator.Generate(40);
        }

        public async Task<string> RegisterUser(RegisterUserRequest model)
        {
            var nonce = GenerateNonce();

            if (!ValidateNonce(Config.GenericSingNonce, model.SignedGenericNonce, model.Address))
                throw new Exception("Generic nonce signed wrong");


            using (var uRepo = new UsersRepository(_connectionString, _chainId))
            {
                if (await uRepo.GetUserByAddress(model.Address) == null)
                {
                    await uRepo.AddNewUser(model.Address, nonce);
                }
                else
                {
                    return null;
                }
            }


            return nonce;
        }

        private bool ValidateNonce(string original, string signed, string signer)
        {
            return true;

            var msgSigner = new EthereumMessageSigner();

            var addr = msgSigner.EncodeUTF8AndEcRecover(original, signed);

            return addr == signer;
        }
    }
}
