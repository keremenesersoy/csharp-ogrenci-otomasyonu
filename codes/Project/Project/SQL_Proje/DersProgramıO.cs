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
    public partial class DersProgramıO : Form
    {
        public DersProgramıO()
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
        public static string deger = "";


        void GridDoldur()
        {
            connect = new SqlConnection(sqlcon);    //sqle bağlandık
            data = new SqlDataAdapter($"select * from sinif{comboBox1.Text} ", connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, $"sinif{comboBox1.Text}");
            dataGridView1.DataSource = ds.Tables[$"sinif{comboBox1.Text}"];  // tabloyu atadik
            connect.Close();

        }
        private void DersProgramıO_Load(object sender, EventArgs e)
        {
            comboBox1.Text = "1";
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            VeriTabani.GridTumunuDoldur(dataGridView1, $"sinif{comboBox1.Text}");
            //dataGridView1.CurrentCell.Value 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridDoldur();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string sql = $"update sinif{comboBox1.Text} set {comboBox2.Text} = '{İslemlerOgretmen.brans}' where Gün = '{comboBox3.Text}'";
            VeriTabani.KomutYolla(sql);
            GridDoldur();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
