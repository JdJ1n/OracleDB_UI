using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace SMI1002_TP
{
    public class ConnexionOracle
    {
        private static string? connectStr;
        #region ConnexionBD
        public static OracleConnection? DbConn(ref string message, ref bool re)
        {
            //La façon dont la base de données est connectée
            OracleConnection conn;
            re = false;
            message = "";
            conn = new OracleConnection(connectStr);
            try
            {
                conn.Open();
                re = true;
            }
            catch (Exception ex)
            {
                message = "Erreur :" + ex.Message.ToString();
                re = false;
                return null;
            }
            finally
            {
                conn.Close();
            }

            return conn;
        }
        #endregion

        #region ConnexionBDstr
        public static OracleConnection? DbConn(string connectionString, ref string message, ref bool re)
        {
            //La façon dont la base de données est connectée
            OracleConnection conn;
            re = false;
            message = "";
            connectStr = connectionString;
            conn = new OracleConnection(connectStr);
            try
            {
                conn.Open();
                re = true;
            }
            catch (Exception ex)
            {
                message = "Erreur :" + ex.Message.ToString();
                re = false;
                return null;
            }
            finally
            {
                conn.Close();
            }

            return conn;
        }
        #endregion

        #region Transaction
        public static bool ExecuteSql(string sql, OracleParameter[] parameters, ref string message)
        {
            bool re = false;
            OracleTransaction? transaction = null;
            try
            {
                OracleConnection? conn = DbConn(ref message, ref re);
                if (conn == null)
                {
                    re = false;
                    message = "L'objet de connexion à la base de données est null";
                }
                else
                {
                    try
                    {
                        if (conn.State == ConnectionState.Closed)
                        {
                            conn.Open();
                        }
                    }
                    catch (Exception e)
                    {
                        re = false;
                        message = e.Message.ToString();
                    }
                    // Début d'une transaction
                    transaction = conn.BeginTransaction(IsolationLevel.Serializable);
                    OracleCommand cmd = new(sql, conn);
                    cmd.Parameters.AddRange(parameters);
                    int count = cmd.ExecuteNonQuery();
                    if (count > 0)
                    {
                        // Soumission d'une transaction
                        transaction.Commit();
                        re = true;
                    }
                    else
                    {
                        // Annulation d'une transaction
                        transaction.Rollback();
                        re = false;
                        message = "Échec de la transaction des données";
                    }
                    cmd.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ee)
            {
                // Annulation d'une transaction
                transaction?.Rollback();
                re = false;
                message = "Erreur de la transaction due à :" + ee.Message.ToString();
            }
            return re;
        }
        #endregion

        #region Consultation
        public static DataTable? SelectSql(string sql, ref string message)
        {
            DataTable? dt = null;
            OracleConnection? conn;
            message = string.Empty;
            string err = "";
            bool re = false;
            try
            {
                conn = DbConn(ref err, ref re);
                if (conn == null)
                {
                    message = "L'objet de connexion à la base de données est null";
                    return null;
                }
                if (conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        return null;
                    }
                }
            }
            catch (Exception ee)
            {
                message = "La connexion à la base de données a échoué en raison de :" + ee.Message.ToString();
                return null;
            }
            // Début d'une transaction
            OracleTransaction transaction = conn.BeginTransaction();

            try
            {
                OracleDataAdapter adapter = new(sql, conn);
                DataSet set = new();
                adapter.Fill(set);
                dt = set.Tables[0];
                // Soumission d'une transaction
                transaction.Commit();
                conn.Close();
                conn.Dispose();
                adapter.Dispose();
                set.Dispose();
            }
            catch (Exception ex)
            {
                // Annulation d'une transaction
                transaction.Rollback();

                message = ex.Message.ToString(); ;
            }

            return dt;
        }

        #endregion
        public static DataTable? SelectSql(string sql, OracleParameter[] parameters, ref string message)
        {
            DataTable? dt = null;
            OracleConnection? conn;
            message = string.Empty;
            string err = "";
            bool re = false;
            try
            {
                conn = DbConn(ref err, ref re);
                if (conn == null)
                {
                    message = "L'objet de connexion à la base de données est null";
                    return null;
                }
                if (conn.State == ConnectionState.Closed)
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        message = ex.Message;
                        return null;
                    }
                }
            }
            catch (Exception ee)
            {
                message = "La connexion à la base de données a échoué en raison de :" + ee.Message.ToString();
                return null;
            }
            // Début d'une transaction
            OracleTransaction transaction = conn.BeginTransaction(IsolationLevel.Serializable);
            try
            {
                OracleDataAdapter adapter = new(sql, conn);
                adapter.SelectCommand.Parameters.AddRange(parameters);
                DataSet set = new();
                adapter.Fill(set);
                dt = set.Tables[0];
                // Soumission d'une transaction
                transaction.Commit();
                conn.Close();
                conn.Dispose();
                adapter.Dispose();
                set.Dispose();
            }
            catch (Exception ex)
            {
                // Annulation d'une transaction
                transaction.Rollback();
                message = ex.Message.ToString(); ;
            }
            return dt;
        }
    }
}
