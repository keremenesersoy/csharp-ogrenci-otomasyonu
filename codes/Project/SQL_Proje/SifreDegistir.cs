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

namespace SQL_Proje
{
    public partial class SifreDegistir : Form
    {
        public static SqlConnection connect;
        public static SqlDataAdapter data;
        public static SqlDataReader dr;
        public static SqlCommand cmd;
        public static string sqlcon = @"Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True";
        public int sonuc = 0;
        public SifreDegistir()
        {
            InitializeComponent();
        }

        private void SifreDegistir_Load(object sender, EventArgs e)
        {
            CaptchaOlustur();
        }

        public void EskiSifreKontrol()
        {
            string password = textBox1.Text;
            string user = login.session;
            string query = $"select sifre from uyeler where TC='{user}' and sifre='{VeriTabani.MD5Sifrele(password)}'";
            connect = new SqlConnection(sqlcon);
            cmd = new SqlCommand(query, connect);
            connect.Open();

            dr = cmd.ExecuteReader();

            // Eğer veri geldiyse 
            if (dr.Read())
            {
                string sorgu = "";
                if(login.sessionYetki == "2")
                {
                    sorgu = $"update tbl_ogrenci set ogr_parola = '{VeriTabani.MD5Sifrele(textBox2.Text)}' where ogr_TC = '{user}'";
                    VeriTabani.KomutYolla(sorgu);
                    sorgu = $"update uyeler set sifre = '{VeriTabani.MD5Sifrele(textBox2.Text)}' where TC = '{user}'";
                    VeriTabani.KomutYolla(sorgu);
                }
                else if(login.sessionYetki == "1")
                {
                    sorgu = $"update tbl_ogretmen set o_parola = '{VeriTabani.MD5Sifrele(textBox2.Text)}' where o_TC = '{user}'";
                    VeriTabani.KomutYolla(sorgu);
                    sorgu = $"update uyeler set sifre = '{VeriTabani.MD5Sifrele(textBox2.Text)}' where TC = '{user}'";
                    VeriTabani.KomutYolla(sorgu);
                }

                MessageBox.Show("Şifre Değiştirme Başarılı");
                label5.Text = "Şifre Değiştirme Başarılı";
                this.Close();  
            }
            else
            {
                label5.Text = "Eski Sifreniz Hatali !";
                CaptchaOlustur();
                textBox1.Focus();
            }
            connect.Close();
        }

        public void CaptchaOlustur()
        {
            textBox4.Clear();
            Random r = new Random();
            int ilk = r.Next(1, 100);
            int ikinci = r.Next(2, 50);
            sonuc = ilk + ikinci;
            label4.Text = ilk.ToString() + " + " + ikinci.ToString() + " = ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox4.Text == sonuc.ToString())
            {
                label5.Text = "";
                if(textBox2.Text == textBox3.Text)
                {
                    EskiSifreKontrol();
                }       
                else
                {
                    label5.Text = "Yeni Şifre Ve Tekrarı Aynı Değil";
                    CaptchaOlustur();
                    textBox2.Focus();
                }
            }
            else
            {
                label5.Text = "Catchpa hatali";
                CaptchaOlustur();
                textBox4.Focus();
            }
        }
    }
}
