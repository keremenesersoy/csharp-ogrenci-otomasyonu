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
    public partial class OgrenciKayit : Form
    {
        public OgrenciKayit()
        {
            InitializeComponent();
        }

        private void OgrenciKayit_Load(object sender, EventArgs e)
        {
            maskedTextBox2.MaxLength = 11;
            textBox4.MaxLength = 1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if(textBox6.Text != textBox8.Text)
            {
                MessageBox.Show("Parolalar Uyuşmuyor !");
                textBox8.Clear();
                textBox6.Clear();
                textBox8.Focus();
            }
            else if(textBox6.Text.Length < 8)
            {
                MessageBox.Show("Lütfen en az 8 karakterden oluşan bir şifre oluştrunuz");
            }
            else
            {
                if(textBox1.Text == "" || textBox2.Text == "" || maskedTextBox2.Text == "" || textBox1.Text == "" || textBox4.Text == "" || textBox6.Text == "" || maskedTextBox1.Text == "" || textBox8.Text == "")
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

                    string query = $"Insert into tbl_ogrenci(ogr_ad , ogr_soyad , ogr_TC , ogr_sinif , ogr_adres , ogr_telno , ogr_cinsiyet , yetkino , ogr_parola) VALUES('{textBox1.Text}','{textBox2.Text}','{maskedTextBox2.Text}',{textBox4.Text},'{richTextBox1.Text}','{maskedTextBox1.Text}','{cinsiyet}',2,'{VeriTabani.MD5Sifrele(textBox6.Text)}')";
                    VeriTabani.KomutYolla(query);
                    query = $"Insert into uyeler(TC,yetkino,sifre) values('{maskedTextBox2.Text}',2,'{VeriTabani.MD5Sifrele(textBox6.Text)}')";
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
