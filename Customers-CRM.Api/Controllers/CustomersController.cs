using Customers_CRM.Library.Classes;
using Customers_CRM.Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace Customers_CRM.Api.Controllers
{
    [ApiController]
    public class CustomersController : Controller
    {
        public ILoadXml _xml;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger, ILoadXml xml)
        {
            _logger = logger;
            _xml = xml;
        }

        [HttpPost]
        [Route("[controller]/GetCustomers")]

        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                string filePath = "customers.xml"; 

                customers = await _xml.LoadCustomers(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return customers;
        }
    }
}
