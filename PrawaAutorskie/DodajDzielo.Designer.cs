
namespace PrawaAutorskie
{
    partial class DodajDzielo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DodajDzielo));
            this.Dodaj = new System.Windows.Forms.Button();
            this.Dane = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.Data = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Godziny = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Tytul = new System.Windows.Forms.Label();
            this.Pliki = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dodajplik = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.Dane.SuspendLayout();
            this.Pliki.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // Dodaj
            // 
            this.Dodaj.Enabled = false;
            this.Dodaj.Location = new System.Drawing.Point(12, 448);
            this.Dodaj.Name = "Dodaj";
            this.Dodaj.Size = new System.Drawing.Size(135, 51);
            this.Dodaj.TabIndex = 0;
            this.Dodaj.Text = "Dodaj";
            this.Dodaj.UseVisualStyleBackColor = true;
            this.Dodaj.Click += new System.EventHandler(this.Dodaj_Click);
            // 
            // Dane
            // 
            this.Dane.Controls.Add(this.richTextBox1);
            this.Dane.Controls.Add(this.dateTimePicker1);
            this.Dane.Controls.Add(this.Data);
            this.Dane.Controls.Add(this.textBox2);
            this.Dane.Controls.Add(this.Godziny);
            this.Dane.Controls.Add(this.textBox1);
            this.Dane.Controls.Add(this.Tytul);
            this.Dane.Location = new System.Drawing.Point(12, 5);
            this.Dane.Name = "Dane";
            this.Dane.Size = new System.Drawing.Size(276, 281);
            this.Dane.TabIndex = 2;
            this.Dane.TabStop = false;
            this.Dane.Text = "Dane";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(8, 109);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(260, 165);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "Implementacja funkcjonalności pozwalającej na";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(67, 80);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 23);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // Data
            // 
            this.Data.AutoSize = true;
            this.Data.Location = new System.Drawing.Point(8, 86);
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size(31, 15);
            this.Data.TabIndex = 4;
            this.Data.Text = "Data";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(67, 51);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 23);
            this.textBox2.TabIndex = 3;
            // 
            // Godziny
            // 
            this.Godziny.AutoSize = true;
            this.Godziny.Location = new System.Drawing.Point(8, 59);
            this.Godziny.Name = "Godziny";
            this.Godziny.Size = new System.Drawing.Size(31, 15);
            this.Godziny.TabIndex = 2;
            this.Godziny.Text = "Czas";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(67, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 23);
            this.textBox1.TabIndex = 1;
            // 
            // Tytul
            // 
            this.Tytul.AutoSize = true;
            this.Tytul.Location = new System.Drawing.Point(8, 30);
            this.Tytul.Name = "Tytul";
            this.Tytul.Size = new System.Drawing.Size(32, 15);
            this.Tytul.TabIndex = 0;
            this.Tytul.Text = "Tytuł";
            // 
            // Pliki
            // 
            this.Pliki.Controls.Add(this.dataGridView1);
            this.Pliki.Location = new System.Drawing.Point(12, 285);
            this.Pliki.Name = "Pliki";
            this.Pliki.Size = new System.Drawing.Size(276, 157);
            this.Pliki.TabIndex = 3;
            this.Pliki.TabStop = false;
            this.Pliki.Text = "Pliki";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Enabled = false;
            this.dataGridView1.Location = new System.Drawing.Point(7, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(260, 129);
            this.dataGridView1.TabIndex = 0;
            // 
            // dodajplik
            // 
            this.dodajplik.Location = new System.Drawing.Point(153, 448);
            this.dodajplik.Name = "dodajplik";
            this.dodajplik.Size = new System.Drawing.Size(135, 51);
            this.dodajplik.TabIndex = 4;
            this.dodajplik.Text = "Dodaj plik";
            this.dodajplik.UseVisualStyleBackColor = true;
            this.dodajplik.Click += new System.EventHandler(this.dodajplik_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // DodajDzielo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 502);
            this.Controls.Add(this.dodajplik);
            this.Controls.Add(this.Pliki);
            this.Controls.Add(this.Dane);
            this.Controls.Add(this.Dodaj);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DodajDzielo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dodaj dzieło";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DodajDzielo_FormClosing);
            this.Load += new System.EventHandler(this.DodajDzielo_Load);
            this.Dane.ResumeLayout(false);
            this.Dane.PerformLayout();
            this.Pliki.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Dodaj;
        private System.Windows.Forms.GroupBox Dane;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Tytul;
        private System.Windows.Forms.Label Data;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label Godziny;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox Pliki;
        private System.Windows.Forms.Button dodajplik;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}