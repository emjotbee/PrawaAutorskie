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
            AddItem();
            principalForm.LoadTable($"SELECT Id, Tytuł, Czas, Data, Opis, Plik FROM ListaDziel WHERE Data >= '{DateTime.Now.Year}-{DateTime.Now.Month}-01'");
            principalForm.CalculateSzczegoly();
            principalForm.FilterFill(DateTime.Now.Year,true);
            Close();
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
    }
    }
