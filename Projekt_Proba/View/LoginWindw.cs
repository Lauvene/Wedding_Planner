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
    public partial class LoginWindw : Form
    {
        public virtual ContentAlignment TextAlign { get; set; }
        public LoginWindw()
        {
            InitializeComponent();
        }
        string cs = @"Data Source=C:\Users\Zmajii\Downloads\final\projekt_01\db_projekt.db";
        public static int ID_korisnika { get; set; }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoginWindw_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }


        

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistracijaWindow RG = new RegistracijaWindow();
            RG.Closed += (s, args) => this.Close();
            RG.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e) //login button
        {
            if (textBox1.Text == "  " || textBox2.Text == "  ")
            {
                MessageBox.Show("Molim upisite username i password!");
                return;
            }
            try
            {
                //Kreiraj konekciju
                SQLiteConnection con = new SQLiteConnection(cs);
                SQLiteCommand cmd = new SQLiteCommand("Select * from Korisnik where Username=@Username and Password=@Password", con);
                SQLiteCommand cmd1 = new SQLiteCommand("Select Id_korisnik from Korisnik where Username=@Username", con);
                con.Open();

                cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                cmd.Parameters.AddWithValue("@Password", textBox2.Text);
                cmd1.Parameters.AddWithValue("@Username", textBox1.Text);
                object result = cmd1.ExecuteScalar();
                ID_korisnika = Convert.ToInt32(result);

                SQLiteDataAdapter adapt = new SQLiteDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapt.Fill(ds);
                con.Close();
                int count = ds.Tables[0].Rows.Count;

                //Ako je count =1 prikazi pocetni prozor
                if (count == 1)
                {
                    MessageBox.Show("Login Uspješan!");
                    this.Hide();
                    WeddingPlannerWellcomeWindow wm = new WeddingPlannerWellcomeWindow();
                    wm.Show();
                }
                else
                {
                    MessageBox.Show("Login Failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
            textBox2.Text = "";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    } 
}
