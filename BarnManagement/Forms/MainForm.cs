using System;
using System.Linq;
using System.Windows.Forms;
using BarnManagement.Data;    // BarnContext bu namespacete ise bırak; değilse kaldır
using BarnManagement.Models;  // Modeller

namespace BarnManagement.Forms
{
    public partial class MainForm : Form
    {
        private const decimal START_BALANCE = 1000m; // başlangıç bakiyesi
        private readonly int _progressStep = 5;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
            => DataGridView1_CellContentClick(sender, e);

        private void dgyProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
            => DgvProducts_CellContentClick(sender, e);

        private void lbBalance_Click(object sender, EventArgs e) { /* no-op */ }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { /* no-op */ }


        public MainForm()
        {
            InitializeComponent();

            // Event bağları
            this.Load += MainForm_Load;
            btnAddAnimal.Click += BtnAddAnimal_Click;
            btnProduce.Click += BtnProduce_Click;
            timerProduction.Tick += TimerProduction_Tick;
            btnSell.Click += BtnSell_Click;               // ÜRÜN satışı
            btnSellAnimal.Click += BtnSellAnimal_Click;   // HAYVAN satışı (yeni)
        }

        // ---------- FORM LOAD ----------
        private void MainForm_Load(object sender, EventArgs e)
        {
            // Enums sınıfını kullandığın için:
            cboSpecies.DataSource = Enum.GetValues(typeof(Enums.Species));
            cboGender.DataSource = Enum.GetValues(typeof(Enums.Gender));

            nudAge.Value = 0;
            prgProduction.Value = 0;

            EnsureBarnExists();
            LoadAnimals();
            LoadAnimalsForSale();   // yeni: satış kombosu
            LoadProducts();
            LoadBalance();

            // Grid ayarları
            dgvProducts.AutoGenerateColumns = true;
            dgvAnimals.AutoGenerateColumns = true;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAnimals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvAnimals.MultiSelect = false;
        }

        // ---------- DB HELPERS ----------
        private void EnsureBarnExists()
        {
            using (var db = new BarnContext())
            {
                if (!db.Barns.Any())
                {
                    db.Barns.Add(new Barn { Capacity = 50, CurrentAnimalCount = 0, Balance = START_BALANCE });
                    db.SaveChanges();
                }
            }
        }

        private void LoadAnimals()
        {
            using (var db = new BarnContext())
            {
                var data = db.Animals
                    .Where(a => a.IsAlive) // sadece canlı/satılmamış hayvanlar listelensin
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

                // Ahır canlı hayvan sayısını güncelle
                var barn = db.Barns.FirstOrDefault();
                if (barn != null)
                {
                    barn.CurrentAnimalCount = db.Animals.Count(x => x.IsAlive);
                    db.SaveChanges();
                }
            }
        }

        private void LoadAnimalsForSale()
        {
            using (var db = new BarnContext())
            {
                var list = db.Animals
                             .Where(a => a.IsAlive)
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
                var data = db.Products
                    .OrderByDescending(p => p.Id)
                    .Select(p => new
                    {
                        p.Id,
                        Type = p.ProductType.ToString(),
                        p.Quantity,
                        p.IsSold
                    })
                    .ToList();

                dgvProducts.DataSource = data;
            }
        }

        private void LoadBalance()
        {
            using (var db = new BarnContext())
            {
                var bal = db.Barns.Select(b => b.Balance).FirstOrDefault();
                lblBalance.Text = $"Bakiye: {bal:0.00}";
            }
        }

        // ---------- FİYAT YARDIMCILARI ----------
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

        // ---------- HAYVAN EKLE (ALIŞ) ----------
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
                var barn = db.Barns.First();

                // Bakiye kontrolü
                var cost = GetBuyPrice(species);
                if (barn.Balance < cost)
                {
                    MessageBox.Show($"Yetersiz bakiye. Gerekli: {cost:0.00}");
                    return;
                }

