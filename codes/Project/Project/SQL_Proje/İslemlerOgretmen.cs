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
    public partial class İslemlerOgretmen : Form
    {
        public İslemlerOgretmen()
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
        public static string brans = "";


        void GridDoldur()
        {
            connect = new SqlConnection(sqlcon);    //sqle bağlandık
            data = new SqlDataAdapter($"select * from tbl_ogretmen where o_TC = '{login.session}'", connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, "tbl_ogretmen");
            dataGridView1.DataSource = ds.Tables["tbl_ogretmen"];  // tabloyu atadik
            connect.Close();

        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            brans = textBox5.Text;
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void İslemlerOgretmen_Load(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            VeriTabani.GridTumunuDoldur(dataGridView1, "tbl_ogretmen");
            GridDoldur();
;
        }

        private void şifreDeğiştirToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SifreDegistir x = new SifreDegistir();
            x.Show();
        }

        private void dersProgramıToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DersProgramıO x = new DersProgramıO();
            x.Show();
        }

        private void öıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SınavNot a = new SınavNot();
            a.Show();

        }
    }
}
