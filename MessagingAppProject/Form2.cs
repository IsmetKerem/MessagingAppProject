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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=localhost;Initial Catalog=ChatDb;Integrated Security=True");

        public string numara;
        void gelenkutusu()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select MESAJID, (AD+' '+SOYAD) as GONDEREN,BASLIK,ICERIK From TBLMESAJLAR inner join TBLKISILER on TBLMESAJLAR.GONDEREN=TBLKISILER.NUMARA where ALICI=" + numara, baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

        }
        void gidenkutusu()
        {
            SqlDataAdapter da2 = new SqlDataAdapter("Select MESAJID,(AD+' '+SOYAD) as ALICI,BASLIK,ICERIK from TBLMESAJLAR inner join TBLKISILER on TBLMESAJLAR.ALICI=TBLKISILER.NUMARA where GONDEREN=" + numara, baglanti);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            lblnumara.Text= numara;
            gelenkutusu();
            gidenkutusu();

            // AD-SOYAD ÇEKME
            baglanti.Open ();
            SqlCommand komut = new SqlCommand("Select AD,SOYAD from TBLKISILER where NUMARA=" + numara, baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) 
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
            }
            baglanti.Close ();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMESAJLAR (GONDEREN,ALICI,BASLIK,ICERIK) values (@p1,@p2,@p3,@p4)",baglanti);
            komut.Parameters.AddWithValue("@p1", numara);
            komut.Parameters.AddWithValue("@p2",mskalici.Text);
            komut.Parameters.AddWithValue("@p3",txtbaslik.Text);
            komut.Parameters.AddWithValue("@p4",rcticerik.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Mesajınız Başarıyla İletildi");
            gidenkutusu ();

        }
    }
}
