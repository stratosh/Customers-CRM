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
        string baseDirectory = AppContext.BaseDirectory;
        private readonly string _xmlFilePath;
        private Result result;
        public CustomersController(ILogger<CustomersController> logger, ILoadXml xml, IConfiguration configuration)
        {
            _logger = logger;
            _xml = xml;
            string newPath = _xml.RemoveLastNDirectories(baseDirectory, 4);
            string relativePath = configuration["FilePaths:CustomersXml"];
            _xmlFilePath = Path.Combine(newPath, relativePath);
        }
        [HttpGet]
        [Route("[controller]/GetCustomers")]
        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                customers = await _xml.LoadCustomers(_xmlFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return customers;
        }
        [HttpPost]
        [Route("[controller]/EditCustomers")]
        public async Task<IActionResult> Edit([FromBody] EditCustomers editData)
        {
            result = await _xml.EditCustomers(_xmlFilePath, editData);
            return Ok(result);
        }
        [HttpPost]
        [Route("[controller]/AddCustomers")]
        public async Task<IActionResult> Add([FromBody] Customer customer)
        {
            result = await _xml.AddCustomers(_xmlFilePath, customer);
            return Ok(result);
        }
    }
}