using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Media;
using System.Threading.Tasks;
using System.Threading;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2;
using static Google.Apis.Drive.v3.DriveService;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using System.IO;
using Google.Apis.Upload;

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
            Loading load = new Loading();
            Task th = new Task(() => Application.Run(load));
            string connetionString;
            SqlConnection cnn;
            connetionString = _connectionstring;
            cnn = new SqlConnection(connetionString);
            try
            {
                // Start the task.
                var task = Task.Factory.StartNew(() =>
                {
                    cnn.Open();
                    cnn.Close();
                });
                if (Application.OpenForms["Loading"] == null)
                {                 
                   th.Start();
                   th.Wait(500);
                }
                // Await the task.
                task.Wait();
                load.Invoke(new Action(() => load.Close()));
                return true;
            }
            catch (Exception ex)
            {
                load.Invoke(new Action(() => load.Close()));
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
                    textBox2.Text = "PrawaAutorskie";
                    textBox3.Text = principalForm.ReadSQL($"DECLARE @cos as varchar(100) ;select @cos = cast(size/128 as varchar) FROM sys.database_files where type = 0 select @cos +N'MB Baza + '+ cast(size/128 as varchar) + N'MB Log' FROM sys.database_files where type = 1", Form1.initialcatalogConnectionString);
                    textBox4.Text = principalForm.ReadSQL($"SELECT physical_name AS data_file FROM sys.master_files WHERE name = 'PrawaAutorskie'", Form1.initialcatalogConnectionString);
                    textBox5.Text = principalForm.ReadSQL($"SELECT physical_name AS data_file FROM sys.master_files WHERE name = 'PrawaAutorskie_log'", Form1.initialcatalogConnectionString);
                    textBox6.Text = Form1.masterConnectionString;
                    textBox7.Text = Form1.initialcatalogConnectionString;
                    textBox8.Text = principalForm.ReadSQL($"SELECT a.backup_finish_date as 'Data zakonczenia backupu' FROM[msdb].[dbo].[backupset] a inner join[msdb].[dbo].[backupmediafamily] b on(a.media_set_id = b.media_set_id)", Form1.initialcatalogConnectionString);
                }
                else
                {
                    textBox1.Text = "Brak połączenia";
                    textBox2.Text = "PrawaAutorskie";
                    textBox3.Text = "Brak połączenia";
                    textBox4.Text = "Brak połączenia";
                    textBox5.Text = "Brak połączenia";
                    textBox6.Text = Form1.masterConnectionString;
                    textBox7.Text = Form1.initialcatalogConnectionString;
                    textBox8.Text = "Brak połączenia";
                }
     
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
            FillBackupsComboBox();            
        }

        private void ZrobBackup_Click(object sender, EventArgs e)
        {
            string nazwa = $"PrawaAutorskie{DateTime.Now:ddMMyyyy}.bak";
            if (principalForm.ExecuteSQLStmt($"IF NOT EXISTS (select Nazwa from Backups where Nazwa ='{nazwa}') INSERT INTO Backups(Nazwa) " + $"VALUES ('{nazwa}')", Form1.initialcatalogConnectionString) && principalForm.ExecuteSQLStmt($"BACKUP DATABASE [PrawaAutorskie] TO  DISK = N'{nazwa}' WITH NOFORMAT, INIT,  NAME = N'PrawaAutorskie-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10", Form1.initialcatalogConnectionString))
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Backup zakończony sukcesem", "Sukces");
                principalForm.ExecuteSQLStmt($"IF NOT EXISTS (select Nazwa from Backups where Nazwa ='{nazwa}') INSERT INTO Backups(Nazwa) " + $"VALUES ('{nazwa}')", Form1.initialcatalogConnectionString);
                FillBackupsComboBox();
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
            if (comboBox2.SelectedIndex == -1)
            {
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
            else
            {
                try
                {
                    string file = comboBox2.SelectedItem.ToString();
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
                catch
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Restore nie powiódł się", "Błąd");
                }
            }

        }

        private void textBox10_Enter(object sender, EventArgs e)
        {
            CzytajBaze.Enabled = true;
            EnableGoogleDrive(false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckSQLConnection(Form1.masterConnectionString))
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Nawiązano połączenie z serwerem SQL", "Sukces");
                FillData(Form1.masterConnectionString);
                comboBox1.SelectedIndex = 1;
                FillBackupsComboBox();
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie udało się nawiązać połączenia z serwerem SQL", "Błąd");
                FillData(Form1.masterConnectionString);
                comboBox1.SelectedIndex = 1;
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
            LoadTable(textBox10.Text);
            //MessageBox.Show(principalForm.ReadSQL(textBox10.Text, Form1.initialcatalogConnectionString), "Sukces");
        }
        void FillBackupsComboBox()
        {
            try
            {
                int i = 0;
                int tableRows = Convert.ToInt32(principalForm.ReadSQL($"SELECT count(*) FROM Backups", Form1.initialcatalogConnectionString));
                if(tableRows < 10)
                {
                    tableRows = 10;
                }
                // Open the connection  
                if (CheckSQLConnection(Form1.initialcatalogConnectionString))
                {
                    // Create a connection  
                    SqlConnection conn = new SqlConnection(Form1.initialcatalogConnectionString);
                    conn.ConnectionString = (Form1.initialcatalogConnectionString);
                    conn.Open();
                    // Create a data adapter  
                    SqlDataAdapter da = new SqlDataAdapter
                    ($"SELECT * from Backups", conn);
                    // Create DataSet, fill it and view in data grid  
                    DataSet ds = new DataSet("Backups");
                    da.Fill(ds, "Backups");
                    DataTable dziela = ds.Tables["Backups"];
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        i++;
                        if (!comboBox2.Items.Contains((string)row["Nazwa"]) && i > tableRows-10)
                        {
                            comboBox2.Items.Add((string)row["Nazwa"]);
                        }
                    }
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Nie można załadować listy backupów", "Błąd");
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie można załadować listy backupów", "Błąd");
            }           
        }

        void LoadTable(string _query)
        {
            try
            {
            // Create a connection  
            SqlConnection conn = new SqlConnection(Form1.initialcatalogConnectionString);
            // Open the connection  
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.ConnectionString = (Form1.initialcatalogConnectionString);
            conn.Open();           
            // Create a data adapter  
            SqlDataAdapter da = new SqlDataAdapter
            (_query, conn);
            // Create DataSet, fill it and view in data grid  
            DataSet ds = new DataSet("Response");
            da.Fill(ds, "Response");
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = ds.Tables["Response"].DefaultView;
            dataGridView1.AutoResizeColumns();
            DataTable dziela = ds.Tables["Response"];
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie można wykonać zapytania", "Błąd");
            }          
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            int asc = e.KeyChar;
            if (asc == 13)
            {
                CzytajBaze_Click(sender, e);
                e.Handled = true;
            }
        }

        private void dataGridView1_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                EnableGoogleDrive(true);
            }
            else
            {
                EnableGoogleDrive(false);
            }
        }

        void EnableGoogleDrive(bool _status)
        {
            if(_status)
            {
                button3.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                principalForm.ExecuteSQLStmt($"IF OBJECT_ID(N'GoogleDrive', N'U') IS NULL CREATE TABLE GoogleDrive" + "(applicationName VARCHAR(255), username VARCHAR(255), ClientId VARCHAR(255), ClientSecret VARCHAR(255), AccessToken VARCHAR(255), RefreshToken VARCHAR(255), BackupFolderId VARCHAR(255))", Form1.initialcatalogConnectionString);
                LoadTable($"SELECT * FROM GoogleDrive");
                //dataGridView1.Enabled = true;
                textBox10.Clear();
                CzytajBaze.Enabled = false;
            }
            else
            {
                button3.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = false;
                dataGridView1.Columns.Clear();
                //dataGridView1.Enabled = false;
                checkBox2.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadTable($"SELECT * FROM GoogleDrive");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(principalForm.ExecuteSQLStmt($"IF EXISTS (select * from GoogleDrive) UPDATE GoogleDrive SET applicationName='{dataGridView1.Rows[0].Cells[0].Value}', username ='{dataGridView1.Rows[0].Cells[1].Value}',ClientId ='{dataGridView1.Rows[0].Cells[2].Value}', ClientSecret ='{dataGridView1.Rows[0].Cells[3].Value}',AccessToken ='{dataGridView1.Rows[0].Cells[4].Value}',RefreshToken ='{dataGridView1.Rows[0].Cells[5].Value}',BackupFolderId ='{dataGridView1.Rows[0].Cells[6].Value}' else INSERT INTO GoogleDrive (applicationName,username,ClientId,ClientSecret,AccessToken,RefreshToken,BackupFolderId) VALUES ('{dataGridView1.Rows[0].Cells[0].Value}','{dataGridView1.Rows[0].Cells[1].Value},','{dataGridView1.Rows[0].Cells[2].Value}','{dataGridView1.Rows[0].Cells[3].Value}','{dataGridView1.Rows[0].Cells[4].Value}','{dataGridView1.Rows[0].Cells[5].Value}','{dataGridView1.Rows[0].Cells[6].Value}')", Form1.initialcatalogConnectionString))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Dane zapisane prawidłowo", "Sukces");
            }
        }

        private DriveService GetService()
        {

            var tokenResponse = new TokenResponse
            {
                AccessToken = principalForm.ReadSQL($"SELECT AccessToken FROM GoogleDrive", Form1.initialcatalogConnectionString),
                RefreshToken = principalForm.ReadSQL($"SELECT RefreshToken FROM GoogleDrive", Form1.initialcatalogConnectionString),
            };


            var applicationName = principalForm.ReadSQL($"SELECT applicationName FROM GoogleDrive", Form1.initialcatalogConnectionString);
            var username = principalForm.ReadSQL($"SELECT username FROM GoogleDrive", Form1.initialcatalogConnectionString);


            var apiCodeFlow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new ClientSecrets
                {
                    ClientId = principalForm.ReadSQL($"SELECT ClientId FROM GoogleDrive", Form1.initialcatalogConnectionString),
                    ClientSecret = principalForm.ReadSQL($"SELECT ClientSecret FROM GoogleDrive", Form1.initialcatalogConnectionString),
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            });


    var credential = new UserCredential(apiCodeFlow, username, tokenResponse);


            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = applicationName
            });
    return service;
        }
        public string CreateFolder(string folderName)
        {
            var service = GetService();
            var driveFolder = new Google.Apis.Drive.v3.Data.File();
            driveFolder.Name = folderName;
            driveFolder.MimeType = "application/vnd.google-apps.folder";
            var command = service.Files.Create(driveFolder);
            var file = command.Execute();
            return file.Id;
        }

        public string UploadFile(Stream file, string fileName, string fileMime, string folder, string fileDescription)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DriveService service = GetService();


                var driveFile = new Google.Apis.Drive.v3.Data.File();
                driveFile.Name = fileName;
                driveFile.Description = fileDescription;
                driveFile.MimeType = fileMime;
                driveFile.Parents = new string[] { folder };


                var request = service.Files.Create(driveFile, file, fileMime);
                request.Fields = "id";

                var response = request.Upload();
                if (response.Status != Google.Apis.Upload.UploadStatus.Completed)
                    throw response.Exception;
                Cursor.Current = Cursors.Default;
                return request.ResponseBody.Id;
            }
            catch
            {
                Cursor.Current = Cursors.Default;
                return "error";
            }

        }

        public async Task UploadFileAsync(Stream file, string fileName, string fileMime, string folder, string fileDescription)
        {
            try
            {
                DriveService service = GetService();


                var driveFile = new Google.Apis.Drive.v3.Data.File();
                driveFile.Name = fileName;
                driveFile.Description = fileDescription;
                driveFile.MimeType = fileMime;
                driveFile.Parents = new string[] { folder };


                var request = service.Files.Create(driveFile, file, fileMime);
                request.Fields = "id";
                request.ProgressChanged += (progress) =>
                {
                    // Update UI with progress
                    if (progress.Status == UploadStatus.Uploading)
                    {
                        int percentComplete = (int)((double)progress.BytesSent / file.Length * 100);
                        // Safely update UI from background thread
                        this.Invoke(new Action(() =>
                        {
                            progressBar1.Value = percentComplete;
                        }));
                    }
                    else if (progress.Status == UploadStatus.Completed)
                    {
                        this.Invoke(new Action(() =>
                        {
                            progressBar1.Value = 100;
                        }));
                    }
                    else if (progress.Status == UploadStatus.Failed)
                    {
                        this.Invoke(new Action(() =>
                        {
                            SystemSounds.Beep.Play();
                            MessageBox.Show(progress.Exception.Message, "Błąd");
                        }));
                    }
                };
                await request.UploadAsync();
            }
            catch
            {

            }

        }

        public bool GetBackupStatus (string folder, string _fileName)
        {
            try
            {
                DriveService service = GetService();
                // Define parameters of request
                FilesResource.ListRequest listRequest = service.Files.List();
                listRequest.PageSize = 100;
                listRequest.Fields = "nextPageToken, files(id, name)";
                listRequest.Q = $"'{folder}' in parents";
                // List files
                IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;
                bool contains = files.Any(p => p.Name == _fileName);
                return contains;          
                }
                catch
                {
                    return false;
                }
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Załaduj plik backupu";
            openFileDialog1.Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                base.Enabled = false;
                Stream file = openFileDialog1.OpenFile();
                var title = openFileDialog1.FileName.Split('\\').Last();
                await UploadFileAsync(file, title, "application/octet-stream", principalForm.ReadSQL($"SELECT BackupFolderId FROM GoogleDrive", Form1.initialcatalogConnectionString), title);
                if (GetBackupStatus(principalForm.ReadSQL($"SELECT BackupFolderId FROM GoogleDrive", Form1.initialcatalogConnectionString), title))
                {
                    SystemSounds.Hand.Play();
                    MessageBox.Show("Zapis zakończony sukcesem", "Sukces");
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Zapis nie powiódł się", "Błąd");
                }
                base.Enabled = true;
                progressBar1.Value = 0;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @$"{Form1.dataPath}");
        }
    }
}
