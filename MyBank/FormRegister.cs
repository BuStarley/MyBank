using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBank
{
    public partial class FormRegister : Form
    {

        private ServiceCorrectField _serviceCorrectField;
        private DataBaseInterpreter _dataBaseInterpreter;

        public FormRegister()
        {
            InitializeComponent();
            _serviceCorrectField = new ServiceCorrectField();
            _dataBaseInterpreter = new DataBaseInterpreter();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
            new FormLogin().Show();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            textBoxFirstName.Select();
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxPasswordRetry.UseSystemPasswordChar = true;
        }


        private void FormRegister_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            if (IsFieldCorrectly() && IsClientNone())
            {
                if (_dataBaseInterpreter.TryCreateClient(new Client(
                    textBoxFirstName.Text,
                    textBoxLastName.Text,
                    textBoxMiddleName.Text,
                    comboBoxGender.Text,
                    new Password(textBoxPassword.Text),
                    textBoxEmail.Text,
                    textBoxPhoneNumber.Text
                    )))
                {
                    Close();
                    return;
                }
                labelError.Text = "Ошибка создания аккаунта!";
            }
        }

        private bool IsFieldCorrectly()
        {

            var lastName = textBoxLastName.Text;
            var firstName = textBoxFirstName.Text;
            var middleName = textBoxMiddleName.Text;
            var gender = comboBoxGender.Text;
            var password = textBoxPassword.Text;
            var email = textBoxEmail.Text;
            var phoneNumber = textBoxPhoneNumber.Text;


            foreach (var control in Controls)
            {
                var textBox = control as TextBox;
                if (textBox != null && textBox.Enabled)
                {
                    if (string.IsNullOrEmpty(textBox.Text))
                    {
                        labelError.Text = "Заполните пожалуйста все поля";
                        return false;
                    }
                }
            }

            if (!_serviceCorrectField.IsGender(gender))
            {
                labelError.Text = "Обязательно выберите пол";
                return false;
            }
            else if (!_serviceCorrectField.IsFirstName(firstName) || !_serviceCorrectField.IsLastName(lastName))
            {
                labelError.Text = "Имя и фамилия может состоять только из букв";
                return false;
            }
            else if (textBoxMiddleName.Enabled && !_serviceCorrectField.IsMiddleName(middleName))
            {
                labelError.Text = "Отчество может состоять только из букв";
                return false;
            }
            else if (!_serviceCorrectField.IsPassword(password))
            {
                labelError.Text = "Пароль слишком простой";
                return false;
            }
            else if (textBoxPassword.Text != textBoxPasswordRetry.Text)
            {
                labelError.Text = "Пароли не совпадают";
                return false;
            }
            else if (!_serviceCorrectField.IsMail(textBoxEmail.Text))
            {
                labelError.Text = "Некоректный тип электронной почты";
                return false;
            }
            else if (!_serviceCorrectField.IsPhoneNumber(textBoxPhoneNumber.Text))
            {
                labelError.Text = "Некоректный номер телефона";
                return false;
            }

            labelError.Text = string.Empty;
            return true;
        }

        private bool IsClientNone()
        {

            var phoneNumber = textBoxPhoneNumber.Text;
            var email = textBoxEmail.Text;

            if (_dataBaseInterpreter.IsPhoneNumberBusy(phoneNumber))
            {
                labelError.Text = "Аккаунт с таким номером телефона уже создан";
                return false;
            }
            else if (_dataBaseInterpreter.IsEmailBusy(email)) 
            {
                labelError.Text = "Аккаунт с такой электронной почтой уже создан";
                return false;
            }

            labelError.Text = string.Empty;
            return true;
        }

        private void checkBoxHasMiddleName_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxHasMiddleName.Checked == true)
            {
                textBoxMiddleName.Enabled = false;
                return;
            }
            textBoxMiddleName.Enabled = true;
            return;
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked == true)
            {
                textBoxPassword.UseSystemPasswordChar = false;
                textBoxPasswordRetry.UseSystemPasswordChar = false;
                return;
            }
            textBoxPassword.UseSystemPasswordChar = true;
            textBoxPasswordRetry.UseSystemPasswordChar = true;
        }

    }
}
