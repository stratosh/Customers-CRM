namespace Customers_CRM.Forms.Functions
{
    public class GetCustomers : IIGetCustomers
    {
        public GetCustomers() { }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            var httpClient = new HttpClient();

            List<Customer> customers = new List<Customer>();
            try
            {
                string apiUrl = "https://localhost:7290/Customers/GetCustomers"; 
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);
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
        public async Task<Result> EditCustomer(string value, int row, int column)
        {
            Result result = new Result();
            var httpClient = new HttpClient();
            EditCustomers res = new EditCustomers(); 
            res.Column = column;
            res.Value = value;
            res.Row = row;

            try
            {
                string apiUrl = "https://localhost:7290/Customers/EditCustomers";
                string jsonContent = JsonConvert.SerializeObject(res);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                result.success = true;

            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages = ex.Message;
            }
            return result;
        }
        public async Task<Result> AddCustomer(Customer customer)
        {
            Result result = new Result();
            var httpClient = new HttpClient();
            try
            {
                string apiUrl = "https://localhost:7290/Customers/AddCustomers";
                string jsonContent = JsonConvert.SerializeObject(customer);
                var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, httpContent);
                response.EnsureSuccessStatusCode();
                string responseContent = await response.Content.ReadAsStringAsync();
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages = ex.Message;
            }
            return result;
        }
    }
}