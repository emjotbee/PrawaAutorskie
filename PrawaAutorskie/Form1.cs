using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using System.Media;
using DataTable = System.Data.DataTable;
using ClosedXML.Excel;
using System.Collections.Generic;

namespace PrawaAutorskie
{
    public partial class Form1 : Form
    {
        private BazaDanych bazad = new BazaDanych();
        public static string masterConnectionString = null;
        public static string initialcatalogConnectionString = null;
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private string version = "0.0.9";
        private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string dataPath = Path.Combine(appDataPath, "PrawaAutorskie");
        private string configFileFullPath = Path.Combine(dataPath, "Config.xml");

        public Form1()
        {
            InitializeComponent();
            Text = "PrawaAutorskie " + version + " ©2021 Kamil Kłonica";
        }       
        public bool PrepareDatabase(string connectionString, string dbName)
        {
            int a = 0;
            Directory.CreateDirectory($"C:\\{dbName}");
            SqlCommand cmd = null;
            using (var connection = new SqlConnection(connectionString))
            {                
                try
                {
                connection.Open();
                using (cmd = new SqlCommand($"If(db_id(N'{dbName}') IS NULL) CREATE DATABASE [{dbName}] ON PRIMARY" + $"(Name={dbName}, filename = 'C:\\{dbName}\\{dbName}.mdf', size=50," + "maxsize=100, filegrowth=10%)log on" + $"(name={dbName}_log, filename='C:\\{dbName}\\{dbName}.ldf',size=3," + "maxsize=20,filegrowth=1)", connection))
                {
                        a = cmd.ExecuteNonQuery();
                        connection.ChangeDatabase("PrawaAutorskie");
                        using (cmd = new SqlCommand("IF OBJECT_ID (N'ListaDziel', N'U') IS NULL CREATE TABLE ListaDziel" + "(Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID()," + "Tytuł VARCHAR(255), Opis TEXT, Czas INTEGER, Data DATE, PlikDirect VARBINARY(MAX), Plik VARCHAR(255))", connection))
                        {
                                cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (SqlException ae)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show(ae.Message.ToString(), "Błąd");
                }
            }
            if(a == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool ExecuteSQLStmt(string sql,string _connectionstring)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                    // Create a connection  
                    conn = new SqlConnection(_connectionstring);
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                    conn.ConnectionString = _connectionstring;
                    conn.Open();
                    cmd = new SqlCommand(sql, conn);
                    int a = cmd.ExecuteNonQuery();
                if (a == 0)
                {
                    Cursor.Current = Cursors.Default;
                    return false;
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    return true;
                }
            }
            catch (Exception ae)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ae.Message.ToString(), "Błąd");
                Cursor.Current = Cursors.Default;
                return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateConfigXML();
            if (bazad.CheckSQLConnection(masterConnectionString))
            {
                PrepareDatabase(masterConnectionString, "PrawaAutorskie");
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01'");
                CalculateSzczegoly();
                FilterFill();
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Brak połączenia z bazą danych. Sprawdź poprawność connection stringa", "Błąd");
                BazaDanych baza = new BazaDanych();
                baza.Show();
                base.Enabled = false;
            }
        }

        private void DodajDzielo_Click(object sender, EventArgs e)
        {
            DodajDzielo dodajdzielo = new DodajDzielo();
            dodajdzielo.Show();
            base.Enabled = false;
        }

        public void CalculateSzczegoly()
        {
            try
            {
                textBox1.Text = "70";
                textBox2.Text = (160 * Convert.ToInt32(textBox1.Text) / 100).ToString();
                textBox3.Text = (Convert.ToInt32(textBox2.Text) - Convert.ToInt32(ReadSQL($"SELECT SUM(Czas) as sum_czas FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01'", initialcatalogConnectionString))).ToString();
                textBox4.Text = ReadSQL($"SELECT COUNT(*) FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01'", initialcatalogConnectionString);

            }
            catch
            {
                textBox1.Text = "70";
                textBox2.Text = (160 * Convert.ToInt32(textBox1.Text) / 100).ToString();
                textBox3.Text = textBox2.Text;
                textBox4.Text = "0";
            }
        }

        public string ReadSQL(string _statement, string _connection)
        {
            //Declare the SqlDataReader
            SqlDataReader rdr = null;
            string field1 = null;
            try
            {
                //Create connection
                SqlConnection conn = new SqlConnection(_connection);

                //Create command
                SqlCommand cmd = new SqlCommand(_statement, conn);
                //Open the connection
                conn.Open();

                // 1. get an instance of the SqlDataReader
                rdr = cmd.ExecuteReader();


                while (rdr.Read())
                {
                    // get the results of each column
                    field1 = rdr.GetValue(0).ToString();
                }
                // 3. close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // close the connection
                if (conn != null)
                {
                    conn.Close();
                }
                return field1;
            }
            catch (Exception ae)
            {
                // 3. close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }
                // close the connection
                if (conn != null)
                {
                    conn.Close();

                }
                return ae.Message.ToString();
            }
        }

        public DataTable LoadTable(string _query)
        {
            // Create a connection  
            conn = new SqlConnection(initialcatalogConnectionString);
            // Open the connection  
            if (conn.State == ConnectionState.Open)
                conn.Close();
            conn.ConnectionString = (initialcatalogConnectionString);
            conn.Open();
            // Create a data adapter  
            SqlDataAdapter da = new SqlDataAdapter
            (_query, conn);
            // Create DataSet, fill it and view in data grid  
            DataSet ds = new DataSet("ListaDziel");
            da.Fill(ds, "ListaDziel");
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = ds.Tables["ListaDziel"].DefaultView;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.AutoResizeColumns();
            DataTable dziela = ds.Tables["ListaDziel"];
            return dziela;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoveUrlop();
            LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01'");
            CalculateSzczegoly();
            FilterFill();
        }
        public void RemoveUrlop()
        {
            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                Guid num = (Guid)dataGridView1.Rows[rowIndex].Cells[0].Value;
                ExecuteSQLStmt($"DELETE FROM ListaDziel WHERE Id = '{num}'", initialcatalogConnectionString);
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Brak urlopu do usunięcia", "Błąd");
            }
        }

