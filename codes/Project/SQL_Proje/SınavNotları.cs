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
    public partial class SınavNotları : Form
    {
        public SınavNotları()
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
            data = new SqlDataAdapter($"select * from tbl_ders where ders_ogrno = {İslemlerOgrenci.ogr_no}", connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, "tbl_ogrenci");
            dataGridView1.DataSource = ds.Tables["tbl_ogrenci"];  // tabloyu atadik
            connect.Close();

        }

        private void SınavNotları_Load(object sender, EventArgs e)
        {
            VeriTabani.GridTumunuDoldur(dataGridView1,"tbl_ders");
            GridDoldur();
            dataGridView1.Columns[0].HeaderText = "Ders Adı";
            dataGridView1.Columns[1].HeaderText = "Ders Notu";
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;

            double toplam = 0;
           
            
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                toplam += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value.ToString());
            }

            label2.Text = (toplam / (dataGridView1.Rows.Count - 1)).ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            raporgoster a = new raporgoster();
            a.Show();
            this.Hide();
        }
    }
}
