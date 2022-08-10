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
    public partial class SınavNot : Form
    {
        public SınavNot()
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
        public static string table = "";


        void GridDoldur()
        {
            connect = new SqlConnection(sqlcon);    //sqle bağlandık
            data = new SqlDataAdapter($"select * from tbl_ogrenci where ogr_sinif = {comboBox1.Text}", connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, "tbl_ogrenci");
            dataGridView1.DataSource = ds.Tables["tbl_ogrenci"];  // tabloyu atadik
            connect.Close();

        }
        private void SınavNot_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "1";
            VeriTabani.GridTumunuDoldur(dataGridView1, "tbl_ogrenci");
            GridDoldur();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            label9.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            label7.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            label5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            label3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            label2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            label19.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            label17.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            label15.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu = $"select * from tbl_ders where ders_ogrno = {label9.Text} and ders_ad = '{İslemlerOgretmen.brans}'";

            connect = new SqlConnection(sqlcon);
            cmd = new SqlCommand(sorgu, connect);
            connect.Open();

            dr = cmd.ExecuteReader();

            // Eğer veri geldiyse 
            if (dr.Read())
            {
                sorgu = $"update tbl_ders set ders_not = {textBox1.Text} where ders_ogrno = {label9.Text}";
                VeriTabani.KomutYolla(sorgu);
                MessageBox.Show($"{label7.Text} Adlı Öğrencinin {İslemlerOgretmen.brans} Ders Notu = {textBox1.Text} güncellendi");
            }
            else
            {
                sorgu = $"insert into tbl_ders(ders_ad,ders_not,ders_sinif,ders_ogrno) values('{İslemlerOgretmen.brans}',{textBox1.Text},{label2.Text},{label9.Text})";
                VeriTabani.KomutYolla(sorgu);
                MessageBox.Show($"{label7.Text} Adlı Öğrencinin {İslemlerOgretmen.brans} Ders Notu = {textBox1.Text} eklendi");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDoldur();
        }
    }
}
