using EFund.Database.Entities;
using EFund.Domain.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFund.Api.Middlewares
{
    public class ChainMiddleware
    {
        private readonly RequestDelegate _next;

        public ChainMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, INetworkService networkService)
        {
            var header = context.Request.Headers["ChainId"];

            if (header.Any())
            {
                int chainId = Convert.ToInt32(context.Request.Headers["ChainId"]);

                var network = await networkService.GetNetworkByChainId(chainId);

                if (network != null)
                    context.Items["Network"] = network;
            }

            await _next(context);
        }
    }
}
