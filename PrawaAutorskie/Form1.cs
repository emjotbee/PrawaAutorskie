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
        private string version = "0.1.4";
        private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static string dataPath = Path.Combine(appDataPath, "PrawaAutorskie");
        private string configFileFullPath = Path.Combine(dataPath, "Config.xml");
        public static string defquery = $"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01' AND Data <= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)}'";

        public Form1()
        {
            InitializeComponent();
            Text = "PrawaAutorskie " + version + " ©2022 Kamil Kłonica";
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
                LoadTable(defquery);
                CalculateSzczegoly();
                FilterFill(DateTime.Now.Year,true);
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
                textBox3.Text = (Convert.ToInt32(textBox2.Text) - Convert.ToInt32(ReadSQL($"SELECT SUM(Czas) as sum_czas FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01' AND Data <= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)}'", initialcatalogConnectionString))).ToString();
                textBox4.Text = ReadSQL($"SELECT COUNT(*) FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01' AND Data <= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)}'", initialcatalogConnectionString);

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
            RemoveDzielo();
            LoadTable(defquery);
            CalculateSzczegoly();
            FilterFill(DateTime.Now.Year,true);
        }
        public void RemoveDzielo()
        {
            try
            {
                if(dataGridView1.CurrentCell != null)
                {
                    int _rowIndex = dataGridView1.CurrentCell.RowIndex;
                    Guid num = (Guid)dataGridView1.Rows[_rowIndex].Cells[0].Value;
                    ExecuteSQLStmt($"DELETE FROM ListaDziel WHERE Id = '{num}'", initialcatalogConnectionString);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Brak dzieła do usunięcia", "Błąd");
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
                if (dataGridView1.CurrentCell != null)
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
                else
                {
                    throw new Exception();
                }                
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Brak pliku do pobrania", "Błąd");
            }
        }
        public static void databaseFileRead(string varID, string varPathToNewLocation)
        {
            try
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
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie pobrać pliku", "Błąd");
            }           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            BazaDanych baza = new BazaDanych();
            baza.Show();
            base.Enabled = false;
        }

        public void FilterFill(int _year,bool _yearchange)
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
                List<int> list2 = new List<int>();
                comboBox2.Items.Clear();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DateTime date = (DateTime)row["Data"];
                    if (!list.Contains(Convert.ToInt32(date.Month)) && date.Year == _year)
                    {

                        list.Add(Convert.ToInt32(date.Month));
                    }
                    if (!list2.Contains(Convert.ToInt32(date.Year)))
                    {

                        list2.Add(Convert.ToInt32(date.Year));
                    }
                    string typPliku = (string)row["Plik"];
                    string last3c = typPliku.Substring(typPliku.LastIndexOf("."));
                    if (!comboBox2.Items.Contains(last3c) && date.Year == _year)
                    {
                        comboBox2.Items.Add(last3c);
                    }
                }
                list.Sort();
                comboBox1.Items.Clear();                
                foreach (int item in list)
                {
                    comboBox1.Items.Add(item);
                }
                if (_yearchange)
                {
                    list2.Sort();
                    comboBox3.Items.Clear();
                    foreach (int item in list2)
                    {
                        comboBox3.Items.Add(item);
                    }
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
            //MessageBox.Show(GetDbSearch(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked));
            string m;
            string p;
            string r;
            if (comboBox3.SelectedIndex > -1)
            {
                r = comboBox3.SelectedItem.ToString();
            }
            else
            {
                r = DateTime.Now.Year.ToString();
            }
            if (comboBox1.SelectedIndex > -1)
            {
                m = comboBox1.SelectedItem.ToString();
            }
            else
            {
                m = "brak";
            }
            if (comboBox2.SelectedIndex > -1)
            {
                p = comboBox2.SelectedItem.ToString();
            }
            else
            {
                p = "";
            }
            if (m == "brak" && comboBox3.SelectedIndex == -1)
            {
                if(textBox5.Text != "Szukaj w bazie danych bez filtrów")
                {
                    comboBox1.SelectedIndex = -1;
                    comboBox2.SelectedIndex = -1;
                    if(!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
                    {
                        SystemSounds.Beep.Play();
                        MessageBox.Show("Przynajmniej jeden obszar wyszukiwania musi być zaznaczony", "Błąd");
                    }
                    else
                    {
                        LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE {GetDbSearch(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked)}");
                    }
                }
                else
                {
                    LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Plik LIKE '%{p}%'");
                }
            }
            else
            {
                if(m == "brak")
                {
                    LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{r}-{1}-01' AND Data <= '{r}-{12}-{DateTime.DaysInMonth(Convert.ToInt32(r), Convert.ToInt32(12))}' AND Plik LIKE '%{p}%'");
                }
                else
                {
                    LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{r}-{m}-01' AND Data <= '{r}-{m}-{DateTime.DaysInMonth(Convert.ToInt32(r), Convert.ToInt32(m))}' AND Plik LIKE '%{p}%'");
                }       
            }
        }

        private void Wyczysc_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            textBox5.Text = "Szukaj w bazie danych bez filtrów";
            LoadTable(defquery);
            FilterFill(DateTime.Now.Year, true);
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
                MessageBox.Show("Plik konfiguracyjny zaktualizowany", "Sukces");
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
                LoadTable(defquery);
                CalculateSzczegoly();
                FilterFill(DateTime.Now.Year, true);
                ResetSearch("Search");
                ResetSearch("Filters");
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
        string GetDbSearch(bool _tytul, bool _opis, bool _plik)
        {
            string _OrAnd(bool _tytul, bool _opis, bool _plik, string _switch)
            {
                switch (_switch)
                {
                    case "OR":
                        if (!_tytul || !_opis)
                        {
                            return "";
                        }
                        else if (!_opis && _tytul)
                        {
                            return "";
                        }
                        else { return "OR"; }

                    case "OR2":
                        if (!_plik)
                        {
                            return "";
                        }
                        else if (!_tytul && !_opis)
                        {
                            return "";
                        }
                        else
                        {
                            return "OR";
                        }
                    default: return "default";
                }                
            }
            string t;
            string o;
            string p;
            if (_tytul)
            {
                t = $"Tytuł LIKE '%{textBox5.Text}%'";
            }
            else
            {
                t = "";
            }
            if (_opis)
            {
                o = $"Opis LIKE '%{textBox5.Text}%'";
            }
            else
            {
                o = "";
            }
            if (_plik)
            {
                p = $"Plik LIKE '%{textBox5.Text}%'";
            }
            else
            {
             p = "";
            }
            string query = $"{t} {_OrAnd(_tytul, _opis, _plik, "OR")} {o} {_OrAnd(_tytul, _opis, _plik, "OR2")} {p}";
            return query;
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            ResetSearch("Filters");
        }
        void ResetSearch(string _switch)
        {
            if(_switch == "Search")
            {
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                textBox5.Text = "Szukaj w bazie danych bez filtrów";
            }
            else
            {
                FilterFill(DateTime.Now.Year, true);
            }
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            ResetSearch("Search");
        }

        private void comboBox2_Click(object sender, EventArgs e)
        {
            ResetSearch("Search");
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            ResetSearch("Search");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterFill(Convert.ToInt32(comboBox3.SelectedItem), false);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            int asc = e.KeyChar;
            if (asc == 13)
            {
                Zastosuj_Click(sender, e);
                e.Handled = true;
            }
        }

        public Guid GetDzieloGuid()
        {
             int _rowIndex = dataGridView1.CurrentCell.RowIndex;
             Guid num = (Guid)dataGridView1.Rows[_rowIndex].Cells[0].Value;
             return num;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DodajDzielo dodajdzielo = new DodajDzielo();
            dodajdzielo.Show();
            base.Enabled = false;
            dodajdzielo.LoadDzielo(GetDzieloGuid());
        }
    }
    }
