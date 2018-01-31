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
    public partial class TroskovnikWindow : Form
    {
        public TroskovnikWindow()
        {
            InitializeComponent();
        }

        public SQLiteConnection cn = new SQLiteConnection(@"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db");
        public static double ukupno { get; set; }
        public static double budzet { get; set; }

        int id = LoginWindw.ID_korisnika;

        protected void Ukupni()
        {
            cn.Open();
            ukupno = 0;
            SQLiteCommand cmd = new SQLiteCommand("Select Cijena From Trosak where Id_korisnika=@id", cn);
            cmd.Parameters.AddWithValue("@id", id);
            SQLiteDataAdapter dta = new SQLiteDataAdapter(cmd);
            DataTable data = new DataTable();
            dta.Fill(data);

            foreach (DataRow dr in data.Rows)
            {
                ukupno += Convert.ToDouble(dr["Cijena"]);
                textBox1.Text = ukupno.ToString();
            }
            cn.Close();
            Promjena(ukupno);
        }
        private void Promjena(double ukupno)
        {
            cn.Open();


            SQLiteCommand cm = new SQLiteCommand("Select Budzet from Korisnik where Id_korisnik=@id", cn);
            cm.Parameters.AddWithValue("@id", id);
            object result = cm.ExecuteScalar();
            budzet = Convert.ToDouble(result);
            budzet = budzet - ukupno;
            BudzetText.Text = budzet.ToString();

            cn.Close();
        }

        private void PopuniListu()
        {
            cn.Open();
            listView1.Items.Clear();
            SQLiteCommand cm = new SQLiteCommand("Select * From Trosak where Id_korisnika =@id  Order by tipTroska ASC", cn); // upit koji uzima tablicu trosak ordirano po tipu troska uzlazno
            cm.Parameters.AddWithValue("@id", id);

            try
            {
                SQLiteDataReader dt = cm.ExecuteReader();
                while (dt.Read())
                {
                    ListViewItem ittem = new ListViewItem(dt["Id_trosak"].ToString());
                    ittem.SubItems.Add(dt["tipTroska"].ToString());
                    ittem.SubItems.Add(dt["Opis_troska"].ToString());
                    ittem.SubItems.Add(dt["Cijena"].ToString());

                    listView1.Items.Add(ittem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cn.Close();
        }

        private void DodajuListu()
        {
            cn.Open();

            string Query = "Insert into  Trosak  (tipTroska, Opis_troska, Cijena,Id_korisnika) values('" + this.KategorijaComboText.Text + "','" + this.NapomenaText.Text + "','" + this.IznosText.Text + "','" + this.id + "' ) ";
            if (KategorijaComboText.Text == "" || NapomenaText.Text == "" || IznosText.Text == "")
            {
                MessageBox.Show("Popunite polja!");
            }
            else
            {
                SQLiteCommand cmd = new SQLiteCommand(Query, cn);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Trosak uspješno dodan na listu!", "Important Message");
            }
            cn.Close();

            KategorijaComboText.Text = String.Empty;
            NapomenaText.Text = String.Empty;
            IznosText.Text = String.Empty;
        }

        private void ObrisisListe()
        {
            var Id_trosak = listView1.FocusedItem.Text;

            string query = "delete from Trosak where Id_trosak=@id;";
            try
            {
                cn.Open();

                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    using (SQLiteCommand com = new SQLiteCommand(query, cn, trans))
                    {
                        com.Parameters.AddWithValue("id", Id_trosak);

                        var should_be_one = com.ExecuteNonQuery();
                        if (should_be_one == 1)
                        {
                            trans.Commit();
                        }
                        else
                        {
                            trans.Rollback();
                            throw new Exception("Pokušaj brisanja više redova!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            cn.Close();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }


        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           // string insertQuery = "INSERT " + int.Parse(richTextBox2.Text) + " to ime FROM BazaProjekt.Osoba";

        }

        private void WellcomeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            WeddingPlannerWellcomeWindow WP = new WeddingPlannerWellcomeWindow();
            WP.Closed += (s, args) => this.Close();
            WP.Show();
        }

        private void WellcomeButtonProfil_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProfilWindow PW = new ProfilWindow();
            PW.Closed += (s, args) => this.Close();
            PW.Show();
        }

        private void WellcomeButtonListaGostiju_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListaGostijuWindow LGW = new ListaGostijuWindow();
            LGW.Closed += (s, args) => this.Close();
            LGW.Show();
        }

        private void WellcomeButtonPlanSjedenja_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlanSjedenjaWindow PSW = new PlanSjedenjaWindow();
            PSW.Closed += (s, args) => this.Close();
            PSW.Show();
        }

        private void WellcomeButtonPretrazivanje_Click(object sender, EventArgs e)
        {
            this.Hide();
            PretrazivanjeWindow PRW = new PretrazivanjeWindow();
            PRW.Closed += (s, args) => this.Close();
            PRW.Show();
        }

        private void WellcomeButtonImenik_Click(object sender, EventArgs e)
        {
            this.Hide();
            ImenikWindow IW = new ImenikWindow();
            IW.Closed += (s, args) => this.Close();
            IW.Show();
        }

        private void WellcomeButtonPodsjetnik_Click(object sender, EventArgs e)
        {
            this.Hide();
            PodsjetnikWindow POW = new PodsjetnikWindow();
            POW.Closed += (s, args) => this.Close();
            POW.Show();
        }



        private void BudzetText_TextChanged(object sender, EventArgs e)
        {

        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DodajBaza_Click(object sender, EventArgs e)
        {

            DodajuListu();
            Ukupni();
            PopuniListu();

        }


        private void TroskovnikWindow_Shown(object sender, EventArgs e) // ovaj se dogadaj podize samo jedanput tj. kada se pokaze prozor prvi put, ne reagira na min/max, skrivanje itd.. 
        {
            /////////////////////////////////////// Popuni listu ///////////////////////////////////////////////////////////////////////

            PopuniListu();

            ////////////////////////////////// Ukupni trosak ////////////////////////////////////////////////////////////////////////////

            Ukupni();
            listView1.FullRowSelect = true;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ukupni();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ObrisisListe();
            PopuniListu();
            Ukupni();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
