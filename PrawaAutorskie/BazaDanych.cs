using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

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
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public bool CheckSQLConnection(string _connectionstring)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = _connectionstring;
            cnn = new SqlConnection(connetionString);
            try
            {
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
                MessageBox.Show("Nie można załadować informacji o bazie danych", "Błąd");
            }
            
        }

        private void BazaDanych_Load(object sender, EventArgs e)
        {
            FillData(Form1.masterConnectionString);
        }

        private void ZrobBackup_Click(object sender, EventArgs e)
        {
            if (principalForm.ExecuteSQLStmt($"BACKUP DATABASE [PrawaAutorskie] TO  DISK = N'C:\\PrawaAutorskie\\PrawaAutorskie{DateTime.Now:ddMMyyyy}.bak' WITH NOFORMAT, NOINIT,  NAME = N'PrawaAutorskie-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10", Form1.initialcatalogConnectionString))
            {
                MessageBox.Show("Backup zakończony sukcesem");
            }
        }
    }
}
