using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using EFund.Domain.Models;
using EFund.Domain.Models.Repositories.Abstract;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EFund.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HedgeFundInfoController : ControllerBase
    {
        private readonly ILogger<HedgeFundInfoController> _logger;

        private readonly IHedgeFundService _hedgeFundService;

        public HedgeFundInfoController(ILogger<HedgeFundInfoController> logger, IHedgeFundService hedgeFundService)
        {
            _logger = logger;
            _hedgeFundService = hedgeFundService;
        }

        [HttpPost("createFundInfo"), DisableRequestSizeLimit]
        public async Task CreateHedgeFundInfo(
                                                [FromForm] IFormFile image,
                                                [FromForm] string contractaddress,
                                                [FromForm] string description,
                                                [FromForm] string name)
        {
            await _hedgeFundService.CreateNewFundInfo(image, new HedgeFundInfo { ContractAddress = contractaddress, Description = description, Name = name });
        }

        [HttpGet("{contractAddress}")]
        public async Task<HedgeFundInfo> GetHedgeFundInfoByContractId(string contractAddress) =>
            await _hedgeFundService.GetHedgeFundInfoByContractAddress(contractAddress);
    }
}