namespace Customers_CRM.Library.Services
{
    public interface ILoadXml
    {
        public Task<List<Customer>> LoadCustomers(string xmlFilePath);
    }
}
