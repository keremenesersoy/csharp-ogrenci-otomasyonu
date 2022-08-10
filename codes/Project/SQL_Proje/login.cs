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
    public partial class login : Form
    {
        public static SqlConnection connect;
        public static SqlDataAdapter data;
        public static SqlDataReader dr;
        public static SqlCommand cmd;
        public static DataSet ds;
        public int try_counter = 0;
        //User Id = sa , password = xxxxxx
        public static string sqlcon = @"Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True";
        public static string session = "";
        public static string sessionYetki = "";

        public login()
        {
            InitializeComponent();

            //data = new SqlDataAdapter("select * from " + sqlsorgu, connect);  //sql sorugusu gönderdik
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "admin" && textBox2.Text == "123")
            {
                Form1 frm1 = new Form1();
                this.Hide();
                frm1.Show();
            }

            if(VeriTabani.LoginKontrol(textBox1.Text, textBox2.Text))
            {
                session = textBox1.Text;

                string query = $"select * from tbl_ogrenci where ogr_TC = '{textBox1.Text}'";
                connect = new SqlConnection(sqlcon);
                cmd = new SqlCommand(query, connect);
                connect.Open();

                dr = cmd.ExecuteReader();

                // Eğer veri geldiyse 
                if (dr.Read())
                {
                    MessageBox.Show("Giriş Başarılı !!");
                    this.Hide();
                    sessionYetki = "2";
                    İslemlerOgrenci a = new İslemlerOgrenci();
                    this.Hide();
                    a.Show();
                    connect.Close();
  
                }
                else
                {
                    MessageBox.Show("Giriş Başarılı !!");
                    this.Hide();
                    sessionYetki = "1";
                    İslemlerOgretmen a = new İslemlerOgretmen();
                    this.Hide();
                    a.Show();
                    connect.Close();
                    
                }

            }
            else
            {
                try_counter++;  
                if(try_counter >= 3)
                {
                    MessageBox.Show("3 Hatali Giriş Yaptınız");
                    Application.Exit();
                }

                
            }
        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            KayitEkle x = new KayitEkle();
            x.Show();
            this.Hide();
        }
    }
}


