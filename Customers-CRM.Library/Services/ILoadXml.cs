namespace Customers_CRM.Library.Services
{
    public interface ILoadXml
    {
        public Task<List<Customer>> LoadCustomers(string xmlFilePath);
        public Task<Result> EditCustomers(string xmlFilePath, EditCustomers editData);
        public Task<Result> AddCustomers(string xmlFilePath, Customer customer);
        public string RemoveLastNDirectories(string path, int n);
    }
}