                // Kapasite kontrolü (opsiyonel)
                if (barn.CurrentAnimalCount >= barn.Capacity)
                {
                    MessageBox.Show("Ahır kapasitesi dolu.");
                    return;
                }

                barn.Balance -= cost;   // alış maliyetini düş
                db.Animals.Add(a);
                db.SaveChanges();
            }

            LoadAnimals();
            LoadAnimalsForSale();
            LoadBalance();

            MessageBox.Show("Hayvan eklendi (bakiye güncellendi).");
        }

        // ---------- ÜRETİM ----------
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
                var animals = db.Animals.Where(a => a.IsAlive).ToList();

                foreach (var a in animals)
                {
                    int life = (a is Cow) ? Cow.StaticLifetimeDays :
                               (a is Chicken) ? Chicken.StaticLifetimeDays :
                               (a is Sheep) ? Sheep.StaticLifetimeDays : 3650;

                    if (a.AgeDays > life) { a.IsAlive = false; continue; }

                    var product = a.ProduceProduct();
                    if (product != null) { db.Products.Add(product); created++; }

                    a.AgeDays += 1;
                }

                db.SaveChanges();
            }

            LoadAnimals();
            LoadAnimalsForSale();
            LoadProducts();
            // MessageBox.Show($"Üretilen ürün adedi: {created}");
        }

        // ---------- ÜRÜN SATIŞI ----------
        private void BtnSell_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null)
            {
                MessageBox.Show("Lütfen satılacak ürünü seçin.");
                return;
            }

            int productId = Convert.ToInt32(dgvProducts.CurrentRow.Cells["Id"].Value);
            decimal unitPrice = nudPrice.Value;
            if (unitPrice <= 0)
            {
                MessageBox.Show("Birim fiyat girin.");
                return;
            }

            using (var db = new BarnContext())
            {
                var p = db.Products.FirstOrDefault(x => x.Id == productId);
                if (p == null) { MessageBox.Show("Ürün bulunamadı."); return; }
                if (p.IsSold) { MessageBox.Show("Bu ürün zaten satılmış."); return; }

                var barn = db.Barns.First();
                barn.Balance += unitPrice * p.Quantity; // ürün geliri
                p.IsSold = true;

                db.SaveChanges();
            }

            LoadProducts();
            LoadBalance();
            MessageBox.Show("Ürün satışı gerçekleştirildi.");
        }

        // ---------- HAYVAN SATIŞI (YENİ) ----------
        private void BtnSellAnimal_Click(object sender, EventArgs e)
        {
            if (cboSellAnimal.SelectedValue == null)
            {
                MessageBox.Show("Lütfen satılacak hayvanı seçin.");
                return;
            }

            int animalId = Convert.ToInt32(cboSellAnimal.SelectedValue);
            decimal price = nudAnimalPrice.Value;
            if (price <= 0)
            {
                MessageBox.Show("Satış fiyatı girin.");
                return;
            }

            using (var db = new BarnContext())
            {
                var a = db.Animals.FirstOrDefault(x => x.Id == animalId && x.IsAlive);
                if (a == null) { MessageBox.Show("Hayvan bulunamadı veya zaten satılmış/ölü."); return; }

                var barn = db.Barns.First();
                barn.Balance += price;   // hayvan satışı geliri
                a.IsAlive = false;       // satıldı/çıktı → artık listelenmesin

                db.SaveChanges();
            }

            LoadAnimals();
            LoadAnimalsForSale();
            LoadBalance();
            MessageBox.Show("Hayvan satışı gerçekleştirildi.");
        }

        // DataGridView1 – CellContentClick
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView grid && e.RowIndex >= 0)
                grid.Rows[e.RowIndex].Selected = true;
        }

        // DgvProducts – CellContentClick
        private void DgvProducts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                dgvProducts.Rows[e.RowIndex].Selected = true;
        }

    }
}
