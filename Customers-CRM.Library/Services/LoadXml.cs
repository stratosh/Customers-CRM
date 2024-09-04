namespace Customers_CRM.Library.Services
{
    public class LoadXml : ILoadXml
    {
        public LoadXml() { }
        public async Task<List<Customer>> LoadCustomers(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath))
                throw new ArgumentException("To xmlFilePath είναι κενό", nameof(xmlFilePath));

            if (!File.Exists(xmlFilePath))
                throw new FileNotFoundException("Το XML δεν βρέθηκε στο path: ", xmlFilePath);
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(People));
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    People people = (People)serializer.Deserialize(fs);
                    fs.Close();
                    return people.Customers;
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Βρέθηκε πρόβλημα στην αποκωδοικοποίηση του People.", ex);
            }
        }
        public async Task<Result> EditCustomers(string xmlFilePath, EditCustomers editData)
        {

            Result result = new Result();
            if (editData == null)
            {
                result.success = false;
                result.messages = "Κενά ή λανθασμένα δεδομένα αλλαγής χρήστη";
                return result;
            }
            try
            {
                if (!System.IO.File.Exists(xmlFilePath))
                {
                    result.success = false;
                    result.messages = "Το αρχείο xml δεν βρέθηκε.";
                    return result;
                }
                XDocument xdoc;
                using (var fileStream = new FileStream(xmlFilePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    xdoc = XDocument.Load(fileStream);
                    var personElement = xdoc.Descendants("Person").ElementAtOrDefault(editData.Row);
                    if (personElement == null)
                    {
                        result.success = false;
                        result.messages = "Η γραμμή δεν βρέθηκε.";
                        return result;
                    }
                    var elementToUpdate = personElement.Elements().ElementAtOrDefault(editData.Column);
                    if (elementToUpdate == null)
                    {
                        result.success = false;
                        result.messages = "Η στήλη δεν βρέθηκε.";
                        return result;
                    }
                    elementToUpdate.Value = editData.Value;
                    fileStream.SetLength(0); 
                    fileStream.Seek(0, SeekOrigin.Begin); 
                    xdoc.Save(fileStream);
                }
                result.success = true;
                return result;
            }
            catch (IOException ioEx)
            {
                result.success = false;
                result.messages = $"File I/O error: {ioEx.Message}";
                return result;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages = $"Unexpected error: {ex.Message}";
                return result;
            }
        }
        public async Task<Result> AddCustomers(string xmlFilePath, Customer customer)
        {
            Result result = new Result();
            try
            {
                XDocument xdoc;
                using (var fileStream = new FileStream(xmlFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    xdoc = XDocument.Load(fileStream);
                }
                XElement root = xdoc.Element("People");
                if (root == null)
                {
                    root = new XElement("People");
                    xdoc.Add(root);
                }
                XElement newPerson = new XElement("Person",
                    new XElement("Name", customer.Name),
                    new XElement("Address", customer.Address),
                    new XElement("City", customer.City),
                    new XElement("PostalCode", customer.PostalCode),
                    new XElement("Email", customer.Email),
                    new XElement("Phone", customer.Phone),
                    new XElement("Phone2", customer.Phone2)
                );
                root.Add(newPerson);
                using (var fileStream = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    xdoc.Save(fileStream);
                }
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.messages = ex.Message;
            }
            return result;
        }
        public string RemoveLastNDirectories(string path, int n)
        {
            string resultPath = path;
            for (int i = 0; i < n; i++)
            {
                resultPath = Directory.GetParent(resultPath)?.FullName;
            }
            return resultPath;
        }
    }
}