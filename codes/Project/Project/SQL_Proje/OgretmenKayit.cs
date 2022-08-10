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
    public partial class OgretmenKayit : Form
    {
        public OgretmenKayit()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void OgretmenKayit_Load(object sender, EventArgs e)
        {
            maskedTextBox2.MaxLength = 11;
        
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text != textBox8.Text)
            {
                MessageBox.Show("Parolalar Uyuşmuyor !");
                textBox8.Clear();
                textBox6.Clear();
                textBox8.Focus();
            }
            else if (textBox6.Text.Length < 8)
            {
                MessageBox.Show("Lütfen en az 8 karakterden oluşan bir şifre oluştrunuz");
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || maskedTextBox2.Text == "" || textBox1.Text == "" || textBox4.Text == "" || textBox6.Text == "" || maskedTextBox1.Text == "" || textBox8.Text == "")
                {
                    MessageBox.Show("Lütfen Butün Bilgileri Eksiksiz Giriniz");
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True");
                    SqlCommand cmd;
                    con.Open();
                    string sql = $"select * from uyeler where TC = '{maskedTextBox2.Text}'";
                    cmd = new SqlCommand(sql, con);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Bu Tc No ya ait Hesap Bulunmaktadır");
                        con.Close();
                        maskedTextBox2.Clear();
                        return;
                    }

                    con.Close();
                    string cinsiyet = "";
                    if (radioButton1.Checked) { cinsiyet = "Erkek"; }
                    else if (radioButton2.Checked) { cinsiyet = "Kadın"; }

                    string query = $"Insert into tbl_ogretmen(o_ad , o_soyad , o_TC , o_tel , o_cinsiyet , o_brans , o_parola , yetkino , adres) VALUES('{textBox1.Text}','{textBox2.Text}','{maskedTextBox2.Text}','{maskedTextBox1.Text}','{cinsiyet}','{textBox4.Text}','{VeriTabani.MD5Sifrele(textBox8.Text)}',1,'{richTextBox1.Text}')";
                    VeriTabani.KomutYolla(query);
                    query = $"Insert into uyeler(TC,yetkino,sifre) values('{maskedTextBox2.Text}',1,'{VeriTabani.MD5Sifrele(textBox6.Text)}')";
                    VeriTabani.KomutYolla(query);

                    MessageBox.Show("Kayıt Olma İşlemi Başarılı !");
                    login a = new login();
                    a.Show();

                    this.Close();


                }
            }
        }
    }
}
