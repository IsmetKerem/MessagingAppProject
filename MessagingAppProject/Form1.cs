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

namespace MessagingAppProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=ChatDb;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKISILER where NUMARA=@p1 and SIFRE=@p2", baglanti);
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2",textBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read()) 
            {
                Form2 frm = new Form2();
                frm.numara = maskedTextBox1.Text;
                frm.Show();
            }
            else
            {
                MessageBox.Show("Numaranız veya şifrenizi hatalı girdiniz lütfen tekrar deneyin.","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            baglanti.Close();
        }
    }
}
