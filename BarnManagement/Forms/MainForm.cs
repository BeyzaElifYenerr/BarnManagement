using System;
using System.Linq;
using System.Windows.Forms;
using BarnManagement.Data;
using BarnManagement.Models;
using BarnManagement.Auth; 

namespace BarnManagement.Forms
{
    public partial class MainForm : Form
    {
        private const decimal START_BALANCE = 1000m;

        
        private readonly int _progressStep = 20;

        public MainForm()
        {
            InitializeComponent();

            
            this.Load += MainForm_Load;
            btnAddAnimal.Click += BtnAddAnimal_Click;
            btnProduce.Click += BtnProduce_Click;
            timerProduction.Tick += TimerProduction_Tick;
            btnSell.Click += BtnSell_Click;
            btnSellAnimal.Click += BtnSellAnimal_Click;

            
            timerProduction.Interval = 1000; 
        }

      
        private decimal GetUnitPrice(Enums.ProductType type)
        {
            switch (type)
            {
                case Enums.ProductType.Milk: return 10m; 
                case Enums.ProductType.Egg: return 5m;  
                case Enums.ProductType.Wool: return 60m; 
                default: return 0m;
            }
        }

        
        private decimal GetBuyPrice(Enums.Species species)
        {
            switch (species)
            {
                case Enums.Species.Cow: return 500m;
                case Enums.Species.Chicken: return 50m;
                case Enums.Species.Sheep: return 300m;
                default: return 0m;
            }
        }

        
        private decimal GetAnimalSellPrice(Animal a)
        {
            int maxLife =
                (a is Cow) ? Cow.StaticLifetimeDays :
                (a is Chicken) ? Chicken.StaticLifetimeDays :
                (a is Sheep) ? Sheep.StaticLifetimeDays :
                Animal.DefaultLifetimeDays;

            var buy = GetBuyPrice(a.Species);
            var ratio = Math.Min((decimal)a.AgeDays / Math.Max(1, maxLife), 1m);
            var factor = 0.4m + ratio * 0.4m; // 0.4 .. 0.8
            return Math.Round(buy * factor, 2);
        }

        
        private int EnsureAndGetBarnId(BarnContext db)
        {
            if (!AuthContext.IsAuthenticated)
            {
                MessageBox.Show("Oturum bulunamadı. Lütfen tekrar giriş yapın.");
                Application.Exit();
                return 0;
            }

            int uid = AuthContext.CurrentUserId.Value;

            var barn = db.Barns.FirstOrDefault(b => b.OwnerUserId == uid);
            if (barn == null)
            {
                barn = new Barn
                {
                    OwnerUserId = uid,
                    Capacity = 50,
                    CurrentAnimalCount = 0,
                    Balance = START_BALANCE
                };
                db.Barns.Add(barn);
                db.SaveChanges();
            }
            return barn.Id;
        }

      

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            cboSpecies.DataSource = Enum.GetValues(typeof(Enums.Species));
            cboGender.DataSource = Enum.GetValues(typeof(Enums.Gender));

            nudAge.Value = 0;
            prgProduction.Value = 0;

            EnsureBarnExists();
            LoadAnimals();
            LoadAnimalsForSale();
            LoadProducts();
            LoadBalance();

            
            dgvProducts.AutoGenerateColumns = true;
            dgvAnimals.AutoGenerateColumns = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvAnimals.MultiSelect = false;
        }

        
        private void EnsureBarnExists()
        {
            using (var db = new BarnContext())
            {
                EnsureAndGetBarnId(db); // yeterli
            }
        }

       

        private void LoadAnimals()
        {
            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);

                var data = db.Animals
                    .Where(a => a.IsAlive && a.BarnId == barnId)
                    .Select(a => new
                    {
                        a.Id,
                        Species = a.Species.ToString(),
                        Gender = a.Gender.ToString(),
                        a.AgeDays,
                        a.IsAlive
                    })
                    .ToList();

                dgvAnimals.DataSource = data;

