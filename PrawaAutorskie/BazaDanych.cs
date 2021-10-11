using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;

namespace PrawaAutorskie
{
    public partial class BazaDanych : Form
    {
        private Form1 principalForm = System.Windows.Forms.Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public BazaDanych()
        {
            InitializeComponent();
        }

        private void BazaDanych_FormClosing(object sender, FormClosingEventArgs e)
        {
            principalForm.Enabled = true;
            principalForm.CheckConn();
        }

        public bool CheckSQLConnection(string _connectionstring)
        {
            try
            {
                string connetionString;
            SqlConnection cnn;
            connetionString = _connectionstring;
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            cnn.Close();
            return true;
            }
            catch
            {
                return false;
            }

        }

        void FillData(string _connetionstring)
        {
            try
            {
                if (CheckSQLConnection(_connetionstring))
                {
                    textBox1.Text = "Połączono";
                }
                else
                {
                    textBox1.Text = "Brak połączenia";
                }
                textBox2.Text = "PrawaAutorskie";
                textBox3.Text = principalForm.ReadSQL($"DECLARE @cos as varchar(100) ;select @cos = cast(size/128 as varchar) FROM sys.database_files where type = 0 select @cos +N'MB Baza + '+ cast(size/128 as varchar) + N'MB Log' FROM sys.database_files where type = 1", Form1.initialcatalogConnectionString);
                textBox4.Text = principalForm.ReadSQL($"SELECT physical_name AS data_file FROM sys.master_files WHERE name = 'PrawaAutorskie'", Form1.initialcatalogConnectionString);
                textBox5.Text = principalForm.ReadSQL($"SELECT physical_name AS data_file FROM sys.master_files WHERE name = 'PrawaAutorskie_log'", Form1.initialcatalogConnectionString);
                textBox6.Text = Form1.masterConnectionString;
                textBox7.Text = Form1.initialcatalogConnectionString;
                textBox8.Text = principalForm.ReadSQL($"SELECT a.backup_finish_date as 'Data zakonczenia backupu' FROM[msdb].[dbo].[backupset] a inner join[msdb].[dbo].[backupmediafamily] b on(a.media_set_id = b.media_set_id)", Form1.initialcatalogConnectionString);
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie można załadować informacji o bazie danych", "Błąd");
            }
            
        }

        private void BazaDanych_Load(object sender, EventArgs e)
        {
            FillData(Form1.masterConnectionString);
            comboBox1.SelectedIndex = 1;
        }

        private void ZrobBackup_Click(object sender, EventArgs e)
        {
            if (principalForm.ExecuteSQLStmt($"BACKUP DATABASE [PrawaAutorskie] TO  DISK = N'C:\\PrawaAutorskie\\PrawaAutorskie{DateTime.Now:ddMMyyyy}.bak' WITH NOFORMAT, NOINIT,  NAME = N'PrawaAutorskie-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10", Form1.initialcatalogConnectionString))
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Backup zakończony sukcesem", "Sukces");
            }
        }

        private void ZapiszMCS_Click(object sender, EventArgs e)
        {
            principalForm.UpdateConfigXML(textBox6.Text, "master");
        }

        private void ZapiszICS_Click(object sender, EventArgs e)
        {
            principalForm.UpdateConfigXML(textBox7.Text, "initial");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                OdtworzBaze.Enabled = true;
                UsunBaze.Enabled = true;
            }
            else
            {
                OdtworzBaze.Enabled = false;
                UsunBaze.Enabled = false;
            }
        }

        private void UsunBaze_Click(object sender, EventArgs e)
        {
            if (principalForm.ExecuteSQLStmt($"ALTER DATABASE PrawaAutorskie SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE PrawaAutorskie", Form1.masterConnectionString))
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Baza danych usunięta", "Sukces");
            }
            
        }

        private void OdtworzBaze_Click(object sender, EventArgs e)
        {
            if (principalForm.PrepareDatabase(Form1.masterConnectionString, "PrawaAutorskie"))
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Baza odtworzona pomyślnie", "Sukces");
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie udało się odtworzyć bazy", "Błąd");
            }
        }

        private void textBox9_Enter(object sender, EventArgs e)
        {
            WyslijRequest.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //RESTORE DATABASE [PrawaAutorskie] FROM  DISK = N'C:\PrawaAutorskie\PrawaAutorskie27092021_2.bak' WITH  FILE = 1,  NOUNLOAD,  STATS = 5
            try
            {
                openFileDialog1.FileName = "";
                openFileDialog1.Title = "Załaduj plik backupu";
                openFileDialog1.Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                        string file = openFileDialog1.FileName.Replace(@"\", @"\\");
                        if (principalForm.ExecuteSQLStmt($"RESTORE DATABASE [PrawaAutorskie] FROM  DISK = N'{file}' WITH  FILE = 1,  NOUNLOAD,  STATS = 5", Form1.masterConnectionString))
                        {
                        SystemSounds.Hand.Play();
                        MessageBox.Show("Restore zakończony sukcesem", "Sukces");
                        }
                        else
                        {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Restore nie powiódł się", "Błąd");
                        }
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie udało się załadować pliku", "Błąd");
            }

        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            CzytajBaze.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckSQLConnection(Form1.masterConnectionString))
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Nawiązano połączenie z serwerem SQL", "Sukces");
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie udało się nawiązać połączenia z serwerem SQL", "Błąd");
            }
        }

        private void WyslijRequest_Click(object sender, EventArgs e)
        {
            string cs = Form1.initialcatalogConnectionString;
            if (comboBox1.SelectedIndex == 0)
            {
                cs = Form1.masterConnectionString;
            }
            if (principalForm.ExecuteSQLStmt($"{textBox9.Text}", cs))
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Request zakończony sukcesem", "Sukces");
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Request nie powiódł się", "Błąd");
            }
        }

        private void CzytajBaze_Click(object sender, EventArgs e)
        {
            SystemSounds.Hand.Play();
            MessageBox.Show(principalForm.ReadSQL(textBox10.Text, Form1.initialcatalogConnectionString), "Sukces");
        }
    }
}
