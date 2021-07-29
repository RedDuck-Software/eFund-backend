using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api.Service;
using EFund.Api.Attributes;
using EFund.Database.Entities;
using EFund.Domain.Models;
using EFund.Domain.Models.Repositories.Abstract;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFund.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HedgeFundInfoController : BaseController
    {
        protected IHedgeFundService HedgeFundService => new HedgeFundService(CurrentNetwork.ChainId, Configuration, ImageService);

        private IConfiguration Configuration { get; }

        private ImageService ImageService { get; }


        public HedgeFundInfoController(IConfiguration configuration, ImageService imageService)
        {
            Configuration = configuration;
            ImageService = imageService;
        }

        [HttpPost("createFundInfo"), DisableRequestSizeLimit]
        [ChainSpecified]
        public async Task CreateHedgeFundInfo(
                                                [FromForm] IFormFile image,
                                                [FromForm] string description,
                                                [Required] [FromForm] string contractaddress,
                                                [Required] [FromForm] string name)
        {
            await HedgeFundService.CreateNewFundInfo(image, new HedgeFundInfo { ContractAddress = contractaddress, Description = description, Name = name });
        }

        [HttpGet("{contractAddress}")]
        [ChainSpecified]
        public async Task<HedgeFundInfo> GetHedgeFundInfoByContractId(string contractAddress) =>
            await HedgeFundService.GetHedgeFundInfoByContractAddress(contractAddress);
    }
}