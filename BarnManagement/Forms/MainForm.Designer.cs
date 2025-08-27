namespace BarnManagement.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dgvAnimals = new System.Windows.Forms.DataGridView();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.GroupBox = new System.Windows.Forms.GroupBox();
            this.btnAddAnimal = new System.Windows.Forms.Button();
            this.nudAge = new System.Windows.Forms.NumericUpDown();
            this.cboGender = new System.Windows.Forms.ComboBox();
            this.cboSpecies = new System.Windows.Forms.ComboBox();
            this.Uretim = new System.Windows.Forms.GroupBox();
            this.btnProduce = new System.Windows.Forms.Button();
            this.prgProduction = new System.Windows.Forms.ProgressBar();
            this.timerProduction = new System.Windows.Forms.Timer(this.components);
            this.Satis = new System.Windows.Forms.GroupBox();
            this.lblBalance = new System.Windows.Forms.Label();
            this.btnSell = new System.Windows.Forms.Button();
            this.nudPrice = new System.Windows.Forms.NumericUpDown();
            this.Hayvan = new System.Windows.Forms.GroupBox();
            this.btnSellAnimal = new System.Windows.Forms.Button();
            this.cboSellAnimal = new System.Windows.Forms.ComboBox();
            this.nudAnimalPrice = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAge)).BeginInit();
            this.Uretim.SuspendLayout();
            this.Satis.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).BeginInit();
            this.Hayvan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAnimalPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAnimals
            // 
            this.dgvAnimals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAnimals.Location = new System.Drawing.Point(59, 28);
            this.dgvAnimals.Name = "dgvAnimals";
            this.dgvAnimals.Size = new System.Drawing.Size(238, 103);
            this.dgvAnimals.TabIndex = 0;
            this.dgvAnimals.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellContentClick);
            // 
            // dgvProducts
            // 
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(408, 28);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.Size = new System.Drawing.Size(258, 103);
            this.dgvProducts.TabIndex = 1;
            this.dgvProducts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvProducts_CellContentClick);
            // 
            // GroupBox
            // 
            this.GroupBox.Controls.Add(this.btnAddAnimal);
            this.GroupBox.Controls.Add(this.nudAge);
            this.GroupBox.Controls.Add(this.cboGender);
            this.GroupBox.Controls.Add(this.cboSpecies);
            this.GroupBox.Location = new System.Drawing.Point(59, 159);
            this.GroupBox.Name = "GroupBox";
            this.GroupBox.Size = new System.Drawing.Size(238, 131);
            this.GroupBox.TabIndex = 2;
            this.GroupBox.TabStop = false;
            // 
            // btnAddAnimal
            // 
            this.btnAddAnimal.Location = new System.Drawing.Point(26, 99);
            this.btnAddAnimal.Name = "btnAddAnimal";
            this.btnAddAnimal.Size = new System.Drawing.Size(75, 23);
            this.btnAddAnimal.TabIndex = 3;
            this.btnAddAnimal.Text = "Ekle";
            this.btnAddAnimal.UseVisualStyleBackColor = true;
            // 
            // nudAge
            // 
            this.nudAge.Location = new System.Drawing.Point(6, 73);
            this.nudAge.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudAge.Name = "nudAge";
            this.nudAge.Size = new System.Drawing.Size(120, 20);
            this.nudAge.TabIndex = 2;
            // 
            // cboGender
            // 
            this.cboGender.FormattingEnabled = true;
            this.cboGender.Location = new System.Drawing.Point(6, 46);
            this.cboGender.Name = "cboGender";
            this.cboGender.Size = new System.Drawing.Size(121, 21);
            this.cboGender.TabIndex = 1;
            // 
            // cboSpecies
            // 
            this.cboSpecies.FormattingEnabled = true;
            this.cboSpecies.Location = new System.Drawing.Point(6, 19);
            this.cboSpecies.Name = "cboSpecies";
            this.cboSpecies.Size = new System.Drawing.Size(121, 21);
            this.cboSpecies.TabIndex = 0;
            // 
            // Uretim
            // 
            this.Uretim.Controls.Add(this.btnProduce);
            this.Uretim.Controls.Add(this.prgProduction);
            this.Uretim.Location = new System.Drawing.Point(59, 305);
            this.Uretim.Name = "Uretim";
            this.Uretim.Size = new System.Drawing.Size(238, 133);
            this.Uretim.TabIndex = 3;
            this.Uretim.TabStop = false;
            // 
            // btnProduce
            // 
            this.btnProduce.Location = new System.Drawing.Point(6, 19);
            this.btnProduce.Name = "btnProduce";
            this.btnProduce.Size = new System.Drawing.Size(122, 23);
            this.btnProduce.TabIndex = 1;
            this.btnProduce.Text = "Üretimi Başlat";
            this.btnProduce.UseVisualStyleBackColor = true;
            // 
            // prgProduction
            // 
            this.prgProduction.Location = new System.Drawing.Point(0, 95);
            this.prgProduction.Name = "prgProduction";
            this.prgProduction.Size = new System.Drawing.Size(238, 23);
            this.prgProduction.TabIndex = 0;
            // 
            // timerProduction
            // 
            this.timerProduction.Interval = 120;
            // 
            // Satis
            // 
            this.Satis.Controls.Add(this.lblBalance);
            this.Satis.Controls.Add(this.btnSell);
            this.Satis.Controls.Add(this.nudPrice);
            this.Satis.Location = new System.Drawing.Point(409, 159);
            this.Satis.Name = "Satis";
            this.Satis.Size = new System.Drawing.Size(257, 131);
            this.Satis.TabIndex = 4;
            this.Satis.TabStop = false;
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(144, 99);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(66, 13);
            this.lblBalance.TabIndex = 2;
            this.lblBalance.Text = "Bakiye: 0,00";
            // 
            // btnSell
            // 
            this.btnSell.Location = new System.Drawing.Point(6, 45);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(75, 23);
            this.btnSell.TabIndex = 1;
            this.btnSell.Text = "Seçileni Sat";
            this.btnSell.UseVisualStyleBackColor = true;
            // 
            // nudPrice
            // 
            this.nudPrice.DecimalPlaces = 2;
            this.nudPrice.Location = new System.Drawing.Point(6, 19);
            this.nudPrice.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudPrice.Name = "nudPrice";
            this.nudPrice.Size = new System.Drawing.Size(120, 20);
            this.nudPrice.TabIndex = 0;
            // 
            // Hayvan
            // 
            this.Hayvan.Controls.Add(this.btnSellAnimal);
            this.Hayvan.Controls.Add(this.cboSellAnimal);
            this.Hayvan.Controls.Add(this.nudAnimalPrice);
            this.Hayvan.Location = new System.Drawing.Point(409, 305);
            this.Hayvan.Name = "Hayvan";
            this.Hayvan.Size = new System.Drawing.Size(257, 128);
            this.Hayvan.TabIndex = 5;
            this.Hayvan.TabStop = false;
            // 
            // btnSellAnimal
            // 
            this.btnSellAnimal.Location = new System.Drawing.Point(6, 72);
            this.btnSellAnimal.Name = "btnSellAnimal";
            this.btnSellAnimal.Size = new System.Drawing.Size(75, 23);
            this.btnSellAnimal.TabIndex = 2;
            this.btnSellAnimal.Text = "Hayvanı Sat";
            this.btnSellAnimal.UseVisualStyleBackColor = true;
            // 
            // cboSellAnimal
            // 
            this.cboSellAnimal.FormattingEnabled = true;
            this.cboSellAnimal.Location = new System.Drawing.Point(6, 19);
            this.cboSellAnimal.Name = "cboSellAnimal";
            this.cboSellAnimal.Size = new System.Drawing.Size(121, 21);
            this.cboSellAnimal.TabIndex = 1;
            // 
            // nudAnimalPrice
            // 
            this.nudAnimalPrice.DecimalPlaces = 2;
            this.nudAnimalPrice.Location = new System.Drawing.Point(6, 46);
            this.nudAnimalPrice.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudAnimalPrice.Name = "nudAnimalPrice";
            this.nudAnimalPrice.Size = new System.Drawing.Size(120, 20);
            this.nudAnimalPrice.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Hayvan);
            this.Controls.Add(this.Satis);
            this.Controls.Add(this.Uretim);
            this.Controls.Add(this.GroupBox);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.dgvAnimals);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAnimals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.GroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAge)).EndInit();
            this.Uretim.ResumeLayout(false);
            this.Satis.ResumeLayout(false);
            this.Satis.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPrice)).EndInit();
            this.Hayvan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudAnimalPrice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAnimals;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.GroupBox GroupBox;
        private System.Windows.Forms.Button btnAddAnimal;
        private System.Windows.Forms.NumericUpDown nudAge;
        private System.Windows.Forms.ComboBox cboGender;
        private System.Windows.Forms.ComboBox cboSpecies;
        private System.Windows.Forms.GroupBox Uretim;
        private System.Windows.Forms.Button btnProduce;
        private System.Windows.Forms.ProgressBar prgProduction;
        private System.Windows.Forms.Timer timerProduction;
        private System.Windows.Forms.GroupBox Satis;
        private System.Windows.Forms.NumericUpDown nudPrice;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.GroupBox Hayvan;
        private System.Windows.Forms.ComboBox cboSellAnimal;
        private System.Windows.Forms.NumericUpDown nudAnimalPrice;
        private System.Windows.Forms.Button btnSellAnimal;
    }
}