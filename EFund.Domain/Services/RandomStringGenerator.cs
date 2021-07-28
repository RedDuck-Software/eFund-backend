using EFund.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFund.Domain.Services
{
    public static class RandomStringGenerator
    {
        public static string Generate(int length)
        {
            const string template = "`1234567890QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
            return string.Join("", template.Take(length).Select(_ => template.RandomElement()));
        }
    }
}
