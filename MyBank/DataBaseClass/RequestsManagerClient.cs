using System;

namespace MyBank
{
    public class RequestsManagerClient
    {
        public string CheckMailAvailability(string mail) =>
            $"SELECT client_email FROM clients WHERE client_email = '{mail}'";

        public string CheckPhoneNumberAvailability(string number) =>
            $"SELECT client_phone_number FROM clients WHERE client_phone_number = '{number}'";

        public string CreateClient(Client client) =>
            $"INSERT INTO clients (" +
            $"client_last_name, " +
            $"client_first_name, " +
            $"client_middle_name, " +
            $"client_gender, " +
            $"client_password, " +
            $"client_email, " +
            $"client_phone_number) VALUES (" +
            $"'{client.LastName}', " +
            $"'{client.FirstName}', " +
            $"'{client.MiddleName}', " +
            $"'{client.Gender}', " +
            $"'{client.Password}', " +
            $"'{client.Email}', " +
            $"'{client.PhoneNumber}')";

        public string GetPasswordByPhoneNumber(string number) =>
            $"SELECT client_Id FROM clients WHERE client_phone_number = '{number}'";

        public string CheckCorrectPasswordByPhoneNumber(string password, string number) =>
            $"SELECT client_Id FROM clients WHERE client_phone_number = '{number}' AND client_password = '{password}'";
    }
}