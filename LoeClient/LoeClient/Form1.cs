using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LOE;

namespace LoeClient
{
    public partial class LoginForm : Form
    {
        private AuthToken authToken;

        public LoginForm()
        {
            InitializeComponent();
            InitializePasswordBox();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void InitializePasswordBox() {
            passwordBox.PasswordChar = '*';
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            authToken = await ApiClient.Login(userNameBox.Text, passwordBox.Text);
            if (!String.IsNullOrEmpty(authToken.error))
            {
                errorLabel.Text = authToken.error;
            }
            else {
                Hide();
                HomeForm homeForm = new HomeForm(authToken);
                homeForm.FormClosed += (s, args) => Close();
                homeForm.Show();
            }
        }
    }
}
