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
using System.ComponentModel.DataAnnotations;

namespace EFund.Api.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<string>> RegisterUser([FromBody] RegisterUserRequest request )
        {
           var res =  await UserService.RegisterUser(request);

            if (res == null)
                return BadRequest("User with this address is already exists");

            return res;
        }

        /// <summary>
        /// </summary>
        /// <param name="image"></param>
        /// <param name="request"></param>
        /// <returns>New nonce</returns>
        [HttpPost("updateUserInfo"), DisableRequestSizeLimit]
        [ChainSpecified]
        public async Task<string> UpdateUserInfo(
                IFormFile image,
                [FromQuery] UpdateUserInfoRequest request)
        {
            return await UserService.UpdateUserInfo(image, request);
        }

        [HttpGet("{userAddress}")]
        [ChainSpecified]
        public async Task<User> GetHedgeFundInfoByContractId([RegularExpression("^0x[a-fA-F0-9]{40}$")] string userAddress) =>
            await UserService.GetUserByAddress(userAddress);

        [HttpGet("getGenericSignNonce")]
        public string GetGenericSignNonce() => Config.GenericSingNonce;
    }
}
