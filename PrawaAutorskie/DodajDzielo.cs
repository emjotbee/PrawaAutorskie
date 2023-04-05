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
using System.IO;
using System.Media;

namespace PrawaAutorskie
{
    public partial class DodajDzielo : Form
    {
        private Form1 principalForm = System.Windows.Forms.Application.OpenForms.OfType<Form1>().FirstOrDefault();
        public DodajDzielo()
        {
            InitializeComponent();
        }

        void AddItem()
        {
            try
            {
                SqlCommand cmd = null;
                string connectionString = Form1.initialcatalogConnectionString;
                byte[] file;
                using (var stream = new FileStream(dataGridView1.Rows[0].Cells[0].Value.ToString(), FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        file = reader.ReadBytes((int)stream.Length);
                    }
                }
                using (var connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (cmd = new SqlCommand("INSERT INTO ListaDziel(Id, Tytuł, Czas, Data, Opis, PlikDirect, Plik) " + $"VALUES (default, '{textBox1.Text}',{textBox2.Text}, '{dateTimePicker1.Value}','{richTextBox1.Text}',@File,'{dataGridView1.Rows[0].Cells[1].Value}') ", connection))
                        {
                            cmd.Parameters.Add("@File", SqlDbType.VarBinary, file.Length).Value = file;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (SqlException ae)
                    {
                        SystemSounds.Hand.Play();
                        MessageBox.Show(ae.Message.ToString(), "Błąd");
                    }
                }
            }
            catch
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Nie można dodać dzieła", "Błąd");
            }
            
            }

        private void Dodaj_Click(object sender, EventArgs e)
        {
            if(Dodaj.Text == "Dodaj")
            {
                AddItem();
                principalForm.LoadTable(Form1.defquery, true);
                principalForm.CalculateSzczegoly();
                principalForm.FilterFill(DateTime.Now.Year, true);
                Close();
            }
            else
            {
                UpdateItem(principalForm.GetDzieloGuid());
                principalForm.LoadTable(Form1.defquery, true);
                principalForm.CalculateSzczegoly();
                principalForm.FilterFill(DateTime.Now.Year, true);
                Close();
            }
        }

        private void dodajplik_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Załącz plik";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.Rows.Add(openFileDialog1.FileName, Path.GetFileName(openFileDialog1.FileName));
                    Dodaj.Enabled = true;
                }
                catch
                {
                    SystemSounds.Hand.Play();
                    MessageBox.Show("Błąd podczas dodawania wniosku", "Błąd");
                }
            }
        }

        private void DodajDzielo_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnCount = 2;
            dataGridView1.Columns[0].Name = "Plik";
            dataGridView1.Columns[1].Name = "Nazwa";
        }

        private void DodajDzielo_FormClosing(object sender, FormClosingEventArgs e)
        {
            principalForm.Enabled = true;
        }

        public void LoadDzielo(Guid _guid)
        {
            Dodaj.Text = "Zapisz";
            Form.ActiveForm.Text = "Aktualizuj dzieło";
            try
            {
                textBox1.Text = principalForm.ReadSQL($"SELECT Tytuł FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString);
                textBox2.Text = principalForm.ReadSQL($"SELECT Czas FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString);
                dateTimePicker1.Value = Convert.ToDateTime(principalForm.ReadSQL($"SELECT Data FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString));
                richTextBox1.Text = principalForm.ReadSQL($"SELECT Opis FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString);
                dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add("baza danych", principalForm.ReadSQL($"SELECT Plik FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString));
                Dodaj.Enabled = true;
                //FillPodobne(_guid);
            }
            catch
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Błąd podczas ładowania dzieła", "Błąd");
            }
            FillPodobne(_guid);
        }

        private void UpdateItem(Guid _guid)
        {
            if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "baza danych")
            {
                principalForm.ExecuteSQLStmt($"UPDATE ListaDziel SET Tytuł='{textBox1.Text}', Czas ='{textBox2.Text}',Data ='{dateTimePicker1.Value}',Opis ='{richTextBox1.Text}' WHERE Id ='{_guid}'", Form1.initialcatalogConnectionString);
            }
            else
            {
                try
                {
                    SqlCommand cmd = null;
                    string connectionString = Form1.initialcatalogConnectionString;
                    byte[] file;
                    using (var stream = new FileStream(dataGridView1.Rows[0].Cells[0].Value.ToString(), FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            file = reader.ReadBytes((int)stream.Length);
                        }
                    }
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            using (cmd = new SqlCommand($"UPDATE ListaDziel SET Tytuł='{textBox1.Text}', Czas ='{textBox2.Text}',Data ='{dateTimePicker1.Value}',Opis ='{richTextBox1.Text}',PlikDirect =@File,Plik ='{dataGridView1.Rows[0].Cells[1].Value}' WHERE Id ='{_guid}'", connection))
                            {
                                cmd.Parameters.Add("@File", SqlDbType.VarBinary, file.Length).Value = file;
                                cmd.ExecuteNonQuery();
                            }
                        }
                        catch (SqlException ae)
                        {
                            SystemSounds.Hand.Play();
                            MessageBox.Show(ae.Message.ToString(), "Błąd");
                        }
                    }
                }
                catch
                {
                    SystemSounds.Hand.Play();
                    MessageBox.Show("Nie można dodać dzieła", "Błąd");
                }
            }          
        }

        void FillPodobne(Guid _guid)
        {
            string dzieloName;
            string dzieloNameRaw = (principalForm.ReadSQL($"SELECT Plik FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString));
            if(dzieloNameRaw.Contains("_"))
            {
                dzieloName = (principalForm.ReadSQL($"SELECT Plik FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString)).Split('_')[0];
            }
            else
            {
                dzieloName = (principalForm.ReadSQL($"SELECT Plik FROM ListaDziel WHERE Id = '{_guid}'", Form1.initialcatalogConnectionString)).Split('.')[0];
            }
            DataTable podobne = principalForm.LoadTable($"SELECT Id, Plik, Opis FROM ListaDziel WHERE Plik LIKE '%{dzieloName}%' AND Id <> '{_guid}'", false);
            if(podobne.Rows.Count > 0)
            {
                dataGridView2.Columns.Clear();
                dataGridView2.DataSource = podobne.DefaultView;
                dataGridView2.Columns[0].Visible = false;
                dataGridView2.AutoResizeColumns();
            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rowIndex = dataGridView2.CurrentCell.RowIndex;
                Guid num = (Guid)dataGridView2.Rows[rowIndex].Cells[0].Value;             
                LoadDzielo(num);
            }
            catch
            {
                SystemSounds.Hand.Play();
                MessageBox.Show("Nie można otworzyć dzieła", "Błąd");
            }
        }
    }
    }
