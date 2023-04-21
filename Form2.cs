using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace SMI1002_TP
{
    public partial class Form2 : Form
    {
        private readonly string tableName;
        public Form2(string tableName)
        {
            InitializeComponent();
            this.tableName = tableName;
            Affichage();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = $"INSERT INTO {tableName} (";
                string values = "VALUES (";
                List<OracleParameter> parameters = new();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    string columnName = column.Name;
                    string? value = dataGridView1.Rows[0].Cells[columnName].Value.ToString();
                    sql += columnName + ",";
                    values += $":{columnName},";
                    parameters.Add(new OracleParameter(columnName, value));
                }
                sql = sql.TrimEnd(',') + ") " + values.TrimEnd(',') + ")";
                string insertMessage = "";
                bool rowsAffected = ConnexionOracle.ExecuteSql(sql, parameters.ToArray(), ref insertMessage);
                if (rowsAffected)
                {
                    MessageBox.Show("Insertion réussie.");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Echec de l'insertion : " + insertMessage);
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Echec de l'insertion : " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Echec de l'insertion : " + ex.Message);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        private void Affichage()
        {
            if (tableName != null)
            {
                string message = "";
                string sql = "SELECT COLUMN_NAME, DATA_TYPE FROM USER_TAB_COLUMNS WHERE TABLE_NAME = :tableName";
                OracleParameter[] parameters = new OracleParameter[]
                {
            new OracleParameter("tableName", tableName)
                };
                DataTable? dt = ConnexionOracle.SelectSql(sql, parameters, ref message);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string? columnName = row["COLUMN_NAME"].ToString();
                        string? dataType = row["DATA_TYPE"].ToString();
                        DataGridViewColumn column = new DataGridViewTextBoxColumn
                        {
                            Name = columnName,
                            HeaderText = columnName + "(" + dataType + ")",
                            Tag = dataType
                        };
                        dataGridView1.Columns.Add(column);
                    }
                }
            }
            dataGridView1.Rows.Add();
        }
    }
}
