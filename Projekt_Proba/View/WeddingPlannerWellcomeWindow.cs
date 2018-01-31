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
    public partial class WeddingPlannerWellcomeWindow : Form
    {

        public WeddingPlannerWellcomeWindow()
        {
            InitializeComponent();
            

        }

        SQLiteConnection con = new SQLiteConnection(@"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db");
        int id_korisnika = LoginWindw.ID_korisnika;


        public class Lista
        {
            List<string> quote = new List<string> {
            " “You don’t marry the person you can live with … you marry the person you can’t live without.” —Unknown Author",
            " “The greatest marriages are built on teamwork. A mutual respect, a healthy dose of admiration, and a never-ending portion of love and grace.” —Fawn Weaver",
            " “Faith makes all things possible. Love makes all things easy.” Dwight Moody",
            " “In all the world, there is no heart for me like yours. In all the world, there is no love for you like mine.” Maya Angelou",
            " “The greatest thing you’ll ever learn is just to love, and be loved in return” -(Moulin Rouge)",
            " “When you realize you want to spend the rest of your life with somebody, you want the rest of your life to start as soon as possible.”– When Harry Met Sally "
            };
            public string Random()
            {
                Random r = new Random();
                int index = r.Next(quote.Count);
                string randomString = quote[index];
                return randomString;
            }



        }

        private void LoadData()
        {
            DataSet ds = new DataSet();
            con.Open();
            SQLiteDataAdapter adapt = new SQLiteDataAdapter("Select tipTroska, Cijena from Trosak where Id_korisnika='" + id_korisnika + "'", con);
            adapt.Fill(ds);
            chart1.DataSource = ds;
            //set the member of the chart data source used to data bind to the X-values of the series   
            chart1.Series["Kategorija"].XValueMember = "tipTroska";
            //set the member columns of the chart data source used to data bind to the X-values of the series   
            chart1.Series["Kategorija"].YValueMembers = "Cijena";
            chart1.Titles.Add("Troškovnik");
            con.Close();

        }
        private void odbrojavanje()
        {
            con.Open();
            SQLiteCommand cmd1 = new SQLiteCommand("Select Datum_vjencanja from Korisnik where Id_korisnik=@id", con);
            cmd1.Parameters.AddWithValue("@id", id_korisnika);

            object result = cmd1.ExecuteScalar();
            DateTime date = DateTime.Now;
            string nesto=Convert.ToString(result);
            DateTime kraj;
            kraj = DateTime.Parse(nesto).Date;
            double ukupno = (kraj - date).TotalDays;
            int i = Convert.ToInt32(ukupno);
            Convert.ToString(i);
            label3.Text = +i+" dana do Vašeg vjenčanja!!";
            con.Close();
        }

        private void Fotografije()
        {
            




        }





        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var TroskovnikWindow = new TroskovnikWindow();
            TroskovnikWindow.Closed += (s, args) => this.Close();
            TroskovnikWindow.Show();
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
            ListaGostijuWindow LGW = new ListaGostijuWindow();
            LGW.Closed += (s, args) => this.Close();
            LGW.Show();
        }

        private void WellcomeButtonPlanSjedenja_Click(object sender, EventArgs e)
        {
            this.Hide();
            PlanSjedenjaWindow PSW = new PlanSjedenjaWindow ();
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

        private void WeddingPlannerWellcomeWindow_Load(object sender, EventArgs e)
        {
            Lista l1 = new Lista();
            label2.Text =l1.Random();
            LoadData();
            odbrojavanje();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

 
    }
}
