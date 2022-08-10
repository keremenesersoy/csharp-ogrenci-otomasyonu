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
    public partial class Form1 : Form
    {
        SqlConnection connect;
        SqlDataAdapter data;
        SqlCommand cmd;
        DataSet ds;
        //User Id = sa , password = xxxxxx
        public static SqlDataReader dr;
        public static string sqlcon = @"Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True";
        public static string table = "";


        void GridDoldurP(string sql, string table)
        {
            connect = new SqlConnection(sqlcon);    //sqle bağlandık
            data = new SqlDataAdapter(sql, connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, table);
            dataGridView1.DataSource = ds.Tables[table];  // tabloyu atadik
            connect.Close();

        }

        //Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True
        public Form1()
        {
            InitializeComponent();
            if (VeriTabani.BaglantiDurum())
            {
                MessageBox.Show("Baglanti Kuruldu");
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox3.Enabled = false;
            VeriTabani.GridTumunuDoldur(dataGridView1, "tbl_ogrenci");
            dataGridView1.Columns[0].HeaderText = "No";
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "TC No";
            dataGridView1.Columns[4].HeaderText = "Sınıf";
            dataGridView1.Columns[5].HeaderText = "Adres";
            dataGridView1.Columns[6].HeaderText = "Telefon";
            dataGridView1.Columns[7].HeaderText = "Cinsiyet";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (radioButton1.Checked)
            {
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox8.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox10.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

            }
            else if (radioButton2.Checked)
            {
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox10.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            }

        }

        private void button3_Click(object sender, EventArgs e) 
        {
            if (TCKontrol(textBox1.Text))
            {
                MessageBox.Show("Bu Tc Kimlik Numarasına Ait Hesap Bulunmaktadır");
                return;
            }
            if (radioButton1.Checked) /// öğrenci
            {
                if(textBox9.Text.Length <= 8)
                {
                    MessageBox.Show("En az 8 karakterli bir şifre belirleyiniz");
                }
                else
                {
                    string sql = $"insert into tbl_ogrenci(ogr_ad,ogr_soyad,ogr_TC,ogr_sinif,ogr_adres,ogr_telno,ogr_cinsiyet,yetkino,ogr_parola) values('{textBox2.Text}','{textBox4.Text}','{textBox1.Text}',{Convert.ToInt32(textBox8.Text)},'{textBox10.Text}','{textBox6.Text}','{textBox7.Text}',2,'{VeriTabani.MD5Sifrele(textBox9.Text)}')";
                    VeriTabani.KomutYolla(sql);
                    sql = $"insert into uyeler(TC,yetkino,sifre) values('{textBox1.Text}',2,'{textBox9.Text}')";
                    VeriTabani.KomutYolla(sql);
                    GridDoldurP("select * from tbl_ogrenci", "tbl_ogrenci");
                }
            }
            else if(radioButton2.Checked) // öğretmen
            {
                if (textBox9.Text.Length <= 8)
                {
                    MessageBox.Show("En az 8 karakterli bir şifre belirleyiniz");
                }
                else
                {
                    string sql = $"insert into tbl_ogretmen(o_ad,o_soyad,o_TC,o_brans,adres,o_tel,o_cinsiyet,yetkino,o_parola) values('{textBox2.Text}','{textBox4.Text}','{textBox1.Text}','{textBox8.Text}','{textBox10.Text}','{textBox6.Text}','{textBox7.Text}',1,'{VeriTabani.MD5Sifrele(textBox9.Text)}')";
                    VeriTabani.KomutYolla(sql);
                    sql = $"insert into uyeler(TC,yetkino,sifre) values('{textBox1.Text}',1,'{textBox9.Text}')";
                    VeriTabani.KomutYolla(sql);
                    GridDoldurP("select * from tbl_ogretmen", "tbl_ogretmen");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string sorgu = $"delete from tbl_ogrenci where ogr_TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sorgu);
                sorgu = $"delete from uyeler where TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sorgu);
                GridDoldurP("select * from tbl_ogrenci", "tbl_ogrenci");
            }
            else if (radioButton2.Checked)
            {
                string sorgu = $"delete from tbl_ogretmen where o_TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sorgu);
                sorgu = $"delete from uyeler where TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sorgu);
                GridDoldurP("select * from tbl_ogretmen", "tbl_ogretmen");

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();

        }


        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            VeriTabani.GridTumunuDoldur(dataGridView1, "tbl_ogretmen");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "TC No";
            dataGridView1.Columns[4].HeaderText = "Telefon";
            dataGridView1.Columns[5].HeaderText = "Cinsiyet";
            dataGridView1.Columns[6].HeaderText = "Branş";
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].HeaderText = "Adres";
            radioButton7.Text = "Branşa Göre Ara";
            label2.Text = "ID:";
            label8.Text = "Branş:";
            table = "tbl_ogretmen";
            maskedTextBox1.Visible = false;
            textBox5.Visible = true;
            if (radioButton7.Checked)
            {
                textBox5.Text = "Branş Giriniz...";
            }

        }


        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if(textBox5.Text != "" && textBox5.Text != "İsim Giriniz..." && textBox5.Text != "Branş Giriniz...")
            {
                if (radioButton1.Checked)
                {
                    if (radioButton4.Checked)
                    {
                        string sql = $"select * from tbl_ogrenci where ogr_ad like '%{textBox5.Text}%' order by ogr_ad asc";
                        GridDoldurP(sql, "tbl_ogrenci");
                    }

                }
                else if (radioButton2.Checked)
                {
                    if (radioButton4.Checked)
                    {
                        string sql = $"select * from tbl_ogretmen where o_ad like '%{textBox5.Text}%' order by o_ad asc";
                        GridDoldurP(sql, "tbl_ogretmen");
                    }
                    else
                    {
                        string sql = $"select * from tbl_ogretmen where o_brans like '%{textBox5.Text}%' order by o_brans asc";
                        GridDoldurP(sql, "tbl_ogretmen");
                    }

                }
            }
            else
            {
                if (radioButton1.Checked)
                {
                    string sql = "select * from tbl_ogrenci";
                    GridDoldurP(sql, "tbl_ogrenci");
                }
                else if (radioButton2.Checked)
                {
                    string sql = "select * from tbl_ogretmen";
                    GridDoldurP(sql, "tbl_ogretmen");
                }
            }
        }

        private void textBox5_MouseClick(object sender, MouseEventArgs e)
        {
            textBox5.Clear();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            VeriTabani.GridTumunuDoldur(dataGridView1, "tbl_ogrenci");
            dataGridView1.Columns[0].HeaderText = "No";
            dataGridView1.Columns[1].HeaderText = "Ad";
            dataGridView1.Columns[2].HeaderText = "Soyad";
            dataGridView1.Columns[3].HeaderText = "TC No";
            dataGridView1.Columns[4].HeaderText = "Sınıf";
            dataGridView1.Columns[5].HeaderText = "Adres";
            dataGridView1.Columns[6].HeaderText = "Telefon";
            dataGridView1.Columns[7].HeaderText = "Cinsiyet";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            radioButton7.Text = "Numaraya Göre Ara";
            label2.Text = "Öğrenci No:";
            label8.Text = "Sınıf:";
            table = "tbl_ogrenci";
            if (radioButton7.Checked)
            {
                textBox5.Visible = false;
                maskedTextBox1.Visible = true;
            }
            textBox5.Text = "İsim Giriniz...";

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox5.Text = "İsim Giriniz...";
                maskedTextBox1.Visible = false;
                textBox5.Visible = true;
            }
            else if (radioButton7.Checked)
            {
                maskedTextBox1.Visible = true;
                textBox5.Visible = false;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                textBox5.Text = "İsim Giriniz...";
                maskedTextBox1.Visible = false;
                textBox5.Visible = true;
            }
            else if (radioButton7.Checked)
            {
                maskedTextBox1.Visible = true;
                textBox5.Visible = false;
            }
            if (radioButton2.Checked)
            {
                textBox5.Text = "Branş Giriniz...";
                maskedTextBox1.Visible = false;
                textBox5.Visible = true;
            }
        }

        public bool TCKontrol(string TC)
        {
            string sql = $"select * from uyeler where TC= '{TC}'";

            connect = new SqlConnection(sqlcon);
            cmd = new SqlCommand(sql, connect);
            connect.Open();

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            string sql = $"select * from tbl_ogrenci where ogr_no like '%{maskedTextBox1.Text}%' order by ogr_no asc";
            GridDoldurP(sql, "tbl_ogrenci");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                string sql = $"update tbl_ogrenci set ogr_ad = '{textBox2.Text}' , ogr_soyad = '{textBox4.Text}' , ogr_TC = '{textBox1.Text}' , ogr_sinif = {textBox8.Text} , ogr_adres = '{textBox10.Text}' , ogr_telno = '{textBox6.Text}' , ogr_cinsiyet = '{textBox7.Text}' , ogr_parola = '{VeriTabani.MD5Sifrele(textBox9.Text)}' where ogr_TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sql);
                sql = $"update uyeler set TC = '{textBox1.Text}' , sifre = '{VeriTabani.MD5Sifrele(textBox9.Text)}' where TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sql);
                GridDoldurP("select * from tbl_ogrenci", "tbl_ogrenci");
            }
            else if (radioButton2.Checked)
            {
                string sql = $"update tbl_ogretmen set o_ad = '{textBox2.Text}' , o_soyad = '{textBox4.Text}' , o_TC = '{textBox1.Text}' , o_brans = '{textBox8.Text}' , adres = '{textBox10.Text}' , o_tel = '{textBox6.Text}' , o_cinsiyet = '{textBox7.Text}' , o_parola = '{VeriTabani.MD5Sifrele(textBox9.Text)}' where o_TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sql);
                sql = $"update uyeler set TC = '{textBox1.Text}' , sifre = '{VeriTabani.MD5Sifrele(textBox9.Text)}' where TC = '{textBox1.Text}'";
                VeriTabani.KomutYolla(sql);
                GridDoldurP("select * from tbl_ogretmen", "tbl_ogretmen");
            }
        }
    }
}
