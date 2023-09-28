
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
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.Raport = new System.Windows.Forms.Button();
            this.Filtry = new System.Windows.Forms.GroupBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.Pobierz = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.DodajDzielo = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.Wyczysc = new System.Windows.Forms.Button();
            this.Zastosuj = new System.Windows.Forms.Button();
            this.Szczegoly = new System.Windows.Forms.GroupBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
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
            this.formsPlot1 = new ScottPlot.FormsPlot();
            this.Lista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.Narzedzia.SuspendLayout();
            this.Filtry.SuspendLayout();
            this.Szczegoly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Lista
            // 
            this.Lista.Controls.Add(this.dataGridView1);
            this.Lista.Location = new System.Drawing.Point(12, 194);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(686, 372);
            this.Lista.TabIndex = 0;
            this.Lista.TabStop = false;
            this.Lista.Text = "Lista";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(674, 342);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.Sorted += new System.EventHandler(this.dataGridView1_Sorted);
            // 
            // Narzedzia
            // 
            this.Narzedzia.Controls.Add(this.checkBox3);
            this.Narzedzia.Controls.Add(this.Raport);
            this.Narzedzia.Controls.Add(this.Filtry);
            this.Narzedzia.Controls.Add(this.checkBox2);
            this.Narzedzia.Controls.Add(this.Pobierz);
            this.Narzedzia.Controls.Add(this.button2);
            this.Narzedzia.Controls.Add(this.checkBox1);
            this.Narzedzia.Controls.Add(this.DodajDzielo);
            this.Narzedzia.Controls.Add(this.textBox5);
            this.Narzedzia.Controls.Add(this.Wyczysc);
            this.Narzedzia.Controls.Add(this.Zastosuj);
            this.Narzedzia.Location = new System.Drawing.Point(704, 282);
            this.Narzedzia.Name = "Narzedzia";
            this.Narzedzia.Size = new System.Drawing.Size(336, 284);
            this.Narzedzia.TabIndex = 2;
            this.Narzedzia.TabStop = false;
            this.Narzedzia.Text = "Narzędzia";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Checked = true;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox3.Location = new System.Drawing.Point(119, 221);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(45, 19);
            this.checkBox3.TabIndex = 18;
            this.checkBox3.Text = "Plik";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // Raport
            // 
            this.Raport.Location = new System.Drawing.Point(7, 90);
            this.Raport.Name = "Raport";
            this.Raport.Size = new System.Drawing.Size(317, 27);
            this.Raport.TabIndex = 9;
            this.Raport.Text = "Generuj raport dla US";
            this.Raport.UseVisualStyleBackColor = true;
            this.Raport.Click += new System.EventHandler(this.Raport_Click);
            // 
            // Filtry
            // 
            this.Filtry.Controls.Add(this.comboBox3);
            this.Filtry.Controls.Add(this.label7);
            this.Filtry.Controls.Add(this.comboBox2);
            this.Filtry.Controls.Add(this.label6);
            this.Filtry.Controls.Add(this.comboBox1);
            this.Filtry.Controls.Add(this.label5);
            this.Filtry.Location = new System.Drawing.Point(6, 123);
            this.Filtry.Name = "Filtry";
            this.Filtry.Size = new System.Drawing.Size(324, 63);
            this.Filtry.TabIndex = 8;
            this.Filtry.TabStop = false;
            this.Filtry.Text = "Filtry";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(141, 22);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(55, 23);
            this.comboBox3.Sorted = true;
            this.comboBox3.TabIndex = 16;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            this.comboBox3.Click += new System.EventHandler(this.comboBox3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(108, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "Rok";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(263, 22);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(55, 23);
            this.comboBox2.Sorted = true;
            this.comboBox2.TabIndex = 14;
            this.comboBox2.Click += new System.EventHandler(this.comboBox2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(202, 28);
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
            this.comboBox1.Size = new System.Drawing.Size(42, 23);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
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
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(63, 221);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(50, 19);
            this.checkBox2.TabIndex = 17;
            this.checkBox2.Text = "Opis";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Pobierz
            // 
            this.Pobierz.Location = new System.Drawing.Point(221, 22);
            this.Pobierz.Name = "Pobierz";
            this.Pobierz.Size = new System.Drawing.Size(103, 62);
            this.Pobierz.TabIndex = 6;
            this.Pobierz.Text = "Pobierz plik";
            this.Pobierz.UseVisualStyleBackColor = true;
            this.Pobierz.Click += new System.EventHandler(this.Pobierz_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(114, 22);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(101, 62);
            this.button2.TabIndex = 5;
            this.button2.Text = "Usuń dzieło";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 221);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(51, 19);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Tytuł";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // DodajDzielo
            // 
            this.DodajDzielo.Location = new System.Drawing.Point(6, 22);
            this.DodajDzielo.Name = "DodajDzielo";
            this.DodajDzielo.Size = new System.Drawing.Size(101, 62);
            this.DodajDzielo.TabIndex = 3;
            this.DodajDzielo.Text = "Dodaj dzieło";
            this.DodajDzielo.UseVisualStyleBackColor = true;
            this.DodajDzielo.Click += new System.EventHandler(this.DodajDzielo_Click);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(7, 192);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(323, 23);
            this.textBox5.TabIndex = 15;
            this.textBox5.Text = "Szukaj w bazie danych bez filtrów";
            this.textBox5.Enter += new System.EventHandler(this.textBox5_Enter);
            this.textBox5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox5_KeyPress);
            // 
            // Wyczysc
            // 
            this.Wyczysc.Location = new System.Drawing.Point(177, 246);
            this.Wyczysc.Name = "Wyczysc";
            this.Wyczysc.Size = new System.Drawing.Size(153, 29);
            this.Wyczysc.TabIndex = 10;
            this.Wyczysc.Text = "Wyczyść";
            this.Wyczysc.UseVisualStyleBackColor = true;
            this.Wyczysc.Click += new System.EventHandler(this.Wyczysc_Click);
            // 
            // Zastosuj
            // 
            this.Zastosuj.Location = new System.Drawing.Point(6, 246);
            this.Zastosuj.Name = "Zastosuj";
            this.Zastosuj.Size = new System.Drawing.Size(165, 29);
            this.Zastosuj.TabIndex = 9;
            this.Zastosuj.Text = "Zastosuj";
            this.Zastosuj.UseVisualStyleBackColor = true;
            this.Zastosuj.Click += new System.EventHandler(this.Zastosuj_Click);
            // 
            // Szczegoly
            // 
            this.Szczegoly.Controls.Add(this.comboBox4);
            this.Szczegoly.Controls.Add(this.label10);
            this.Szczegoly.Controls.Add(this.pictureBox1);
            this.Szczegoly.Controls.Add(this.textBox7);
            this.Szczegoly.Controls.Add(this.textBox6);
            this.Szczegoly.Controls.Add(this.label9);
            this.Szczegoly.Controls.Add(this.label8);
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
            this.Szczegoly.Size = new System.Drawing.Size(336, 249);
            this.Szczegoly.TabIndex = 3;
            this.Szczegoly.TabStop = false;
            this.Szczegoly.Text = "Szczegóły";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "Styczeń",
            "Luty",
            "Marzec",
            "Kwiecień",
            "Maj",
            "Czerwiec",
            "Lipiec",
            "Sierpień",
            "Wrzesień",
            "Październik",
            "Listopad",
            "Grudzień"});
            this.comboBox4.Location = new System.Drawing.Point(162, 23);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(162, 23);
            this.comboBox4.TabIndex = 17;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.comboBox4_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 31);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(47, 15);
            this.label10.TabIndex = 13;
            this.label10.Text = "Miesiąc";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.pictureBox1.BackgroundImage = global::PrawaAutorskie.Properties.Resources.icon;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Location = new System.Drawing.Point(269, 82);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 53);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(162, 216);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(162, 23);
            this.textBox7.TabIndex = 11;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(162, 187);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(162, 23);
            this.textBox6.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 15);
            this.label9.TabIndex = 9;
            this.label9.Text = "Godzin ogółem";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 195);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 15);
            this.label8.TabIndex = 8;
            this.label8.Text = "Dzieł ogółem";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(162, 141);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(162, 23);
            this.textBox4.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox3.Location = new System.Drawing.Point(162, 112);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(101, 23);
            this.textBox3.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(162, 82);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(101, 23);
            this.textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(162, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(162, 23);
            this.textBox1.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 15);
            this.label4.TabIndex = 3;
            this.label4.Text = "Dzieł w tym mc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(153, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pozostało godzin w tym mc";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Potrzebnych godzin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 62);
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
            this.menuStrip1.Size = new System.Drawing.Size(1052, 24);
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
            // formsPlot1
            // 
            this.formsPlot1.Location = new System.Drawing.Point(12, 27);
            this.formsPlot1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.formsPlot1.Name = "formsPlot1";
            this.formsPlot1.Size = new System.Drawing.Size(680, 164);
            this.formsPlot1.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1052, 578);
            this.Controls.Add(this.formsPlot1);
            this.Controls.Add(this.Szczegoly);
            this.Controls.Add(this.Narzedzia);
            this.Controls.Add(this.Lista);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrawaAutorskie";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Lista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.Narzedzia.ResumeLayout(false);
            this.Narzedzia.PerformLayout();
            this.Filtry.ResumeLayout(false);
            this.Filtry.PerformLayout();
            this.Szczegoly.ResumeLayout(false);
            this.Szczegoly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label10;
        private ScottPlot.FormsPlot formsPlot1;
    }
}

