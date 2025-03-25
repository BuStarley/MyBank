using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyBank
{
    public partial class FormLogin : Form
    {

        private ServiceCorrectField _serviceCorrectField;
        private DataBaseInterpreter _dataBaseInterpreter;
        private Hash _hash;

        public FormLogin()
        {
            InitializeComponent();
            _serviceCorrectField = new ServiceCorrectField();
            _dataBaseInterpreter = new DataBaseInterpreter();
            _hash = new Hash();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void buttonClose_Click(object sender, EventArgs e) => this.Close();

        private void FormLogin_Load(object sender, EventArgs e)
        {
            textBoxPhoneNumber.Select();
            textBoxPassword.UseSystemPasswordChar = true;

            textBoxPassword.MaxLength = 256;
            textBoxPhoneNumber.MaxLength = 20;
        }

        private void FormLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBoxShowPassword.Checked == true)
            {
                textBoxPassword.UseSystemPasswordChar = true;
                return;
            }
            textBoxPassword.UseSystemPasswordChar = false;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            labelError.ForeColor = Color.OrangeRed;
            if (IsFieldCorrect() && IsClientHas() && IsCorrectPassword())
            {
                labelError.ForeColor = Color.Green;
                labelError.Text = "Вход произведен успешно";
            }
        }

        private bool IsCorrectPassword()
        {
            if (_dataBaseInterpreter.IsCorrectPasswordByPhoneNumber(_hash.GetHash(textBoxPassword.Text), textBoxPhoneNumber.Text))
            {
                return true;
            }
            labelError.Text = "Неверный пароль";
            return false;
        }

        private bool IsClientHas()
        {
            if (!_dataBaseInterpreter.IsPhoneNumberBusy(textBoxPhoneNumber.Text))
            {
                labelError.Text = "Аккаунта с таким номера телефона нет";
                return false;
            }
            return true;
        }

        private bool IsFieldCorrect()
        {
            if (string.IsNullOrEmpty(textBoxPassword.Text) || string.IsNullOrEmpty(textBoxPhoneNumber.Text))
            {
                labelError.Text = "Заполните пожалуйста все поля";
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

        private void linkLabelRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Hide();
            new FormRegister().ShowDialog();
        }
    }
}
