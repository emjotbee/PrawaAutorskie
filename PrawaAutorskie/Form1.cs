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
using System.Threading.Tasks;
using System.Collections;
using System.Drawing;
using System.Xml.Linq;
using System.Linq;

namespace PrawaAutorskie
{
    public partial class Form1 : Form
    {
        private Loading load = new Loading();
        private BazaDanych bazad = new BazaDanych();
        public static string masterConnectionString = null;
        public static string initialcatalogConnectionString = null;
        private SqlConnection conn = null;
        private SqlCommand cmd = null;
        private string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        private static string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static string dataPath = Path.Combine(appDataPath, "PrawaAutorskie");
        private string configFileFullPath = Path.Combine(dataPath, "Config.xml");
        public static string defquery = $"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01' AND Data <= '{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)}'";

        public Form1()
        {
            InitializeComponent();
            Text = "PrawaAutorskie " + version + " ©2023 Kamil Kłonica";
        }       
        public bool PrepareDatabase(string connectionString, string dbName)
        {
            int a = 0;
            SqlCommand cmd = null;
            using (var connection = new SqlConnection(connectionString))
            {                
                try
                {
                connection.Open();
                using (cmd = new SqlCommand($"If(db_id(N'{dbName}') IS NULL) BEGIN CREATE DATABASE [{dbName}] ALTER DATABASE [{dbName}] MODIFY FILE(NAME = N'{dbName}', SIZE = 50, MAXSIZE = UNLIMITED, FILEGROWTH = 10%) ALTER DATABASE [{dbName}] MODIFY FILE (NAME = N'{dbName}_log', SIZE = 10, MAXSIZE = UNLIMITED, FILEGROWTH = 1) END", connection))
                {
                        a = cmd.ExecuteNonQuery();
                        connection.ChangeDatabase("PrawaAutorskie");
                        using (cmd = new SqlCommand("IF OBJECT_ID (N'ListaDziel', N'U') IS NULL CREATE TABLE ListaDziel" + "(Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID()," + "Tytuł VARCHAR(255), Opis TEXT, Czas INTEGER, Data DATE, PlikDirect VARBINARY(MAX), Plik VARCHAR(255));IF OBJECT_ID(N'Backups', N'U') IS NULL CREATE TABLE Backups" + "(Nazwa VARCHAR(255))", connection))
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
            try
            {
                CreateConfigXML();
                if (bazad.CheckSQLConnection(masterConnectionString))
                {
                    PrepareDatabase(masterConnectionString, "PrawaAutorskie");
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    LoadTable(defquery, true);
                    CalculateSzczegoly(DateTime.Now.Month);
                    FilterFill(DateTime.Now.Year, true);
                    GetData();
                    //SetTextBoxCompleteSource(textBox5, LoadTable($"SELECT Plik FROM ListaDziel", false));
                    this.WindowState = FormWindowState.Minimized;
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("Brak połączenia z bazą danych. Sprawdź poprawność connection stringa", "Błąd");
                    BazaDanych baza = new BazaDanych();
                    baza.Show();
                    baza.TopMost = true;
                    base.Enabled = false;
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Brak połączenia z bazą danych. Sprawdź poprawność connection stringa", "Błąd");
                BazaDanych baza = new BazaDanych();
                baza.Show();
                baza.TopMost = true;
                base.Enabled = false;
            }            
        }

        private void DodajDzielo_Click(object sender, EventArgs e)
        {
            DodajDzielo dodajdzielo = new DodajDzielo();
            dodajdzielo.Show();
            base.Enabled = false;
        }

        void CreateChart(double[] dataX, double[] dataY)
        {
            formsPlot1.Plot.Clear();
            formsPlot1.Plot.XLabel("Miesiąc");
            formsPlot1.Plot.YLabel("Liczba dzieł");
            formsPlot1.Plot.Title("Ilość dzieł w miesiącach");

            formsPlot1.Plot.AddScatter(dataX, dataY);
            formsPlot1.Refresh();
        }

        public void GetData()
        {
            List<ChartData> chartList = new List<ChartData>();
            ChartData chartData = new ChartData();
            chartList = chartData.GetChartData();

            double[] dataX = new double[chartList.Count];
            double[] dataY = new double[chartList.Count];

            int i = 0;
            foreach (ChartData data in chartList)
            {
                dataX[i] = data.Month;
                dataY[i] = data.IloscDziel;
                i++;
            }
            CreateChart(dataX, dataY);
        }


        public void CalculateSzczegoly(int _month)
        {
            try
            {
                comboBox4.SelectedIndex = _month -1;
                textBox1.Text = "70";
                textBox2.Text = ((160 - (GetDniWolne(_month) * 8)) * Convert.ToInt32(textBox1.Text) / 100).ToString();
                textBox3.Text = (Convert.ToInt32(textBox2.Text) - Convert.ToInt32(ReadSQL($"SELECT SUM(Czas) as sum_czas FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{_month}-01' AND Data <= '{DateTime.Now.Year}-{_month}-{DateTime.DaysInMonth(DateTime.Now.Year, _month)}'", initialcatalogConnectionString))).ToString();
                textBox4.Text = ReadSQL($"SELECT COUNT(*) FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{_month}-01' AND Data <= '{DateTime.Now.Year}-{_month}-{DateTime.DaysInMonth(DateTime.Now.Year, _month)}'", initialcatalogConnectionString);
                textBox6.Text = ReadSQL($"SELECT COUNT(*) FROM ListaDziel", initialcatalogConnectionString);
                textBox7.Text = ReadSQL($"SELECT SUM(Czas) as sum_czas FROM ListaDziel", initialcatalogConnectionString);

            }
            catch
            {
                textBox1.Text = "70";
                textBox2.Text = (((160 * Convert.ToInt32(textBox1.Text) / 100)) - (GetDniWolne(_month) * 8)).ToString();
                textBox3.Text = textBox2.Text;
                textBox4.Text = "0";
                textBox6.Text = ReadSQL($"SELECT COUNT(*) FROM ListaDziel", initialcatalogConnectionString);
                textBox7.Text = ReadSQL($"SELECT SUM(Czas) as sum_czas FROM ListaDziel", initialcatalogConnectionString);
            }
            if (GetDniWolne(_month) > 0)
            {
                textBox2.Font = new Font(textBox2.Font, FontStyle.Bold);
                textBox3.Font = new Font(textBox3.Font, FontStyle.Bold);
            }
            else
            {
                textBox2.Font = new Font(textBox2.Font, FontStyle.Regular);
                textBox3.Font = new Font(textBox3.Font, FontStyle.Regular);
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

        public DataTable LoadTable(string _query, bool _changePrincipal)
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
                if (_changePrincipal)
                    {
                        dataGridView1.Columns.Clear();
                        dataGridView1.DataSource = ds.Tables["ListaDziel"].DefaultView;
                        dataGridView1.Columns[0].Visible = false;
                        dataGridView1.AutoResizeColumns();
                    }
                DataTable dziela = ds.Tables["ListaDziel"];
                ColourResults();
                return dziela;
                //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime data = RemoveDzielo();
            LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{data.Month}-01' AND Data <= '{DateTime.Now.Year}-{data.Month}-{DateTime.DaysInMonth(DateTime.Now.Year, data.Month)}'", true);
            CalculateSzczegoly(data.Month);
            FilterFill(DateTime.Now.Year,true);
            GetData();
        }
        public DateTime RemoveDzielo()
        {
            try
            {
                if(dataGridView1.CurrentCell != null)
                {
                    int _rowIndex = dataGridView1.CurrentCell.RowIndex;
                    DateTime data = (DateTime)dataGridView1.Rows[_rowIndex].Cells[3].Value;
                    Guid num = (Guid)dataGridView1.Rows[_rowIndex].Cells[0].Value;
                    ExecuteSQLStmt($"DELETE FROM ListaDziel WHERE Id = '{num}'", initialcatalogConnectionString);
                    return data;
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
                return DateTime.Now;
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
                    if(comboBox3.SelectedIndex == -1)
                    {
                        if (!comboBox2.Items.Contains(last3c))
                        {
                            comboBox2.Items.Add(last3c);
                        }
                    }
                    else
                    {
                        if (!comboBox2.Items.Contains(last3c) && date.Year == _year)
                        {
                            comboBox2.Items.Add(last3c);
                        }
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
            try
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
                    if (textBox5.Text != "Szukaj w bazie danych bez filtrów")
                    {
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                        if (!checkBox1.Checked && !checkBox2.Checked && !checkBox3.Checked)
                        {
                            SystemSounds.Beep.Play();
                            MessageBox.Show("Przynajmniej jeden obszar wyszukiwania musi być zaznaczony", "Błąd");
                        }
                        else
                        {
                            LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE {GetDbSearch(checkBox1.Checked, checkBox2.Checked, checkBox3.Checked)}",true);
                        }
                    }
                    else
                    {
                        LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Plik LIKE '%{p}%'", true);
                    }
                }
                else
                {
                    if (m == "brak")
                    {
                        LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{r}-{1}-01' AND Data <= '{r}-{12}-{DateTime.DaysInMonth(Convert.ToInt32(r), Convert.ToInt32(12))}' AND Plik LIKE '%{p}%'", true);
                    }
                    else
                    {
                        LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{r}-{m}-01' AND Data <= '{r}-{m}-{DateTime.DaysInMonth(Convert.ToInt32(r), Convert.ToInt32(m))}' AND Plik LIKE '%{p}%'", true);
                    }
                }
            }
            catch
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("Nie można wykonać zapytania", "Błąd");
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
            LoadTable(defquery, true);
            FilterFill(DateTime.Now.Year, true);
            comboBox4.SelectedIndex = DateTime.Now.Month - 1;
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
                            wb.Worksheets.Add(LoadTable($"SELECT Id, Tytuł, Data, Opis, Plik FROM ListaDziel", true), "Dzieła");
                            wb.SaveAs(Path.Combine(dlg.SelectedPath,"Raport.xlsx"));
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
                LoadTable(defquery, true);
                CalculateSzczegoly(DateTime.Now.Month);
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
            }else if(asc == 27)
            {
                Wyczysc_Click(sender, e);
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

        void ColourResults()
        {
            foreach (DataGridViewRow item2 in (IEnumerable)dataGridView1.Rows)
            {
                string plik = item2.Cells["Plik"].Value.ToString();
                string last3c = plik.Substring(plik.LastIndexOf("."));
                if (last3c == ".ps1")
                {
                    item2.DefaultCellStyle.BackColor = Color.LightYellow;
                }
                if (last3c == ".zip")
                {
                    item2.DefaultCellStyle.BackColor = Color.LightGreen;
                }
                if (last3c == ".docx")
                {
                    item2.DefaultCellStyle.BackColor = Color.LightSkyBlue;
                }
            }
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ColourResults();
        }

        int GetDniWolne(int _month)
        {
            IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
            {
                for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                    yield return day;
            }
            int daysDed = 0;
            int daysFree = 0;
            try
            {

                string dataPath = Path.Combine(appDataPath, "UrlopyDelegacje");
                string urlopyFileFullPath = Path.Combine(dataPath, DateTime.Now.Year + "Urlopy.xml");
                if (File.Exists(urlopyFileFullPath))
                {
                    pictureBox1.BackColor = Color.LightGreen;
                    var doc = XDocument.Load(urlopyFileFullPath);

                    var urlopy = doc.Root
                        .Descendants("Urlop")
                        .Select(node => new Urlop
                        {
                            Od = Convert.ToDateTime(node.Element("Od").Value),
                            Do = Convert.ToDateTime(node.Element("Do").Value),
                            Delegacja = Convert.ToBoolean(node.Element("Delegacja").Value),
                            DniIlosc = Convert.ToInt32(node.Element("DniIlosc").Value),
                        })
                        .ToList();
                            foreach(Urlop url in urlopy)
                            {
                                if (!url.Delegacja)
                                {
                                    if (url.Od.Month == _month || url.Do.Month == _month)
                                    {
                                        foreach (DateTime day in EachDay(url.Od, url.Do))
                                        {
                                            if (day.Month != _month)
                                            {
                                                if (day.DayOfWeek != DayOfWeek.Saturday && day.DayOfWeek != DayOfWeek.Sunday)
                                                {
                                                    daysDed++;
                                                }

                                            }
                                        }
                                        daysFree += url.DniIlosc;
                                    }
                                }
                            }
                    return daysFree-daysDed;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SystemSounds.Hand.Play();
            MessageBox.Show("Dni wolne w miesiącu " + comboBox4.Text + " : " + GetDniWolne(comboBox4.SelectedIndex + 1).ToString(), "Dane z aplikacji UrlopyDelegacje©");
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateSzczegoly(comboBox4.SelectedIndex+1);
            LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{comboBox4.SelectedIndex + 1}-01' AND Data <= '{DateTime.Now.Year}-{comboBox4.SelectedIndex + 1}-{DateTime.DaysInMonth(DateTime.Now.Year, comboBox4.SelectedIndex + 1)}'", true);
        }

        void SetTextBoxCompleteSource(TextBox _textbox, DataTable _table)
        {
            string[] array = _table.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            AutoCompleteStringCollection allowedTypes = new AutoCompleteStringCollection();
            allowedTypes.AddRange(array);
            _textbox.AutoCompleteCustomSource = allowedTypes;
            _textbox.AutoCompleteMode = AutoCompleteMode.Suggest;
            _textbox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        void CheckAutoComplete()
        {
            if (!checkBox1.Checked && !checkBox2.Checked && checkBox3.Checked)
            {
                SetTextBoxCompleteSource(textBox5, LoadTable($"SELECT Plik FROM ListaDziel", false));
            }
            else
            {
                textBox5.AutoCompleteMode = AutoCompleteMode.None;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckAutoComplete();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            CheckAutoComplete();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            CheckAutoComplete();
        }
    }

    internal class Urlop
    {
        public DateTime Od { get; set; }
        public DateTime Do { get; set; }
        public bool Delegacja { get; set; }
        public int DniIlosc { get; set; }
    }

    internal class ChartData
    {
        public int Month { get; set; }
        public double IloscDziel { get; set; }

        public List<ChartData> GetChartData()
        {
            Form1 principalForm = System.Windows.Forms.Application.OpenForms.OfType<Form1>().FirstOrDefault();
            List<ChartData> chartList = new List<ChartData>();

            for (int i = 1; i < 13; i++)
            {
                ChartData chartData = new ChartData();
                double iloscDziel = Convert.ToDouble(principalForm.ReadSQL($"SELECT COUNT(*) FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{i}-01' AND Data <= '{DateTime.Now.Year}-{i}-{DateTime.DaysInMonth(DateTime.Now.Year, i)}'", Form1.initialcatalogConnectionString));
                chartData.Month = i;
                chartData.IloscDziel = iloscDziel;
                chartList.Add(chartData);
            }
            return chartList;

        }
    }

}
