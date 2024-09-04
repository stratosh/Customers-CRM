using Customers_CRM.Forms.Functions;
namespace Customers_CRM.Forms
{
    public partial class Form1 : Form
    {
        private readonly IIGetCustomers _getCustomers;
        public Form1()
        {
            InitializeComponent();
            _getCustomers = new GetCustomers();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            _getCustomers.FetchCustomersFromApiAsync();
        }
    }
}
