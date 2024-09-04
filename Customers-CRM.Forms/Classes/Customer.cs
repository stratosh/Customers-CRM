namespace Customers_CRM.Forms.Classes
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
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Phone2 { get; set; } = string.Empty;
    }
    public class EditCustomers
    {
        public string Value { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}