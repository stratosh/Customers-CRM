using Customers_CRM.Library.Classes;
using Newtonsoft.Json;

namespace Customers_CRM.Forms.Functions
{
    public class GetCustomers : IIGetCustomers
    {

        public GetCustomers() { }

 

        public async Task<List<Customer>> FetchCustomersFromApiAsync()
        {
            var httpClient = new HttpClient();

            List<Customer> customers = new List<Customer>();
            try
            {
                string apiUrl = "http://localhost:7290/GetCustomers"; 
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, null);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                customers = JsonConvert.DeserializeObject<List<Customer>>(responseContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching customers: {ex.Message}");
            }

            return customers;
        }
    }
}
