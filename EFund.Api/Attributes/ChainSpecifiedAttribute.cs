using EFund.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFund.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ChainSpecifiedAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if ((Network)context.HttpContext.Items["Network"] == null)
            {
                context.Result = new JsonResult(new { message = "ChainId must be specified as request header" })
                {
                    StatusCode = StatusCodes.Status405MethodNotAllowed
                };
            }
        }
    }
}
