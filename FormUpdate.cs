using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace SMI1002_TP
{
    public partial class FormUpdate : Form
    {
        private readonly string tableName;
        private readonly string primaryKeyColumnName;
        private readonly string primaryKey;
        public FormUpdate(string tableName, string primaryKeyColumnName, string primaryKey)
        {
            InitializeComponent();
            this.tableName = tableName;
            this.primaryKeyColumnName = primaryKeyColumnName;
            this.primaryKey = primaryKey;
            Affichage();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    DataGridViewRow row = dataGridView1.Rows[0];
                    List<string> setClauses = new();
                    List<OracleParameter> parameters = new();
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        string columnName = dataGridView1.Columns[i].Name;
                        object value = row.Cells[i].Value;
                        setClauses.Add($"{columnName} = :{columnName}");
                        parameters.Add(new OracleParameter(columnName, value));
                    }
                    string setClause = string.Join(", ", setClauses);
                    string sql = $"UPDATE {tableName} SET {setClause} WHERE {primaryKeyColumnName} = :primaryKey";
                    parameters.Add(new OracleParameter("primaryKey", primaryKey));
                    string message = "";
                    bool rowsAffected = ConnexionOracle.ExecuteSql(sql, parameters.ToArray(), ref message);
                    if (rowsAffected)
                    {
                        MessageBox.Show("Modifié avec succès");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Echec de la modification : " + message);
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Echec de la modification : " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                MessageBox.Show("Echec de la modification : " + ex.Message);
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

                sql = $"SELECT * FROM {tableName} WHERE {primaryKeyColumnName} = :primaryKey";
                parameters = new OracleParameter[]
                {
            new OracleParameter("primaryKey", primaryKey)
                };
                dt = ConnexionOracle.SelectSql(sql, parameters, ref message);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    object[] values = new object[dataGridView1.Columns.Count];
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        string columnName = dataGridView1.Columns[i].Name;
                        values[i] = row[columnName];
                    }
                    dataGridView1.Rows.Add(values);
                }
            }
        }
    }
}
