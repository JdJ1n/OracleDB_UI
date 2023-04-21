using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace SMI1002_TP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool re = false;
            string message = "";
            Form5 form5 = new();
            if (form5.ShowDialog() == DialogResult.OK)
            {
                string connectionString = form5.ReturnValue;
                //"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.16.25.43)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=coursbd.uqtr.ca)));User Id=SMI1002_017;Password=76gesj98;"
                try
                {
                    OracleConnection? conn = ConnexionOracle.DbConn(connectionString, ref message, ref re);
                    if (conn == null)
                    {
                        MessageBox.Show("Echec de la connexion ¨¤ la base de donn¨¦es :" + message);
                    }
                    else if (re)
                    {
                        MessageBox.Show("Connexion ¨¤ la base de donn¨¦es r¨¦ussie");
                        string sql = string.Format("SELECT table_name FROM user_tables");
                        DataTable? dt = ConnexionOracle.SelectSql(sql, ref message);
                        this.dataGridView1.DataSource = dt;
                        if (dataGridView1.SelectedCells.Count > 0)
                        {
                            DataGridViewCell cell = dataGridView1.SelectedCells[0];
                            object value = cell.Value;
                            string? tableName = value.ToString();
                            if (tableName is not null)
                            {
                                string messagetable = "";
                                string sqltable = string.Format($"SELECT * FROM {tableName}");
                                DataTable? dt2 = ConnexionOracle.SelectSql(sqltable, ref messagetable);
                                this.dataGridView2.DataSource = dt2;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Echec de la connexion ¨¤ la base de donn¨¦es :" + message);
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Echec de la connexion ¨¤ la base de donn¨¦es :" + ex);
                }
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string message = "";
            if (e.RowIndex >= 0)
            {
                string? tableName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                if (tableName != null)
                {
                    string sql = string.Format($"SELECT * FROM {tableName}");
                    DataTable? dt = ConnexionOracle.SelectSql(sql, ref message);
                    this.dataGridView2.DataSource = dt;
                }

            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                DataGridViewCell cell = dataGridView1.SelectedCells[0];
                object value = cell.Value;
                string? tableName = value.ToString();
                if (tableName is not null)
                {
                    Form2 form2 = new(tableName);
                    if (form2.ShowDialog() == DialogResult.OK)
                    {
                        string message = "";
                        string sql = string.Format($"SELECT * FROM {tableName}");
                        DataTable? dt = ConnexionOracle.SelectSql(sql, ref message);
                        this.dataGridView2.DataSource = dt;
                    }
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0 && dataGridView1.SelectedCells.Count > 0)
            {
                DataGridViewCell cell = dataGridView2.SelectedCells[0];
                DataGridViewRow row = cell.OwningRow;
                string? tableName = dataGridView1.SelectedCells[0].Value.ToString();
                string message = "";
                //Ici, nous avons utilis¨¦ l'optimisation des requ¨ºtes de s¨¦lection pour obtenir le nom de la table actuellement affich¨¦e et la cl¨¦ primaire de l'option s¨¦lectionn¨¦e, en fonction des cellules s¨¦lectionn¨¦es par l'utilisateur dans les deux dataGridViews.
                string sql = $"SELECT cols.column_name FROM all_constraints cons, all_cons_columns cols WHERE cols.table_name = '{tableName}' AND cons.constraint_type = 'P' AND cons.constraint_name = cols.constraint_name AND cons.owner = cols.owner ORDER BY cols.table_name, cols.position";
                DataTable? dt = ConnexionOracle.SelectSql(sql, ref message);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string? primaryKeyColumnName = dt.Rows[0]["column_name"].ToString();
                    object primaryKeyValue = row.Cells[primaryKeyColumnName].Value;
                    string? primaryKey = primaryKeyValue.ToString();
                    if (tableName is not null && primaryKey is not null && primaryKeyColumnName is not null)
                    {
                        Form3 form3 = new(tableName, primaryKeyColumnName, primaryKey);
                        if (form3.ShowDialog() == DialogResult.OK)
                        {
                            string messagetable = "";
                            string sqltable = string.Format($"SELECT * FROM {tableName}");
                            DataTable? dt2 = ConnexionOracle.SelectSql(sqltable, ref messagetable);
                            this.dataGridView2.DataSource = dt2;
                        }
                    }
                }
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {

            if (dataGridView2.SelectedCells.Count > 0 && dataGridView1.SelectedCells.Count > 0)
            {
                DataGridViewCell cell = dataGridView2.SelectedCells[0];
                DataGridViewRow row = cell.OwningRow;
                string? tableName = dataGridView1.SelectedCells[0].Value.ToString();
                string message = "";
                string sql = $"SELECT cols.column_name FROM all_constraints cons, all_cons_columns cols WHERE cols.table_name = '{tableName}' AND cons.constraint_type = 'P' AND cons.constraint_name = cols.constraint_name AND cons.owner = cols.owner ORDER BY cols.table_name, cols.position";
                DataTable? dt = ConnexionOracle.SelectSql(sql, ref message);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string? primaryKeyColumnName = dt.Rows[0]["column_name"].ToString();
                    object primaryKeyValue = row.Cells[primaryKeyColumnName].Value;
                    string? primaryKey = primaryKeyValue.ToString();
                    if (tableName is not null && primaryKey is not null && primaryKeyColumnName is not null)
                    {
                        Form4 form4 = new(tableName, primaryKeyColumnName, primaryKey);
                        if (form4.ShowDialog() == DialogResult.OK)
                        {
                            string messagetable = "";
                            string sqltable = string.Format($"SELECT * FROM {tableName}");
                            DataTable? dt2 = ConnexionOracle.SelectSql(sqltable, ref messagetable);
                            this.dataGridView2.DataSource = dt2;
                        }
                    }
                }
            }


        }
    }
}