        public void CreateConfigXML()
        {
            try
            {
                if (File.Exists(configFileFullPath))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(configFileFullPath);
                    XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("connectionstring");
                    foreach (XmlElement item in elementsByTagName)
                    {

                        masterConnectionString = item.InnerText;
                        initialcatalogConnectionString = item.GetAttribute("InitialCatalog");
                    }
                }
                else
                {
                    Directory.CreateDirectory(dataPath);
                    XmlDocument xmlDocument2 = new XmlDocument();
                    xmlDocument2.LoadXml("<config></config>");
                    XmlElement xmlElement3 = xmlDocument2.CreateElement("connectionstring");
                    XmlAttribute xmlAttribute2 = xmlDocument2.CreateAttribute("InitialCatalog");
                    xmlAttribute2.Value = @"Server=localhost\SQLEXPRESS;Initial Catalog=PrawaAutorskie;User Id=PrawaAutorskie;Password=Pa$$w0rd1;";
                    xmlElement3.Attributes.Append(xmlAttribute2);
                    xmlElement3.InnerText = @"Server=localhost\SQLEXPRESS;Database=master;User Id=PrawaAutorskie;Password=Pa$$w0rd1;";
                    xmlDocument2.DocumentElement.AppendChild(xmlElement3);
                    xmlDocument2.PreserveWhitespace = true;
                    xmlDocument2.Save(configFileFullPath);
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(configFileFullPath);
                    XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("connectionstring");
                    foreach (XmlElement item in elementsByTagName)
                    {

                        masterConnectionString = item.InnerText;
                        initialcatalogConnectionString = item.GetAttribute("InitialCatalog");
                    }
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Błąd podczas tworzenia pliku konfiguracyjnego", "Błąd");
            }
        }

