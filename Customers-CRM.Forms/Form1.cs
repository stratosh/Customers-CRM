using Customers_CRM.Forms.Classes;
using Customers_CRM.Forms.Functions;
namespace Customers_CRM.Forms
{
    public partial class Form1 : Form
    {
        private readonly IIGetCustomers _getCustomers;
        private DataGridView dataGridView1;
        private Result result;
        List<Customer> customers = new List<Customer>();
        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();

            _getCustomers = new GetCustomers();

        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                customers = await _getCustomers.GetCustomersAsync();
                dataGridView1.DataSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Πρόβλημα στην φόρτωση των πελατών {ex.Message}");
            }
        }
        private void InitializeDataGridView()
        {
            dataGridView1 = new DataGridView
            {
                Dock = DockStyle.Top,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = true,

                AllowUserToDeleteRows = false,
            };
            this.Controls.Add(dataGridView1);
            dataGridView1.CellValueChanged += DataGridView1_CellValueChanged;
        }
        private async void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                OnCellValueChanged(e.RowIndex, e.ColumnIndex);
            }
        }
        private async void OnCellValueChanged(int rowIndex, int columnIndex)
        {
            var newValue = dataGridView1.Rows[rowIndex].Cells[columnIndex].Value;
            result = await _getCustomers.EditCustomer(Convert.ToString(newValue), rowIndex, columnIndex);
            if (result != null && result.success == true)
            {
                MessageBox.Show($"Επιτυχής αλλαγή σε: {newValue}");
            }
            else
            {
                MessageBox.Show($"Αποτυχία αλλαγής κελιού");
            }
        }
        private async void saveButton_Click(object sender, EventArgs e)
        {
            bool nameExist = customers.Any(c => c.Name.Equals(onomaText.Text, StringComparison.OrdinalIgnoreCase));
            bool telExists = customers.Any(c => c.Phone.Equals(tilText.Text, StringComparison.OrdinalIgnoreCase));
            bool tel2Exists = customers.Any(c => c.Phone.Equals(til2Text.Text, StringComparison.OrdinalIgnoreCase));
            var message = (nameExist, telExists, tel2Exists) switch
            {
                (true, _, _) => $"Υπάρχει ήδη πελάτης με το όνομα: {onomaText.Text}",
                (_, true, _) => $"Υπάρχει ήδη πελάτης με τον αριθμό: {tilText.Text}",
                (_, _, true) => $"Υπάρχει ήδη πελάτης με τον αριθμό: {til2Text.Text}",
                _ => null
            };
            if (message != null)
            {
                MessageBox.Show(message);
            }
            else
            {
                Customer customer = new Customer();
                customer.Name = onomaText.Text;
                customer.Address = addressText.Text;
                if (!emailText.Text.Contains("@"))
                {
                    MessageBox.Show("Παρακαλώ εισάγετε ένα έγκυρο email.");
                    return;
                }
                else
                {
                    customer.Email = emailText.Text;
                }
                customer.City = cityText.Text;
                if (int.TryParse(tkText.Text, out int postalCode))
                {
                    customer.PostalCode = postalCode;
                }
                else
                {
                    MessageBox.Show($"Παρακαλώ εισάγετε σωστό ταχυδρομικό κώδικα");
                    return;
                }
                customer.Phone = tilText.Text;
                customer.Phone2 = til2Text.Text;
                try
                {
                    result = await _getCustomers.AddCustomer(customer);
                    if (result.success == true)
                    {
                        MessageBox.Show($"Ο πελάτης με το όνομα: {customer.Name} εισήχθει επιτυχώς");
                        customers = await _getCustomers.GetCustomersAsync();
                        dataGridView1.DataSource = customers;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Πρόβλημα στην φόρτωση των πελατών {ex.Message}");
                }
            }
        }
    }
}