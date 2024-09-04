namespace Customers_CRM.Forms.Functions
{
    public interface IIGetCustomers
    {
        public Task<List<Customer>> GetCustomersAsync();
        public Task<Result> EditCustomer(string value, int row, int column);
        public Task<Result> AddCustomer(Customer customer);
    }
}