using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyBank
{
    public class Hash
    {

        public static readonly Hash Instance = new Hash();


        public string GetHash(string value)
        {
            MD5 md5 = MD5.Create();

            byte[] bytes = Encoding.ASCII.GetBytes(value);
            byte[] hash = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (var a in hash)
            {
                sb.Append(a.ToString("X2"));
            }
            return sb.ToString();

        }

    }
}
