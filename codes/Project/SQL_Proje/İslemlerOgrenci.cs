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
    public partial class İslemlerOgrenci : Form
    {
        public İslemlerOgrenci()
        {
            InitializeComponent();
        }

        SqlConnection connect;
        SqlDataAdapter data;
        SqlCommand cmd;
        DataSet ds;
        //User Id = sa , password = xxxxxx
        public static SqlDataReader dr;
        public static string sqlcon = @"Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True";
        public static string ogr_no = "";
        public static string sinif = "";
        

        void GridDoldur()
        {
            connect = new SqlConnection(sqlcon);    //sqle bağlandık
            data = new SqlDataAdapter($"select * from tbl_ogrenci where ogr_TC = '{login.session}'", connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, "tbl_ogrenci");
            dataGridView1.DataSource = ds.Tables["tbl_ogrenci"];  // tabloyu atadik
            connect.Close();

        }

        private void İslemlerOgrenci_Load(object sender, EventArgs e)
        {
            VeriTabani.GridTumunuDoldur(dataGridView1, "tbl_ogrenci");
            dataGridView1.Visible = false;
            GridDoldur();
        }


        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = $"update tbl_ogrenci set ogr_ad = '{textBox2.Text}' , ogr_soyad = '{textBox3.Text}' , ogr_TC = '{textBox4.Text}' , ogr_sinif = {textBox5.Text} , ogr_adres = '{textBox9.Text}' , ogr_telno = '{textBox8.Text}' , ogr_cinsiyet = '{textBox7.Text}' , ogr_parola = '{VeriTabani.MD5Sifrele(textBox6.Text)}' where ogr_TC = '{textBox4.Text}'";
            VeriTabani.KomutYolla(sql);
            sql = $"update uyeler set TC = '{textBox4.Text}' , sifre = '{VeriTabani.MD5Sifrele(textBox6.Text)}' where TC = '{textBox4.Text}'";
            VeriTabani.KomutYolla(sql);
            MessageBox.Show("Bilgileriniz Güncellendi");
            GridDoldur();
        }

        private void şifreDeğiştirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SifreDegistir a = new SifreDegistir();
            a.Show();
        }

        private void dersProgramıToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sinif = textBox5.Text;
            DersProgram a = new DersProgram();
            a.Show();
        }

        private void sınavNotlarıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ogr_no = textBox1.Text;
            SınavNotları a = new SınavNotları();
            a.Show();

        }

        private void öıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
