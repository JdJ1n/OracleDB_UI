using System.Data;

namespace SMI1002_TP
{
    public partial class FormTrace : Form
    {
        public FormTrace()
        {
            InitializeComponent();
            string message = "";
            string sql = "SELECT * FROM AUDIT_TABLE WHERE OPERATION IN ('INSERT', 'UPDATE', 'DELETE')";
            DataTable? dt = ConnexionOracle.SelectSql(sql, ref message);
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
