namespace Customers_CRM.Library.Services
{
    public class LoadXml : ILoadXml
    {
        public async Task<List<Customer>> LoadCustomers(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath))
                throw new ArgumentException("XML file path cannot be null or empty.", nameof(xmlFilePath));

            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException("The specified XML file was not found.", xmlFilePath);

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(People));
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
                {
                    People people = (People)serializer.Deserialize(fs);
                    return people.Customers;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while loading and deserializing the XML.", ex);
            }
        }
    }
}
