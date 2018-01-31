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
    public partial class PodsjetnikWindow : Form
    {
        public PodsjetnikWindow()
        {
            InitializeComponent();

            comboBox1.Items.Add("Visok");
            comboBox1.Items.Add("Obican");
            comboBox1.Items.Add("Nizak");

            
        }

        int id_korisnika = LoginWindw.ID_korisnika;
        SQLiteConnection cn = new SQLiteConnection(@"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db");


        private void fillview()
        {
            listView1.Items.Clear();
            cn.Open();
            SQLiteCommand cm = new SQLiteCommand("Select Id_pod, Naziv, Datum, Prioritet From Podsjetnik where Id_korisnika=@id  Order by Datum ASC", cn);
            cm.Parameters.AddWithValue("@id", id_korisnika);
            try
            {
                SQLiteDataReader dt = cm.ExecuteReader();
                while (dt.Read())
                {
                    ListViewItem ittem = new ListViewItem(dt["Id_pod"].ToString());
                    ittem.SubItems.Add(dt["Naziv"].ToString());
                    ittem.SubItems.Add(dt["Datum"].ToString());
                    ittem.SubItems.Add(dt["Prioritet"].ToString());

                    listView1.Items.Add(ittem);
                }
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void DodajuListu()
        {
            cn.Open();
            string Query = "Insert into Podsjetnik (Naziv, Datum, Prioritet, Id_korisnika) values('" + this.textBox1.Text + "','" + this.dateTimePicker1.Text + "','" + this.comboBox1.Text + "', '"+id_korisnika+"')";
            if (textBox1.Text == "" || dateTimePicker1.Text == "")
            {
                MessageBox.Show("Popunite obavezna polja!");
            }
            else
            {
                SQLiteCommand cmd = new SQLiteCommand(Query, cn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Podsjetnik je dodan!", "Important Message");
            }
            cn.Close();


            textBox1.Text = String.Empty;
            comboBox1.Text = String.Empty;
        }

        private void ObrisisListe()
        {
            var Id_pod = listView1.FocusedItem.Text;

            string query = "Delete from Podsjetnik where Id_pod=@id;";

            try
            {
                cn.Open();

                using (SQLiteTransaction trans = cn.BeginTransaction())
                {
                    using (SQLiteCommand com = new SQLiteCommand(query, cn, trans))
                    {

                        com.Parameters.AddWithValue("id", Id_pod);

                        var should_be_one = com.ExecuteNonQuery();

                        if (should_be_one == 1)
                        {

                            trans.Commit();
                            MessageBox.Show("Podsjetnik je obrisan!", "Important Message");

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
            ListaGostijuWindow PSW = new ListaGostijuWindow();
            PSW.Closed += (s, args) => this.Close();
            PSW.Show();

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

        private void WellcomeButtonImenik_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ImenikWindow IW = new ImenikWindow();
            IW.Closed += (s, args) => this.Close();
            IW.Show();
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



        private void button4_Click(object sender, EventArgs e)
        {
            ObrisisListe();
            fillview();
        }

        
        private void PodsjetnikWindow_Shown(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PodsjetnikWindow_Load(object sender, EventArgs e)
        {
            fillview();
        }
    }
}
