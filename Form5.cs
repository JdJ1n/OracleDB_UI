namespace SMI1002_TP
{
    public partial class Form5 : Form
    {
        public string ReturnValue { get; set; }
        public Form5()
        {
            InitializeComponent();
            ReturnValue = string.Empty;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string host = textBox1.Text;
            string port = textBox2.Text;
            string sid = textBox3.Text;
            string uid = textBox4.Text;
            string pwd = textBox5.Text;
            ReturnValue = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={sid})));User Id={uid};Password={pwd};";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
