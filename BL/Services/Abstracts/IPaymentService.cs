using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Abstracts
{
    public interface IPaymentService
    {
        public Task<string> CreateCheckoutSessionAsync(decimal amount, string currency);

    }
}
