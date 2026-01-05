
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
            Lista = new System.Windows.Forms.GroupBox();
            dataGridView1 = new System.Windows.Forms.DataGridView();
            Narzedzia = new System.Windows.Forms.GroupBox();
            checkBox3 = new System.Windows.Forms.CheckBox();
            Raport = new System.Windows.Forms.Button();
            Filtry = new System.Windows.Forms.GroupBox();
            comboBox3 = new System.Windows.Forms.ComboBox();
            label7 = new System.Windows.Forms.Label();
            comboBox2 = new System.Windows.Forms.ComboBox();
            label6 = new System.Windows.Forms.Label();
            comboBox1 = new System.Windows.Forms.ComboBox();
            label5 = new System.Windows.Forms.Label();
            checkBox2 = new System.Windows.Forms.CheckBox();
            Pobierz = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            checkBox1 = new System.Windows.Forms.CheckBox();
            DodajDzielo = new System.Windows.Forms.Button();
            textBox5 = new System.Windows.Forms.TextBox();
            Wyczysc = new System.Windows.Forms.Button();
            Zastosuj = new System.Windows.Forms.Button();
            Szczegoly = new System.Windows.Forms.GroupBox();
            comboBox4 = new System.Windows.Forms.ComboBox();
            label10 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            textBox7 = new System.Windows.Forms.TextBox();
            textBox6 = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            textBox4 = new System.Windows.Forms.TextBox();
            textBox3 = new System.Windows.Forms.TextBox();
            textBox2 = new System.Windows.Forms.TextBox();
            textBox1 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            formsPlot1 = new ScottPlot.FormsPlot();
            Lista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            Narzedzia.SuspendLayout();
            Filtry.SuspendLayout();
            Szczegoly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // Lista
            // 
            Lista.Controls.Add(dataGridView1);
            Lista.Location = new System.Drawing.Point(12, 194);
            Lista.Name = "Lista";
            Lista.Size = new System.Drawing.Size(686, 372);
            Lista.TabIndex = 0;
            Lista.TabStop = false;
            Lista.Text = "Lista";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new System.Drawing.Point(6, 21);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new System.Drawing.Size(674, 342);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellDoubleClick += dataGridView1_CellDoubleClick;
            dataGridView1.Sorted += dataGridView1_Sorted;
            // 
            // Narzedzia
            // 
            Narzedzia.Controls.Add(checkBox3);
            Narzedzia.Controls.Add(Raport);
            Narzedzia.Controls.Add(Filtry);
            Narzedzia.Controls.Add(checkBox2);
            Narzedzia.Controls.Add(Pobierz);
            Narzedzia.Controls.Add(button2);
            Narzedzia.Controls.Add(checkBox1);
            Narzedzia.Controls.Add(DodajDzielo);
            Narzedzia.Controls.Add(textBox5);
            Narzedzia.Controls.Add(Wyczysc);
            Narzedzia.Controls.Add(Zastosuj);
            Narzedzia.Location = new System.Drawing.Point(704, 282);
            Narzedzia.Name = "Narzedzia";
            Narzedzia.Size = new System.Drawing.Size(336, 284);
            Narzedzia.TabIndex = 2;
            Narzedzia.TabStop = false;
            Narzedzia.Text = "Narzędzia";
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox3.Location = new System.Drawing.Point(119, 221);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(45, 19);
            checkBox3.TabIndex = 18;
            checkBox3.Text = "Plik";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // Raport
            // 
            Raport.Location = new System.Drawing.Point(7, 90);
            Raport.Name = "Raport";
            Raport.Size = new System.Drawing.Size(317, 27);
            Raport.TabIndex = 9;
            Raport.Text = "Generuj raport dla US";
            Raport.UseVisualStyleBackColor = true;
            Raport.Click += Raport_Click;
            // 
            // Filtry
            // 
            Filtry.Controls.Add(comboBox3);
            Filtry.Controls.Add(label7);
            Filtry.Controls.Add(comboBox2);
            Filtry.Controls.Add(label6);
            Filtry.Controls.Add(comboBox1);
            Filtry.Controls.Add(label5);
            Filtry.Location = new System.Drawing.Point(6, 123);
            Filtry.Name = "Filtry";
            Filtry.Size = new System.Drawing.Size(324, 63);
            Filtry.TabIndex = 8;
            Filtry.TabStop = false;
            Filtry.Text = "Filtry";
            // 
            // comboBox3
            // 
            comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new System.Drawing.Point(141, 22);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new System.Drawing.Size(55, 23);
            comboBox3.Sorted = true;
            comboBox3.TabIndex = 16;
            comboBox3.SelectedIndexChanged += comboBox3_SelectedIndexChanged;
            comboBox3.Click += comboBox3_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 9F);
            label7.Location = new System.Drawing.Point(108, 28);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(27, 15);
            label7.TabIndex = 15;
            label7.Text = "Rok";
            // 
            // comboBox2
            // 
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(263, 22);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(55, 23);
            comboBox2.Sorted = true;
            comboBox2.TabIndex = 14;
            comboBox2.Click += comboBox2_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 9F);
            label6.Location = new System.Drawing.Point(202, 28);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(54, 15);
            label6.TabIndex = 13;
            label6.Text = "Typ pliku";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(59, 22);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(42, 23);
            comboBox1.TabIndex = 12;
            comboBox1.Click += comboBox1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 9F);
            label5.Location = new System.Drawing.Point(6, 28);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(47, 15);
            label5.TabIndex = 11;
            label5.Text = "Miesiąc";
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Checked = true;
            checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox2.Location = new System.Drawing.Point(63, 221);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(50, 19);
            checkBox2.TabIndex = 17;
            checkBox2.Text = "Opis";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // Pobierz
            // 
            Pobierz.Location = new System.Drawing.Point(221, 22);
            Pobierz.Name = "Pobierz";
            Pobierz.Size = new System.Drawing.Size(103, 62);
            Pobierz.TabIndex = 6;
            Pobierz.Text = "Pobierz plik";
            Pobierz.UseVisualStyleBackColor = true;
            Pobierz.Click += Pobierz_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(114, 22);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(101, 62);
            button2.TabIndex = 5;
            button2.Text = "Usuń dzieło";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Checked = true;
            checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox1.Location = new System.Drawing.Point(6, 221);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(51, 19);
            checkBox1.TabIndex = 16;
            checkBox1.Text = "Tytuł";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // DodajDzielo
            // 
            DodajDzielo.Location = new System.Drawing.Point(6, 22);
            DodajDzielo.Name = "DodajDzielo";
            DodajDzielo.Size = new System.Drawing.Size(101, 62);
            DodajDzielo.TabIndex = 3;
            DodajDzielo.Text = "Dodaj dzieło";
            DodajDzielo.UseVisualStyleBackColor = true;
            DodajDzielo.Click += DodajDzielo_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new System.Drawing.Point(7, 192);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(323, 23);
            textBox5.TabIndex = 15;
            textBox5.Text = "Szukaj w bazie danych bez filtrów";
            textBox5.Enter += textBox5_Enter;
            textBox5.KeyPress += textBox5_KeyPress;
            // 
            // Wyczysc
            // 
            Wyczysc.Location = new System.Drawing.Point(177, 246);
            Wyczysc.Name = "Wyczysc";
            Wyczysc.Size = new System.Drawing.Size(153, 29);
            Wyczysc.TabIndex = 10;
            Wyczysc.Text = "Wyczyść";
            Wyczysc.UseVisualStyleBackColor = true;
            Wyczysc.Click += Wyczysc_Click;
            // 
            // Zastosuj
            // 
            Zastosuj.Location = new System.Drawing.Point(6, 246);
            Zastosuj.Name = "Zastosuj";
            Zastosuj.Size = new System.Drawing.Size(165, 29);
            Zastosuj.TabIndex = 9;
            Zastosuj.Text = "Zastosuj";
            Zastosuj.UseVisualStyleBackColor = true;
            Zastosuj.Click += Zastosuj_Click;
            // 
            // Szczegoly
            // 
            Szczegoly.Controls.Add(comboBox4);
            Szczegoly.Controls.Add(label10);
            Szczegoly.Controls.Add(pictureBox1);
            Szczegoly.Controls.Add(textBox7);
            Szczegoly.Controls.Add(textBox6);
            Szczegoly.Controls.Add(label9);
            Szczegoly.Controls.Add(label8);
            Szczegoly.Controls.Add(textBox4);
            Szczegoly.Controls.Add(textBox3);
            Szczegoly.Controls.Add(textBox2);
            Szczegoly.Controls.Add(textBox1);
            Szczegoly.Controls.Add(label4);
            Szczegoly.Controls.Add(label3);
            Szczegoly.Controls.Add(label2);
            Szczegoly.Controls.Add(label1);
            Szczegoly.Location = new System.Drawing.Point(704, 27);
            Szczegoly.Name = "Szczegoly";
            Szczegoly.Size = new System.Drawing.Size(336, 249);
            Szczegoly.TabIndex = 3;
            Szczegoly.TabStop = false;
            Szczegoly.Text = "Szczegóły";
            // 
            // comboBox4
            // 
            comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { "Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień" });
            comboBox4.Location = new System.Drawing.Point(162, 23);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new System.Drawing.Size(162, 23);
            comboBox4.TabIndex = 17;
            comboBox4.SelectedIndexChanged += comboBox4_SelectedIndexChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(7, 31);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(47, 15);
            label10.TabIndex = 13;
            label10.Text = "Miesiąc";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.Color.FromArgb(255, 128, 128);
            pictureBox1.BackgroundImage = Properties.Resources.icon;
            pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pictureBox1.Location = new System.Drawing.Point(269, 82);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(55, 53);
            pictureBox1.TabIndex = 12;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // textBox7
            // 
            textBox7.Location = new System.Drawing.Point(162, 216);
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.Size = new System.Drawing.Size(162, 23);
            textBox7.TabIndex = 11;
            // 
            // textBox6
            // 
            textBox6.Location = new System.Drawing.Point(162, 187);
            textBox6.Name = "textBox6";
            textBox6.ReadOnly = true;
            textBox6.Size = new System.Drawing.Size(162, 23);
            textBox6.TabIndex = 10;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(7, 224);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(88, 15);
            label9.TabIndex = 9;
            label9.Text = "Godzin ogółem";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(7, 195);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(76, 15);
            label8.TabIndex = 8;
            label8.Text = "Dzieł ogółem";
            // 
            // textBox4
            // 
            textBox4.Location = new System.Drawing.Point(162, 141);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.Size = new System.Drawing.Size(162, 23);
            textBox4.TabIndex = 7;
            // 
            // textBox3
            // 
            textBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            textBox3.Location = new System.Drawing.Point(162, 112);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new System.Drawing.Size(101, 23);
            textBox3.TabIndex = 6;
            // 
            // textBox2
            // 
            textBox2.Location = new System.Drawing.Point(162, 82);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new System.Drawing.Size(101, 23);
            textBox2.TabIndex = 5;
            // 
            // textBox1
            // 
            textBox1.Location = new System.Drawing.Point(162, 53);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(162, 23);
            textBox1.TabIndex = 4;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(7, 149);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(88, 15);
            label4.TabIndex = 3;
            label4.Text = "Dzieł w tym mc";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(7, 120);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(153, 15);
            label3.TabIndex = 2;
            label3.Text = "Pozostało godzin w tym mc";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(7, 90);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(112, 15);
            label2.TabIndex = 1;
            label2.Text = "Potrzebnych godzin";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(7, 62);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(104, 15);
            label1.TabIndex = 0;
            label1.Text = "% praw autorskich";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripMenuItem1 });
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1052, 24);
            menuStrip1.TabIndex = 4;
            menuStrip1.Text = "menuStrip1";
            menuStrip1.ItemClicked += menuStrip1_ItemClicked;
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(85, 20);
            toolStripMenuItem1.Text = "Baza danych";
            // 
            // formsPlot1
            // 
            formsPlot1.Location = new System.Drawing.Point(12, 27);
            formsPlot1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new System.Drawing.Size(680, 164);
            formsPlot1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1052, 578);
            Controls.Add(formsPlot1);
            Controls.Add(Szczegoly);
            Controls.Add(Narzedzia);
            Controls.Add(Lista);
            Controls.Add(menuStrip1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "PrawaAutorskie";
            Load += Form1_Load;
            Lista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            Narzedzia.ResumeLayout(false);
            Narzedzia.PerformLayout();
            Filtry.ResumeLayout(false);
            Filtry.PerformLayout();
            Szczegoly.ResumeLayout(false);
            Szczegoly.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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

