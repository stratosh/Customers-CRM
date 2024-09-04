namespace Customers_CRM.Library.Classes
{
    [XmlRoot("People")]
    public class People
    {
        [XmlElement("Person")]
        public List<Customer> Customers { get; set; } = new List<Customer>();
    }

    public class Customer 
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int PostalCode { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
    }
}
