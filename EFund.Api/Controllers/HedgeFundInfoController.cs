using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EFund.Domain.Models;
using EFund.Domain.Models.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EFund.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HedgeFundInfoController : ControllerBase
    {
        private readonly ILogger<HedgeFundInfoController> _logger;
        private readonly IHedgeFundInfosRepository _repository;
        
        public HedgeFundInfoController(ILogger<HedgeFundInfoController> logger, IHedgeFundInfosRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IEnumerable<HedgeFundInfo> GetHedgeFundInfos() => _repository.GetHedgeFundInfos();

        [HttpGet("{contractId}")]
        public HedgeFundInfo GetHedgeFundInfoByContractId(string contractId) =>
            _repository.GetHedgeFundInfoByContractId(contractId);
    }
}