using Customers_CRM.Library.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers_CRM.Forms.Functions
{
    public interface IIGetCustomers
    {
        public Task<List<Customer>> FetchCustomersFromApiAsync();

    }
}
