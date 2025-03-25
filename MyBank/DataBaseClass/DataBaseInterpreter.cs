using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace MyBank
{
    public class DataBaseInterpreter
    {

        private IDataBaseConnection<SqlConnection> _dataBaseConnection;
        private RequestsManagerClient _requestsManagerClient;


        public DataBaseInterpreter()
        {
            _dataBaseConnection = new SqlDataBaseConnection();
            _requestsManagerClient = new RequestsManagerClient();
        }


        public bool IsEmailBusy(string email) =>
            CheckOnBusy(_requestsManagerClient.CheckPhoneNumberAvailability(email));

        public bool IsPhoneNumberBusy(string number) =>
            CheckOnBusy(_requestsManagerClient.CheckPhoneNumberAvailability(number));

        public bool TryCreateClient(Client client)
        {
            var adapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var command = new SqlCommand(
                _requestsManagerClient.CreateClient(client),
                _dataBaseConnection.GetConnection()
            );

            _dataBaseConnection.OpenConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                _dataBaseConnection.CloseConnection();
                return true;
            }

            _dataBaseConnection.CloseConnection();
            return false;
        }


        private bool CheckOnBusy(string request)
        {

            var adapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var command = new SqlCommand(request, _dataBaseConnection.GetConnection());

            _dataBaseConnection.OpenConnection();

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                _dataBaseConnection.CloseConnection();
                return true;
            }
            _dataBaseConnection.CloseConnection();
            return false;
        }

        public bool IsCorrectPasswordByPhoneNumber(string password, string number)
        {
            var adapter = new SqlDataAdapter();
            var dataTable = new DataTable();

            var command = new SqlCommand(
                _requestsManagerClient.CheckCorrectPasswordByPhoneNumber(password, number), 
                _dataBaseConnection.GetConnection()
            );

            _dataBaseConnection.OpenConnection();

            adapter.SelectCommand = command;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count > 0)
            {
                _dataBaseConnection.CloseConnection();
                return true;
            }
            _dataBaseConnection.CloseConnection();
            return false;
        }
    }
}