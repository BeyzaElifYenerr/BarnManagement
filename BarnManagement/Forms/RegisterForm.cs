using System;
using System.Linq;
using System.Windows.Forms;
using BarnManagement.Data;
using BarnManagement.Models;      
using BarnManagement.Security;    

namespace BarnManagement.Forms
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            btnRegister.Click += BtnRegister_Click;
            btnGoLogin.Click += (s, e) => { new LoginForm().Show(); this.Hide(); };
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }


        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text?.Trim();
            var pass = txtPassword.Text;
            var pass2 = txtPasswordConfirm.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Kullanıcı adı gerekli"); return;
            }
            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Şifre gerekli"); return;
            }
            if (pass != pass2)
            {
                MessageBox.Show("Şifreler uyuşmuyor"); return;
            }

            using (var db = new BarnContext())
            {
                
                if (db.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Bu kullanıcı adı zaten mevcut"); return;
                }

                PasswordHelper.CreatePasswordHash(pass, out string hash, out string salt);

                var user = new User
                {
                    Username = username,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };

                db.Users.Add(user);
                db.SaveChanges();

                MessageBox.Show("Kayıt başarılı! Şimdi giriş yapabilirsiniz.");
                new LoginForm().Show();
                this.Hide();
            }
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
