using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBank
{
    public class SqlDataBaseConnection : IDataBaseConnection<SqlConnection>
    {

        private SqlConnection _connection;


        public SqlDataBaseConnection()
        {
            _connection = new SqlConnection(
                @"Data Source=RICARDODIMOS\SQLEXPRESS;
                Initial Catalog=test;
                Integrated Security=True"
            );
        }


        public void OpenConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }


        public SqlConnection GetConnection() => _connection;
    }
}
