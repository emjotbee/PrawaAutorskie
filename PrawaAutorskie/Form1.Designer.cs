
namespace PrawaAutorskie
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Lista = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Narzedzia = new System.Windows.Forms.GroupBox();
            this.Raport = new System.Windows.Forms.Button();
            this.Filtry = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Wyczysc = new System.Windows.Forms.Button();
            this.Zastosuj = new System.Windows.Forms.Button();
            this.Pobierz = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.DodajDzielo = new System.Windows.Forms.Button();
            this.Szczegoly = new System.Windows.Forms.GroupBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.Lista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Narzedzia.SuspendLayout();
            this.Filtry.SuspendLayout();
            this.Szczegoly.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lista
            // 
            this.Lista.Controls.Add(this.dataGridView1);
            this.Lista.Location = new System.Drawing.Point(12, 27);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(686, 411);
            this.Lista.TabIndex = 0;
            this.Lista.TabStop = false;
            this.Lista.Text = "Lista";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(674, 383);
            this.dataGridView1.TabIndex = 0;
            // 
            // Narzedzia
            // 
            this.Narzedzia.Controls.Add(this.Raport);
            this.Narzedzia.Controls.Add(this.Filtry);
            this.Narzedzia.Controls.Add(this.Pobierz);
            this.Narzedzia.Controls.Add(this.button2);
            this.Narzedzia.Controls.Add(this.DodajDzielo);
            this.Narzedzia.Location = new System.Drawing.Point(704, 170);
            this.Narzedzia.Name = "Narzedzia";
            this.Narzedzia.Size = new System.Drawing.Size(303, 268);
            this.Narzedzia.TabIndex = 2;
            this.Narzedzia.TabStop = false;
            this.Narzedzia.Text = "Narzędzia";
            // 
            // Raport
            // 
            this.Raport.Location = new System.Drawing.Point(7, 90);
            this.Raport.Name = "Raport";
            this.Raport.Size = new System.Drawing.Size(289, 27);
            this.Raport.TabIndex = 9;
            this.Raport.Text = "Generuj raport dla US";
            this.Raport.UseVisualStyleBackColor = true;
            this.Raport.Click += new System.EventHandler(this.Raport_Click);
            // 
            // Filtry
            // 
            this.Filtry.Controls.Add(this.comboBox2);
            this.Filtry.Controls.Add(this.label6);
            this.Filtry.Controls.Add(this.comboBox1);
            this.Filtry.Controls.Add(this.label5);
            this.Filtry.Controls.Add(this.Wyczysc);
            this.Filtry.Controls.Add(this.Zastosuj);
            this.Filtry.Location = new System.Drawing.Point(6, 123);
            this.Filtry.Name = "Filtry";
            this.Filtry.Size = new System.Drawing.Size(290, 139);
            this.Filtry.TabIndex = 8;
            this.Filtry.TabStop = false;
            this.Filtry.Text = "Filtry";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(166, 22);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(51, 23);
            this.comboBox2.Sorted = true;
            this.comboBox2.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(107, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Typ pliku";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(59, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(30, 23);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Miesiąc";
            // 
            // Wyczysc
            // 
            this.Wyczysc.Location = new System.Drawing.Point(149, 105);
            this.Wyczysc.Name = "Wyczysc";
            this.Wyczysc.Size = new System.Drawing.Size(135, 29);
            this.Wyczysc.TabIndex = 10;
            this.Wyczysc.Text = "Wyczyść";
            this.Wyczysc.UseVisualStyleBackColor = true;
            this.Wyczysc.Click += new System.EventHandler(this.Wyczysc_Click);
            // 
            // Zastosuj
            // 
            this.Zastosuj.Location = new System.Drawing.Point(8, 105);
            this.Zastosuj.Name = "Zastosuj";
            this.Zastosuj.Size = new System.Drawing.Size(135, 29);
            this.Zastosuj.TabIndex = 9;
            this.Zastosuj.Text = "Zastosuj";
            this.Zastosuj.UseVisualStyleBackColor = true;
            this.Zastosuj.Click += new System.EventHandler(this.Zastosuj_Click);
            // 
            // Pobierz
            // 
            this.Pobierz.Location = new System.Drawing.Point(208, 22);
            this.Pobierz.Name = "Pobierz";
            this.Pobierz.Size = new System.Drawing.Size(89, 62);
            this.Pobierz.TabIndex = 6;
            this.Pobierz.Text = "Pobierz plik";
            this.Pobierz.UseVisualStyleBackColor = true;
            this.Pobierz.Click += new System.EventHandler(this.Pobierz_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(101, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 62);
            this.button2.TabIndex = 5;
            this.button2.Text = "Usuń dzieło";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // DodajDzielo
            // 
            this.DodajDzielo.Location = new System.Drawing.Point(6, 22);
            this.DodajDzielo.Name = "DodajDzielo";
            this.DodajDzielo.Size = new System.Drawing.Size(89, 62);
            this.DodajDzielo.TabIndex = 3;
            this.DodajDzielo.Text = "Dodaj dzieło";
            this.DodajDzielo.UseVisualStyleBackColor = true;
            this.DodajDzielo.Click += new System.EventHandler(this.DodajDzielo_Click);
            // 
            // Szczegoly
            // 
            this.Szczegoly.Controls.Add(this.textBox4);
            this.Szczegoly.Controls.Add(this.textBox3);
            this.Szczegoly.Controls.Add(this.textBox2);
            this.Szczegoly.Controls.Add(this.textBox1);
            this.Szczegoly.Controls.Add(this.label4);
            this.Szczegoly.Controls.Add(this.label3);
            this.Szczegoly.Controls.Add(this.label2);
            this.Szczegoly.Controls.Add(this.label1);
            this.Szczegoly.Location = new System.Drawing.Point(704, 27);
            this.Szczegoly.Name = "Szczegoly";
            this.Szczegoly.Size = new System.Drawing.Size(303, 137);
            this.Szczegoly.TabIndex = 3;
            this.Szczegoly.TabStop = false;
            this.Szczegoly.Text = "Szczegóły";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(162, 101);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(135, 23);
            this.textBox4.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(162, 72);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(135, 23);
            this.textBox3.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(162, 42);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(135, 23);
            this.textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(162, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(135, 23);
            this.textBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dzieł w tym mc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pozostało godzin w tym mc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Potrzebnych godzin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "% praw autorskich";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1019, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(85, 20);
            this.toolStripMenuItem1.Text = "Baza danych";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 450);
            this.Controls.Add(this.Szczegoly);
            this.Controls.Add(this.Narzedzia);
            this.Controls.Add(this.Lista);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "PrawaAutorskie";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Lista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Narzedzia.ResumeLayout(false);
            this.Filtry.ResumeLayout(false);
            this.Filtry.PerformLayout();
            this.Szczegoly.ResumeLayout(false);
            this.Szczegoly.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox Lista;
        private System.Windows.Forms.GroupBox Narzedzia;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox Szczegoly;
        private System.Windows.Forms.Button DodajDzielo;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button Pobierz;
        private System.Windows.Forms.GroupBox Filtry;
        private System.Windows.Forms.Button Wyczysc;
        private System.Windows.Forms.Button Zastosuj;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button Raport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}

