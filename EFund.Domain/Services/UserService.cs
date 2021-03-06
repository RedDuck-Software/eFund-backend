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
        Task<string> RegisterUser(UpdateUserInfoRequest model, IFormFile image = null);
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
                var user = await uRepo.GetUserByAddress(req.Address);

                if (!ValidateNonce(user.SignNonce, req.SignedNonce, req.Address))
                    throw new Exception("Nonce signed wrong");

                if (user != null)
                {
                    var imgPath = image == null ? user.ImageUrl : await _imageService.SaveImage(image, req.Address, _chainId);

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

        public async Task<string> RegisterUser(UpdateUserInfoRequest model, IFormFile image = null)
        {
            if (model == null)
                throw new ArgumentNullException("Model cannot be null");


            if (!ValidateNonce(Config.GenericSingNonce, model.SignedNonce, model.Address))
                throw new Exception("Generic nonce signed wrong");


            using (var uRepo = new UsersRepository(_connectionString, _chainId))
            {
                if (await uRepo.GetUserByAddress(model.Address) == null)
                {
                    string imgPath = image == null ? null : await _imageService.SaveImage(image, model.Address, _chainId);

                    var nonce = GenerateNonce();

                    await uRepo.AddNewUser(new User
                    {
                        Address = model.Address,
                        ChainId = _chainId,
                        Description = model.Description,
                        ImageUrl = imgPath,
                        SignNonce = nonce,
                        Username = model.Username
                    });
                    return nonce;

                }
            }

            return null;
        }

        private bool ValidateNonce(string original, string signed, string signer)
        {
            var msgSigner = new EthereumMessageSigner();

            var addr = msgSigner.EncodeUTF8AndEcRecover(original, signed);

            return addr == signer;
        }
    }
}
