using Api.Service;
using EFund.Database.Entities;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFund.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Network CurrentNetwork => (Network)HttpContext.Items["Network"];
    }
}
