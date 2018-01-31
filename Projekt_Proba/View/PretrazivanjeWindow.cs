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
    public partial class PretrazivanjeWindow : Form
    {
        public PretrazivanjeWindow()
        {
            InitializeComponent();
        }


        SQLiteConnection cn = new SQLiteConnection(@"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db");


        private void pretrazi()
        {
            if (KategorijaComboText.Text == "")
            {
                MessageBox.Show("Molim odaberite kategoriju", "Obavijest!");
                return;
            }
            cn.Open();
            listView1.Items.Clear();
            SQLiteCommand cm = new SQLiteCommand("SELECT Tvrtke.id_tvrtke, Tvrtke.Naziv_tvrtke, Tvrtke.Telefon, Tvrtke.Email,Tvrtke.naziv_kat, Tvrtke.Napomena, Lokacija.Naziv_lokacije FROM Tvrtke INNER JOIN Lokacija on Tvrtke.Id_lokacija=Lokacija.Id_lok WHERE naziv_kat = @naziv", cn);
          
            cm.Parameters.AddWithValue("@naziv", KategorijaComboText.Text);
        

            try
            {
                SQLiteDataReader dt = cm.ExecuteReader();
         
                while (dt.Read())
                {
                    ListViewItem ittem = new ListViewItem(dt["id_tvrtke"].ToString());
                    ittem.SubItems.Add(dt["Naziv_tvrtke"].ToString());
                    ittem.SubItems.Add(dt["Telefon"].ToString());
                    ittem.SubItems.Add(dt["Email"].ToString());
                    ittem.SubItems.Add(dt["Napomena"].ToString());
                    ittem.SubItems.Add(dt["Naziv_lokacije"].ToString());

                    listView1.Items.Add(ittem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cn.Close();
        }


        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            pretrazi();
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

        private void WellcomeButtonTroskovnik_Click(object sender, EventArgs e)
        {

            this.Hide();
            TroskovnikWindow TW = new TroskovnikWindow();
            TW.Closed += (s, args) => this.Close();
            TW.Show();
        }


        private void WellcomeButtonListaGostiju_Click(object sender, EventArgs e)
        {
            this.Hide();
            ListaGostijuWindow PSW = new ListaGostijuWindow();
            PSW.Closed += (s, args) => this.Close();
            PSW.Show();
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

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            pretrazi();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)// pretrazi btn
        {
            pretrazi();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void PretrazivanjeWindow_Load(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;

        }
    }
}

