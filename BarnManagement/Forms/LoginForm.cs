using System;
using System.Linq;
using System.Windows.Forms;
using BarnManagement.Data;
using BarnManagement.Models;      // BarnContext, User
using BarnManagement.Security;    // PasswordHelper

namespace BarnManagement.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
            btnGoRegister.Click += (s, e) => { new RegisterForm().Show(); this.Hide(); };
        }

        private void LoginForm_Load(object sender, EventArgs e) { }


        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text?.Trim();
            var pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Kullanıcı adı ve şifre gerekli"); return;
            }

            using (var db = new BarnContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    MessageBox.Show("Kullanıcı bulunamadı"); return;
                }

                bool ok = PasswordHelper.Verify(pass, user.PasswordHash, user.PasswordSalt);
                if (!ok)
                {
                    MessageBox.Show("Şifre hatalı"); return;
                }

                // Başarılı giriş
                MessageBox.Show("Giriş başarılı!");

                // MainForm hazır olduğunda:
                // new MainForm(user).Show();
                // this.Hide();
            }
        }
    }
}
