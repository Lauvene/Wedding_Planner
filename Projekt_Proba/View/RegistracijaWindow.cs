using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Projekt_Proba.View
{
    public partial class RegistracijaWindow : Form
    {
       string connection = @"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db";
       public RegistracijaWindow()
        {
            InitializeComponent();
        }

       
        private void button2_Click(object sender, EventArgs e) // povratak na login
        {
            this.Hide();
            LoginWindw Login = new LoginWindw();
            Login.Closed += (s, args) => this.Close();
            Login.Show();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e) //registracija
        {
            if (UsernameText.Text == "" || PasswordText.Text == "" || ImeText.Text == "" || PrezimeText.Text == "" || dateTimePicker1.Text == "" || BrojGostiju.Text == "" || BudzetText.Text == "")
            {
                MessageBox.Show("Morate popuniti obavezna polja!");
            }

            else
            {
                using (SQLiteConnection con = new SQLiteConnection(connection))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand("SELECT COUNT(*) FROM [Korisnik] WHERE Username=@username", con);
                    cmd.Parameters.AddWithValue("@username", UsernameText.Text);
                    object result = cmd.ExecuteScalar();
                    int nTables = Convert.ToInt32(result);

                    if (nTables > 0)
                    {
                        MessageBox.Show("Username je zauzet! Pokušajte ponovno");
                    }
                    else
                    {
                        dateTimePicker1.Format = DateTimePickerFormat.Custom;
                        dateTimePicker1.CustomFormat = "YYYY-MM-DD";

                        string query = @"INSERT INTO Korisnik(Ime, Prezime,Datum_Vjencanja,Broj_Gostiju,Budzet, Username, Password ) VALUES ( '" + ImeText.Text + "','" + PrezimeText.Text + "','" + dateTimePicker1.Value.Date + "','" + BrojGostiju.Text + "','" + BudzetText.Text + "','" + UsernameText.Text + "','" + PasswordText.Text + "')";
                        SQLiteCommand cm = new SQLiteCommand(query, con);
                        cm.ExecuteNonQuery();

                        MessageBox.Show("Registracija uspješna!");
                        LoginWindw WP = new LoginWindw();
                        WP.Show();
                        WP.Closed += (s, args) => this.Close();
                    }
                    con.Close();
                }


            }
        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {
            int tester;
            if (!Int32.TryParse(BrojGostiju.Text, out tester))
            {
                MessageBox.Show("Molim upisite broj");
                return;
            }
        }


        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {
            decimal tester;
            int test;
            if (!Decimal.TryParse(BudzetText.Text, out tester) || !Int32.TryParse(BudzetText.Text, out test))
            {
                MessageBox.Show("Molim upisite broj!");
                return;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrezimeText_TextChanged(object sender, EventArgs e)
        {

        }
    }
}