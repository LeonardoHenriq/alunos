using System.Data;
using System.Data.SqlClient;

namespace Aluno.Persistence.Util
{
    public class Connection
    {
        private static readonly string _connectionString = @"Provider=SQLOLEDB.1;Persist Security Info=False;User ID=sa;Initial Catalog=PRINT_MANAGER;Data Source=DESKTOP-QB9VO73\SQL2014";

        private int timeout = 3000;
        public int Timeout { get { return timeout; } set { timeout = value; } }

        internal SqlConnection sqlConnection;

        private List<SqlConnection> sqlConnections;
        private bool keepConnection = false;
        public SqlTransaction Transaction;

        public Connection()
        {
            try
            {
                sqlConnections = new List<SqlConnection>();
                this.sqlConnection = new SqlConnection();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public Connection(bool keepConnection) : this()
        {
            this.keepConnection = keepConnection;
        }

        public void Open()
        {
            if (this.sqlConnection.State == ConnectionState.Closed && (this.sqlConnection.ConnectionString ?? "") != "")
                this.sqlConnection.Open();
        }
        public void Close()
        {
            foreach (SqlConnection c in sqlConnections)
            {
                if (c.State == ConnectionState.Open)
                {
                    c.Close();
                    SqlConnection.ClearPool(c);
                }
            }
        }
        public static string GetConnectionString()
        {
            string connectionString = string.Empty;
            try
            {
                connectionString = _connectionString;
            }
            catch { }
            return connectionString;
        }
        public SqlConnection GetConnection()
        {
            if (sqlConnection.State == ConnectionState.Open && this.keepConnection)
                return sqlConnection;

            this.sqlConnection = new SqlConnection(GetConnectionString());

            keepConnection = true;

            sqlConnections.Add(sqlConnection);
            return sqlConnection;
        }

        public DataSet GetDataSet(SqlDataAdapter daSql)
        {
            DataSet dsSet;

            dsSet = new DataSet();
            daSql.SelectCommand.Connection = this.GetConnection();
            daSql.SelectCommand.CommandTimeout = timeout;
            this.Open();
            if (this.Transaction != null)
                daSql.SelectCommand.Transaction = Transaction;

            try
            {
                daSql.Fill(dsSet);
            }
            catch (Exception Ex)
            {
                this.Close();
                throw Ex;
            }
            return dsSet;
        }

        public DataSet GetDataSet(SqlCommand cmdSql)
        {
            return this.GetDataSet(new SqlDataAdapter(cmdSql));
        }
        public DataSet GetDataSet(string strSql)
        {
            return this.GetDataSet(new SqlCommand(strSql));
        }

        public SqlDataReader GetDataReader(SqlCommand cmdSql)
        {
            cmdSql.CommandTimeout = timeout;

            if (this.Transaction != null)
            {
                cmdSql.Connection = sqlConnection;
                cmdSql.Transaction = Transaction;
            }
            else
            {
                cmdSql.Connection = this.GetConnection();
                this.Open();
            }

            if (this.Transaction != null)
                cmdSql.Transaction = Transaction;


            try
            {
                if (this.keepConnection)
                    return cmdSql.ExecuteReader();
                else
                    return cmdSql.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception Ex)
            {
                this.Close();
                throw Ex;
            }
        }
        public SqlDataReader GetDataReader(string strSql)
        {
            return this.GetDataReader(new SqlCommand(strSql));
        }
        public DataTable GetDataTable(SqlDataAdapter daSql)
        {
            DataTable dtTable;

            dtTable = new DataTable();
            daSql.SelectCommand.Connection = this.GetConnection();
            daSql.SelectCommand.CommandTimeout = timeout;
            this.Open();
            if (this.Transaction != null)
                daSql.SelectCommand.Transaction = Transaction;

            try
            {
                daSql.Fill(dtTable);
            }
            catch (Exception Ex)
            {
                this.Close();
                throw Ex;
            }
            return dtTable;
        }

        public DataTable GetDataTable(SqlCommand cmdSql)
        {
            return this.GetDataTable(new SqlDataAdapter(cmdSql));
        }
        public DataTable GetDataTable(string strSql)
        {
            return this.GetDataTable(new SqlCommand(strSql));
        }

        public int Execute(SqlCommand cmdSql)
        {
            int linhasAfetadas = new int();
            cmdSql.CommandTimeout = timeout;
            if (this.Transaction != null)
            {
                cmdSql.Connection = sqlConnection;
                cmdSql.Transaction = Transaction;
            }
            else
            {
                cmdSql.Connection = this.GetConnection();
                this.Open();
            }

            try
            {
                linhasAfetadas = cmdSql.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                this.Close();
                throw Ex;
            }

            return linhasAfetadas;
        }
        public void Execute(string strSql)
        {
            this.Execute(new SqlCommand(strSql));
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            GetConnection();
            this.Open();
            this.Transaction = sqlConnection.BeginTransaction(isolationLevel);
        }

        public void CommitAndClose()
        {
            if (sqlConnection.State == ConnectionState.Open)
                this.Transaction.Commit();
            this.Close();
        }

        public void RollbackAndClose()
        {
            if (sqlConnection.State == ConnectionState.Open)
                this.Transaction.Rollback();
            this.Close();
        }
    }
}