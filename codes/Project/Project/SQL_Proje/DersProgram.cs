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
    public partial class DersProgram : Form
    {
        public DersProgram()
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
            data = new SqlDataAdapter($"select * from tbl_ogrenci where ogr_TC = '{login.session}'", connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, "tbl_ogrenci");
            dataGridView1.DataSource = ds.Tables["tbl_ogrenci"];  // tabloyu atadik
            connect.Close();

        }
        private void DersProgram_Load(object sender, EventArgs e)
        {
            VeriTabani.GridTumunuDoldur(dataGridView1 , $"sinif{İslemlerOgrenci.sinif}");
        }
    }
}