        private void Pobierz_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                Guid num = (Guid)dataGridView1.Rows[rowIndex].Cells[0].Value;
                using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                {
                    dlg.Description = "Wybierz folder";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        databaseFileRead(num.ToString(), Path.Combine(dlg.SelectedPath, dataGridView1.Rows[rowIndex].Cells[5].Value.ToString()));
                        SystemSounds.Hand.Play();
                        MessageBox.Show("Plik zapisany pomyślnie", "Sukces");
                    }
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie można zapisać pliku", "Błąd");
            }
        }
        public static void databaseFileRead(string varID, string varPathToNewLocation)
        {
            using (var varConnection = new SqlConnection(initialcatalogConnectionString))
            using (var sqlQuery = new SqlCommand(@"SELECT PlikDirect FROM ListaDziel WHERE Id = @varID", varConnection))
            {
                varConnection.Open();
                sqlQuery.Parameters.AddWithValue("@varID", varID);
                using (var sqlQueryResult = sqlQuery.ExecuteReader())
                    if (sqlQueryResult != null)
                    {
                        sqlQueryResult.Read();
                        var blob = new Byte[(sqlQueryResult.GetBytes(0, 0, null, 0, int.MaxValue))];
                        sqlQueryResult.GetBytes(0, 0, blob, 0, blob.Length);
                        using (var fs = new FileStream(varPathToNewLocation, FileMode.Create, FileAccess.Write))
                        fs.Write(blob, 0, blob.Length);
                    }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            BazaDanych baza = new BazaDanych();
            baza.Show();
            base.Enabled = false;
        }

        public void FilterFill()
        {
            try
            {
                // Create a connection  
                conn = new SqlConnection(initialcatalogConnectionString);
                // Open the connection  
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.ConnectionString = (initialcatalogConnectionString);
                conn.Open();
                // Create a data adapter  
                SqlDataAdapter da = new SqlDataAdapter
                ($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel", conn);
                // Create DataSet, fill it and view in data grid  
                DataSet ds = new DataSet("ListaDziel");
                da.Fill(ds, "ListaDziel");
                List<int> list = new List<int>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DateTime date = (DateTime)row["Data"];
                    if (!list.Contains(Convert.ToInt32(date.Month)))
                    {

                        list.Add(Convert.ToInt32(date.Month));
                    }                   
                    string typPliku = (string)row["Plik"];
                    string last3c = typPliku.Substring(typPliku.LastIndexOf("."));
                    if (!comboBox2.Items.Contains(last3c))
                    {
                        comboBox2.Items.Add(last3c);
                    }
                }
                list.Sort();
                foreach (int item in list)
                {
                    comboBox1.Items.Add(item);
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie załadować listy", "Błąd");
            }
        }

        private void Zastosuj_Click(object sender, EventArgs e)
        {
            string m;
            string p;
            if (comboBox1.SelectedIndex > -1)
            {
                m = comboBox1.SelectedItem.ToString();
            }
            else
            {
                m = DateTime.Now.Month.ToString();
            }
            if (comboBox2.SelectedIndex > -1)
            {
                p = comboBox2.SelectedItem.ToString();
            }
            else
            {
                p = "";
            }
            LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{m}-01' AND Data < '{DateTime.Now.Year}-{m}-{DateTime.DaysInMonth(DateTime.Now.Year, Convert.ToInt32(m))}' AND Plik LIKE '%{p}%'");
        }

        private void Wyczysc_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01'");
        }


        private void Raport_Click(object sender, EventArgs e)
        {
            //Exporting to Excel
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                try
                {
                    dlg.Description = "Wybierz folder";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        using (XLWorkbook wb = new XLWorkbook())
                        {
                            wb.Worksheets.Add(LoadTable($"SELECT Id, Tytuł, Data, Opis, Plik FROM ListaDziel"), "Dzieła");
                            wb.SaveAs(Path.Combine(dlg.SelectedPath,"Dziela.xlsx"));
                            foreach(DataGridViewRow row in dataGridView1.Rows)
                            {
                                databaseFileRead(dataGridView1.Rows[row.Index].Cells[0].Value.ToString(), Path.Combine(dlg.SelectedPath, dataGridView1.Rows[row.Index].Cells[4].Value.ToString()));
                            }
                            SystemSounds.Hand.Play();
                            MessageBox.Show("Pliki zapisane pomyślnie", "Sukces");
                            Cursor.Current = Cursors.Default;
                        }
                    }
                }
                catch(Exception ex)
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show(ex.Message.ToString(), "Błąd");
                    Cursor.Current = Cursors.Default;
                }
            }

        }
        public void UpdateConfigXML(string _connstr,string _switch)
        {
            try
            {
                Directory.CreateDirectory(dataPath);
                XmlDocument xmlDocument2 = new XmlDocument();
                xmlDocument2.LoadXml("<config></config>");
                XmlElement xmlElement3 = xmlDocument2.CreateElement("connectionstring");
                XmlAttribute xmlAttribute2 = xmlDocument2.CreateAttribute("InitialCatalog");
                if(_switch == "initial")
                {
                    xmlAttribute2.Value = $@"{_connstr}";
                    xmlElement3.Attributes.Append(xmlAttribute2);
                    xmlElement3.InnerText = masterConnectionString;
                }
                else
                {
                    xmlAttribute2.Value = initialcatalogConnectionString;
                    xmlElement3.Attributes.Append(xmlAttribute2);
                    xmlElement3.InnerText = $@"{_connstr}";
                }
                xmlDocument2.DocumentElement.AppendChild(xmlElement3);
                xmlDocument2.PreserveWhitespace = true;
                xmlDocument2.Save(configFileFullPath);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(configFileFullPath);
                XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("connectionstring");
                foreach (XmlElement item in elementsByTagName)
                {

                    masterConnectionString = item.InnerText;
                    initialcatalogConnectionString = item.GetAttribute("InitialCatalog");
                }
                SystemSounds.Hand.Play();
                MessageBox.Show("Plik konfiguracyjny zauktualizowany", "Sukces");
            }
            catch
            {

                SystemSounds.Beep.Play();
                MessageBox.Show("Błąd podczas aktualizacji pliku konfiguracyjnego", "Błąd");
            }
        }

        public void CheckConn()
        {
            if(!bazad.CheckSQLConnection(masterConnectionString) || !bazad.CheckSQLConnection(initialcatalogConnectionString))
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Brak połączenia z bazą danych. Sprawdź poprawność connection stringa", "Błąd");
                ButtonsEnabled(false);
            }
            else if(bazad.CheckSQLConnection(masterConnectionString) && bazad.CheckSQLConnection(initialcatalogConnectionString))
            {
                ButtonsEnabled(true);
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01'");
                CalculateSzczegoly();
                FilterFill();
            }
        }
        void ButtonsEnabled(bool _switch)
        {
            if (_switch)
            {
                DodajDzielo.Enabled = true;
                button2.Enabled = true;
                Pobierz.Enabled = true;
                Raport.Enabled = true;
                Zastosuj.Enabled = true;
                Wyczysc.Enabled = true;
            }
            else
            {
                DodajDzielo.Enabled = false;
                button2.Enabled = false;
                Pobierz.Enabled = false;
                Raport.Enabled = false;
                Zastosuj.Enabled = false;
                Wyczysc.Enabled = false;
            }
        }
    }
    }

