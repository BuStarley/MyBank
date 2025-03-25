namespace MyBank
{
    public class Client
    {

        public Client(string firstName,string lastName, string middleName, string gender, Password password, string email, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            Gender = gender.Substring(0, 3);
            Password = password.ToString();
            Email = email;
            PhoneNumber = phoneNumber;
        }


        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public string Gender { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}