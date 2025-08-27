using System;
using System.Linq;
using System.Windows.Forms;
using BarnManagement.Data;        
using BarnManagement.Models;      
using BarnManagement.Security;    

namespace BarnManagement.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            
            btnLogin.Click += BtnLogin_Click;
            btnGoRegister.Click += (s, e) =>
            {
                new RegisterForm().Show();
                this.Hide();
            };

            
            this.AcceptButton = btnLogin;
        }

        
        private void LoginForm_Load(object sender, EventArgs e) { }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUsername.Text?.Trim();
            var pass = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Kullanıcı adı ve şifre gerekli");
                return;
            }

            try
            {
                using (var db = new BarnContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Username == username);
                    if (user == null)
                    {
                        MessageBox.Show("Kullanıcı bulunamadı");
                        return;
                    }

                    bool ok = PasswordHelper.Verify(pass, user.PasswordHash, user.PasswordSalt);
                    if (!ok)
                    {
                        MessageBox.Show("Şifre hatalı");
                        return;
                    }
                }

                
                var main = new MainForm();
                main.FormClosed += (s, e2) => this.Close(); 
                main.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Giriş sırasında hata: " + ex.Message);
            }
            finally
            {
                
                txtPassword.Text = string.Empty;
            }
        }
    }
}
