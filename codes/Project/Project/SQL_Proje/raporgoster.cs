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
    public partial class raporgoster : Form
    {
        public raporgoster()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        public DataSet ds;
        public static string sqlcon = @"Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True";
        private void raporgoster_Load(object sender, EventArgs e)
        {
            raporDoldur($"select * from tbl_ders where ders_ogrno = {İslemlerOgrenci.ogr_no}");
        }
        public void raporDoldur(string sql)
        {
            con = new SqlConnection(sqlcon);
            da = new SqlDataAdapter(sql,con);
            ds = new DataSet();

            con.Open();
            da.Fill(ds);
            raporSinavNot1.SetDataSource(ds.Tables[0]);
            crystalReportViewer1.ReportSource = raporSinavNot1;
            con.Close();
        }
    }
}
