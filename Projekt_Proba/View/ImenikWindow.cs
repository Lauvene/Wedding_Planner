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
    public partial class ImenikWindow : Form
    {

        public ImenikWindow()
        {
            InitializeComponent();

            
        }

        int id_korisnika = LoginWindw.ID_korisnika;
        SQLiteConnection cn = new SQLiteConnection(@"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db");

        
        private void fillview()
        {
            listView1.Items.Clear();
            cn.Open();
            SQLiteCommand cm = new SQLiteCommand("Select Id_kontakt, Ime, kontakt_telefon, Napomena, Email From Kontakt where Id_korisnika=@id  Order by Ime ASC", cn);
            cm.Parameters.AddWithValue("@id", id_korisnika);
            try
            {
                SQLiteDataReader dt = cm.ExecuteReader();
                while (dt.Read())
                {
                    ListViewItem ittem = new ListViewItem(dt["Id_kontakt"].ToString());
                    ittem.SubItems.Add(dt["Ime"].ToString());
                    ittem.SubItems.Add(dt["kontakt_telefon"].ToString());
                    ittem.SubItems.Add(dt["Napomena"].ToString());
                    ittem.SubItems.Add(dt["Email"].ToString());

                    listView1.Items.Add(ittem);
                }
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void ObrisisListe()
        {

            var Id_kontakt = listView1.FocusedItem.Text;

            string query = "Delete from Kontakt where Id_kontakt=@id;";


            try
            {
                cn.Open();

                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    using (SQLiteCommand com = new SQLiteCommand(query, cn, trans))
                    {

                        com.Parameters.AddWithValue("id", Id_kontakt);

                        var should_be_one = com.ExecuteNonQuery();

                        if (should_be_one == 1)
                        {

                            trans.Commit();
                            MessageBox.Show("Kontakt je obrisan!", "Important Message");

                        }
                        else
                        {

                            trans.Rollback();

                            throw new Exception("An attempt to delete multiple rows was detected");
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


        private void DodajuListu()
        {
            cn.Open();
            string Query = "Insert into Kontakt (Ime, kontakt_telefon, Napomena, Email,Id_korisnika) values('" + this.textBox1.Text + "','" + this.textBox2.Text + "','" + this.textBox4.Text + "','" + this.textBox3.Text + "', '"+id_korisnika+"')";
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Popunite obavezna polja!");
            }
            else
            {
                SQLiteCommand cmd = new SQLiteCommand(Query, cn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Gost je dodan u listu!", "Important Message");
            }
            cn.Close();


            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox3.Text = String.Empty;
        }


        private void WellcomeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            WeddingPlannerWellcomeWindow WP = new WeddingPlannerWellcomeWindow();
            WP.Closed += (s, args) => this.Close();
            WP.Show();
        }

        private void WellcomeButtonProfil_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ProfilWindow PW = new ProfilWindow();
            PW.Closed += (s, args) => this.Close();
            PW.Show();
        }

        private void WellcomeButtonTroskovnik_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            TroskovnikWindow TW = new TroskovnikWindow();
            TW.Closed += (s, args) => this.Close();
            TW.Show();
        }

        private void WellcomeButtonListaGostiju_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ListaGostijuWindow LGW = new ListaGostijuWindow();
            LGW.Closed += (s, args) => this.Close();
            LGW.Show();

        }

        private void WellcomeButtonPlanSjedenja_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            PlanSjedenjaWindow PSW = new PlanSjedenjaWindow();
            PSW.Closed += (s, args) => this.Close();
            PSW.Show();
        }

        private void WellcomeButtonPretrazivanje_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            PretrazivanjeWindow PRW = new PretrazivanjeWindow();
            PRW.Closed += (s, args) => this.Close();
            PRW.Show();
        }

    
        private void WellcomeButtonPodsjetnik_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            PodsjetnikWindow POW = new PodsjetnikWindow();
            POW.Closed += (s, args) => this.Close();
            POW.Show();
        }



        private void button1_Click(object sender, EventArgs e)

        {
            DodajuListu();
            fillview();
        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ObrisisListe();
            fillview();
        }

        private void ImenikWindow_Shown(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;
        }

        private void ImenikWindow_Load(object sender, EventArgs e)
        {
            fillview();
            listView1.FullRowSelect = true;
        }
    }
}
