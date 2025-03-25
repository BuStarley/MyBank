using System;
using System.Security.Cryptography;
using System.Text;

namespace MyBank
{
    public class Password
    {

        private string value;

        public Password(string value)
        {
            this.value = Hash.Instance.GetHash(value);
        }

        public override string ToString() => value;

    }
}