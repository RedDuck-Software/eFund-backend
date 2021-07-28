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

namespace EFund.Api.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// </summary>
        /// <returns>Personalized user nonce</returns>
        [HttpPost("register")]
        public async Task<string> RegisterUser([FromBody] RegisterUserRequest request )
        {
            return await _userService.RegisterUser(request);
        }

        [HttpPost("updateUserInfo"), DisableRequestSizeLimit]
        public async Task<string> UpdateUserInfo(
                [FromForm] IFormFile image,
                [FromForm] UpdateUserInfoRequest request)
        {
            return await _userService.UpdateUserInfo(image, request);
        }


        public string GetGenericSignNonce() => Config.GenericSingNonce;
    }
}