                var barn = db.Barns.First(b => b.Id == barnId);
                barn.CurrentAnimalCount = db.Animals.Count(x => x.IsAlive && x.BarnId == barnId);
                db.SaveChanges();
            }
        }

        private void LoadAnimalsForSale()
        {
            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);

                var list = db.Animals
                             .Where(a => a.IsAlive && a.BarnId == barnId)
                             .Select(a => new { a.Id, Text = a.Id + " - " + a.Species.ToString() })
                             .ToList();

                cboSellAnimal.DisplayMember = "Text";
                cboSellAnimal.ValueMember = "Id";
                cboSellAnimal.DataSource = list;
            }
        }

        private void LoadProducts()
        {
            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);

               
                var data = db.Products
                    .Where(p => p.BarnId == barnId)
                    .OrderByDescending(p => p.Id)
                    .ToList()
                    .Select(p => new
                    {
                        p.Id,
                        Type = p.ProductType.ToString(),
                        p.Quantity,
                        p.IsSold,
                        UnitPrice = GetUnitPrice(p.ProductType)
                    })
                    .ToList();

                dgvProducts.DataSource = data;
            }
        }

        private void LoadBalance()
        {
            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);
                var bal = db.Barns.Where(b => b.Id == barnId).Select(b => b.Balance).FirstOrDefault();
                lblBalance.Text = $"Bakiye: {bal:0.00}";
            }
        }

        

        private void BtnAddAnimal_Click(object sender, EventArgs e)
        {
            var species = (Enums.Species)cboSpecies.SelectedItem;
            var gender = (Enums.Gender)cboGender.SelectedItem;
            int age = (int)nudAge.Value;

            Animal a;
            switch (species)
            {
                case Enums.Species.Cow: a = new Cow { Gender = gender, AgeDays = age }; break;
                case Enums.Species.Chicken: a = new Chicken { Gender = gender, AgeDays = age }; break;
                case Enums.Species.Sheep: a = new Sheep { Gender = gender, AgeDays = age }; break;
                default: MessageBox.Show("Tür seçimi hatalı"); return;
            }

            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);
                var barn = db.Barns.First(b => b.Id == barnId);

                
                var cost = GetBuyPrice(species);
                if (barn.Balance < cost)
                {
                    MessageBox.Show($"Yetersiz bakiye. Gerekli: {cost:0.00}");
                    return;
                }

                
                if (barn.CurrentAnimalCount >= barn.Capacity)
                {
                    MessageBox.Show("Ahır kapasitesi dolu.");
                    return;
                }

                
                a.BarnId = barnId;

                barn.Balance -= cost;
                db.Animals.Add(a);
                db.SaveChanges();
            }

            LoadAnimals();
            LoadAnimalsForSale();
            LoadBalance();

            MessageBox.Show("Hayvan eklendi (bakiye güncellendi).");
        }

       

        private void BtnProduce_Click(object sender, EventArgs e)
        {
            if (timerProduction.Enabled) return;
            prgProduction.Value = 0;
            btnProduce.Enabled = false;
            timerProduction.Start();
        }

        private void TimerProduction_Tick(object sender, EventArgs e)
        {
            prgProduction.Value = Math.Min(100, prgProduction.Value + _progressStep);

            if (prgProduction.Value >= 100)
            {
                timerProduction.Stop();
                ProduceAll();            
                btnProduce.Enabled = true;
                prgProduction.Value = 0;
            }
        }

        private void ProduceAll()
        {
            int created = 0;

            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);

                var animals = db.Animals
                                .Where(a => a.IsAlive && a.BarnId == barnId)
                                .ToList();

                foreach (var a in animals)
                {
                    
                    int life = (a is Cow) ? Cow.StaticLifetimeDays :
                               (a is Chicken) ? Chicken.StaticLifetimeDays :
                               (a is Sheep) ? Sheep.StaticLifetimeDays :
                               Animal.DefaultLifetimeDays;

                    if (a.AgeDays > life)
                    {
                        a.IsAlive = false;
                        continue;
                    }

                    
                    var product = a.ProduceProduct();
                    if (product != null)
                    {
                        product.BarnId = barnId; 
                        db.Products.Add(product);
                        created++;
                    }

                    
                    a.AgeDays += 1;
                }

                db.SaveChanges();
            }

            LoadAnimals();
            LoadAnimalsForSale();
            LoadProducts();
            
        }

        

        private void BtnSell_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Lütfen satılacak ürünü seçin.");
                return;
            }

            int productId = Convert.ToInt32(dgvProducts.CurrentRow.Cells["Id"].Value);

            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);

                var p = db.Products.FirstOrDefault(x => x.Id == productId && x.BarnId == barnId);
                if (p == null) { MessageBox.Show("Ürün bulunamadı veya yetkiniz yok."); return; }
                if (p.IsSold) { MessageBox.Show("Bu ürün zaten satılmış."); return; }

                
                var unitPrice = GetUnitPrice(p.ProductType);
                if (unitPrice <= 0)
                {
                    MessageBox.Show("Ürün tipi için fiyat bulunamadı.");
                    return;
                }

                var barn = db.Barns.First(b => b.Id == barnId);
                barn.Balance += unitPrice * p.Quantity;
                p.IsSold = true;

                db.SaveChanges();
            }

            LoadProducts();
            LoadBalance();
            MessageBox.Show("Ürün satışı gerçekleştirildi (sabit fiyattan).");
        }

      

        private void BtnSellAnimal_Click(object sender, EventArgs e)
        {
            if (cboSellAnimal.SelectedValue == null)
            {
                MessageBox.Show("Lütfen satılacak hayvanı seçin.");
                return;
            }

            int animalId = Convert.ToInt32(cboSellAnimal.SelectedValue);

            using (var db = new BarnContext())
            {
                int barnId = EnsureAndGetBarnId(db);

                var a = db.Animals.FirstOrDefault(x => x.Id == animalId && x.IsAlive && x.BarnId == barnId);
                if (a == null)
                {
                    MessageBox.Show("Hayvan bulunamadı veya yetkiniz yok.");
                    return;
                }

                
                var price = GetAnimalSellPrice(a);

                var barn = db.Barns.First(b => b.Id == barnId);
                barn.Balance += price;
                a.IsAlive = false;

                db.SaveChanges();
            }

            LoadAnimals();
            LoadAnimalsForSale();
            LoadBalance();
            MessageBox.Show("Hayvan satışı gerçekleştirildi (sabit fiyattan).");
        }

       

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView grid && e.RowIndex >= 0)
                grid.Rows[e.RowIndex].Selected = true;
        }

        private void DgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                dgvProducts.Rows[e.RowIndex].Selected = true;
        }

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void MainForm_Load_1(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) => DataGridView1_CellContentClick(sender, e);
        private void dgyProducts_CellContentClick(object sender, DataGridViewCellEventArgs e) => DgvProducts_CellContentClick(sender, e);
    }
}
