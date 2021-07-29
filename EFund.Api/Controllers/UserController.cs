using EFund.Api.Service;
using EFund.Database.Entities;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFund.Domain.Models.Request;
using EFund.Api.Attributes;
using Microsoft.Extensions.Configuration;
using Api.Service;

namespace EFund.Api.Controllers
{
    public class UserController : BaseController
    {

        protected IUserService UserService => new UserService(CurrentNetwork.ChainId, Configuration, ImageService);

        private IConfiguration Configuration { get; }

        private ImageService ImageService { get; }


        public UserController(IConfiguration configuration, ImageService imageService)
        {
            Configuration = configuration;
            ImageService = imageService;
        }

        /// <summary>
        /// </summary>
        /// <returns>Personalized user nonce</returns>
        [HttpPost("register")]
        [ChainSpecified]
        public async Task<string> RegisterUser([FromBody] RegisterUserRequest request )
        {
            return await UserService.RegisterUser(request);
        }

        /// <summary>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="request"></param>
        /// <returns>New nonce</returns>
        [HttpPost("updateUserInfo"), DisableRequestSizeLimit]
        [ChainSpecified]
        public async Task<string> UpdateUserInfo(
                [FromForm] IFormFile image,
                [FromForm] UpdateUserInfoRequest request)
        {
            return await UserService.UpdateUserInfo(image, request);
        }


        public string GetGenericSignNonce() => Config.GenericSingNonce;
    }
}
