using System;
using System.Linq;
using System.Windows.Forms;
using BarnManagement.Data;
using BarnManagement.Models;
using BarnManagement.Security;
using BarnManagement.Auth;   

namespace BarnManagement.Forms
{
    public partial class LoginForm : Form
    {
        private const decimal START_BALANCE = 1000m; 

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
            var pass = txtPassword.Text ?? string.Empty;

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

                    
                    AuthContext.SignIn(user.Id);

                    
                    var userBarn = db.Barns.FirstOrDefault(b => b.OwnerUserId == user.Id);
                    if (userBarn == null)
                    {
                        db.Barns.Add(new Barn
                        {
                            OwnerUserId = user.Id,     
                            Capacity = 50,
                            CurrentAnimalCount = 0,
                            Balance = START_BALANCE
                        });
                        db.SaveChanges();
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
