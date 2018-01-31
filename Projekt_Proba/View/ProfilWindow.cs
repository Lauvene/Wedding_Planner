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
    public partial class ProfilWindow : Form
    {
        public ProfilWindow()
        {
            InitializeComponent();
        }

        int id_korisnika = LoginWindw.ID_korisnika;
        SQLiteConnection cn = new SQLiteConnection(@"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db");


        private void prikaziProfil()
        {
            cn.Open();

            SQLiteCommand cmd1 = new SQLiteCommand("Select * from Korisnik where Id_korisnik=@id", cn);
            cmd1.Parameters.AddWithValue("@id", id_korisnika);

            SQLiteDataReader dt = cmd1.ExecuteReader();

            while (dt.Read())
            {
                bunifuCustomTextbox3.Text = (dt["Ime"].ToString());
                bunifuCustomTextbox5.Text = (dt["Prezime"].ToString());
                bunifuCustomTextbox6.Text = (dt["Email"].ToString());
                bunifuCustomTextbox1.Text = (dt["Datum_vjencanja"].ToString());
                bunifuCustomTextbox2.Text = (dt["Broj_Gostiju"].ToString());


            }
            cn.Close();
        }
        private void IzmjeniProfil()
        {
            cn.Open();
            SQLiteCommand cmd2 = new SQLiteCommand("Update Korisnik set Ime='" + bunifuCustomTextbox3.Text + "', Prezime= '" + bunifuCustomTextbox5.Text + "', Email='" + bunifuCustomTextbox6.Text + "', Datum_vjencanja='" + bunifuCustomTextbox1.Text + "', Broj_gostiju='" + bunifuCustomTextbox2.Text + "'", cn);
            if(bunifuCustomTextbox3.Text=="  "|| bunifuCustomTextbox5.Text=="  "|| bunifuCustomTextbox6.Text=="  "|| bunifuCustomTextbox1.Text=="  " || bunifuCustomTextbox2.Text=="  ")
            {
                MessageBox.Show("Potrebno je popuniti prazna polja!", "Upozorenje!");
            }
            else
            {
                cmd2.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Promjene uspješno spremljene!", "Obavijest!");
                prikaziProfil();
            }



        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            WeddingPlannerWellcomeWindow WP= new WeddingPlannerWellcomeWindow();
            WP.Show();
            WP.Closed += (s, args) => this.Close();
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            TroskovnikWindow TW = new TroskovnikWindow();
            TW.Show();
            TW.Closed += (s, args) => this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListaGostijuWindow LGW = new ListaGostijuWindow();
            LGW.Show();
            LGW.Closed += (s, args) => this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlanSjedenjaWindow PSW = new PlanSjedenjaWindow();
            PSW.Show();
            PSW.Closed += (s, args) => this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            PretrazivanjeWindow PRW = new PretrazivanjeWindow();
            PRW.Show();
            PRW.Closed += (s, args) => this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            PodsjetnikWindow POW = new PodsjetnikWindow();
            POW.Show();
            POW.Closed += (s, args) => this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
            ImenikWindow IW = new ImenikWindow();
            IW.Show();
            IW.Closed += (s, args) => this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void IzmjeniBtn_Click(object sender, EventArgs e)
        {
            IzmjeniProfil();
        }

        private void ProfilWindow_Load(object sender, EventArgs e)
        {
            prikaziProfil();
        }
    }
}
