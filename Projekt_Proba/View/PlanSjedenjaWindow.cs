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
    public partial class PlanSjedenjaWindow : Form
    {
        public PlanSjedenjaWindow()
        {
            InitializeComponent();
            
            
        }
        int id_korisnika = LoginWindw.ID_korisnika;
        SQLiteConnection con = new SQLiteConnection(@"data source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db");
        object result3;



        private void fillview()
        {
            listView1.Items.Clear();
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand("Select Id_gost, Ime, Prezime, Id_stol From Gost where Id_korisika=@id", con);
            cmd.Parameters.AddWithValue("@id", id_korisnika);

            try
            {
                SQLiteDataReader dt = cmd.ExecuteReader();
                while (dt.Read())
                {
                    ListViewItem ittem = new ListViewItem(dt["Id_gost"].ToString());
                    ittem.SubItems.Add(dt["Ime"].ToString());
                    ittem.SubItems.Add(dt["Prezime"].ToString());
                    ittem.SubItems.Add(dt["Id_stol"].ToString());

                    listView1.Items.Add(ittem);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void fillviewstol()
        {
            listView2.Items.Clear();
            con.Open();
            SQLiteCommand cm = new SQLiteCommand("Select Id_stola, Naziv_stola, Broj_stolica From Stol", con);

            try
            {
                SQLiteDataReader dt = cm.ExecuteReader();
                while (dt.Read())
                {
                    ListViewItem ittem = new ListViewItem(dt["Id_stola"].ToString());
                    ittem.SubItems.Add(dt["Naziv_stola"].ToString());
                    ittem.SubItems.Add(dt["Broj_stolica"].ToString());

                    listView2.Items.Add(ittem);
                }
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void fillstolovi()
        {
            con.Open();
            int Id_gost = Convert.ToInt32(result3);
            SQLiteCommand cmd1 = new SQLiteCommand("Select Id_stola from Stol where Naziv_stola=@Naziv_stola", con);
            cmd1.Parameters.AddWithValue("@Naziv_stola", textBox2.Text);
            object result = cmd1.ExecuteScalar();
            int Id_stola = Convert.ToInt32(result);
            SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd1);
            DataSet ds = new DataSet();
            adapt.Fill(ds);
            int count = ds.Tables[0].Rows.Count;
            int b;
            if (count == 1)
            {
                SQLiteCommand cmd2 = new SQLiteCommand("Select Id_gost from Gost where Id_stol=@id_stola", con);
                cmd2.Parameters.AddWithValue("@id_stola", Id_stola);
                SQLiteDataReader reader = cmd2.ExecuteReader();

                while (reader.HasRows)
                {
                    reader.Read();
                    
                }
                b = reader.StepCount; //broji koliko je gostiju za tim stolom
                SQLiteCommand cmd3 = new SQLiteCommand("Select Broj_stolica from Stol where Id_stola=@id", con);
                cmd3.Parameters.AddWithValue("@id", Id_stola);

                object result1 = cmd3.ExecuteScalar();
                int broj_stolica = Convert.ToInt32(result1);

                if (broj_stolica>b) // provjerava ima li mjesta za zadanim stolom
                {
                    MessageBox.Show(Convert.ToString(Id_gost), "id_gost");
                    SQLiteCommand cmd4 = new SQLiteCommand("Update  Gost set Id_stol='" + Id_stola + "' where Id_gost='"+Id_gost+"'", con);
                    cmd4.ExecuteNonQuery();
                    MessageBox.Show("Dodano!", "Obavijest");
                    con.Close();
                    

                }
                else
                {
                    MessageBox.Show("Stol nema mjesta!", "Upozorenje!");
                }
            }
               
            else{
                MessageBox.Show("Ne postoji", " Upozorenje!");
            }
            con.Close();
            
        }




        private void fillnovistol()
        {
            con.Open();

                string Query = "Insert into Stol (Naziv_stola, Broj_stolica) values('" + this.textBox1.Text + "','" + this.textBox3.Text + "')";
                SQLiteCommand createCommand = new SQLiteCommand(Query, con);
                createCommand.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Spremljeno!", "Obavijest");
                fillview();
                fillviewstol();
           
        }

        void obrisiStol()
        {
            object result4 = listView2.FocusedItem.Text;
            int id_stola = Convert.ToInt32(result4);
           

            con.Open();

            SQLiteCommand cm2 = new SQLiteCommand("Update  Gost set Id_stola=NULL where  Id_stola=@id", con);
            SQLiteCommand cm1 = new SQLiteCommand("Delete from Stol where Id_stola=@Id_stola", con);

            cm2.Parameters.AddWithValue("@id", id_stola);
            cm1.Parameters.AddWithValue("@Id_stola", id_stola);


            cm2.ExecuteNonQuery();
            cm1.ExecuteNonQuery();
            MessageBox.Show("Obrisano", "Obavijest!");

            

            con.Close();
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


        private void button1_Click(object sender, EventArgs e)
        {

            fillstolovi();
            fillview();
            fillviewstol();


        }


        private void button2_Click(object sender, EventArgs e)
        {
            fillnovistol();
        }



        private void PlanSjedenjaWindow_Load(object sender, EventArgs e)
        {
            listView1.FullRowSelect = true;
            listView2.FullRowSelect = true;
            fillview();
            fillviewstol();
        }

        public void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                textBox4.Text = item.SubItems[1].Text;
                textBox5.Text = item.SubItems[2].Text;
            }
            else
            {
                textBox4.Text = string.Empty;
                textBox5.Text = string.Empty;
            }
            result3 = listView1.FocusedItem.Text;
           
          
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void obrisibtn_Click(object sender, EventArgs e)
        {
            obrisiStol();
            fillview();
            fillviewstol();
        }
    }
}
