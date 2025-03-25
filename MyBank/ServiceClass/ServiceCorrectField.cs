using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyBank
{
    public class ServiceCorrectField
    {
        public bool IsGender(string value) => !string.IsNullOrEmpty(value);

        public bool IsFirstName(string value) => IsLetter(value);

        public bool IsLastName(string value) => IsLetter(value);

        public bool IsMiddleName(string value) => IsLetter(value);

        public bool IsPassword(string value) =>
            Regex.IsMatch(value, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#!?@%$^&*-]).{8,}");

        public bool IsMail(string value) =>
            Regex.IsMatch(value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public bool IsPhoneNumber(string value) =>
            Regex.IsMatch(value, "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$");

        private bool IsLetter(string value) => Regex.IsMatch(value, "^[а-яА-Яa-zA-Z]+$");
    }
}
