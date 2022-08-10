using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;

namespace SQL_Proje
{
    class VeriTabani
    {
        public static SqlConnection connect;
        public static SqlDataAdapter data;
        public static SqlCommand cmd;
        static SqlDataReader dr;
        public static DataSet ds;

        //User Id = sa , password = xxxxxx
        public static string sqlcon = @"Data Source=DESKTOP-3NOT18U;Initial Catalog=deneme;Integrated Security=True";

        public static bool BaglantiDurum()
        {
            //veri tabanı baglantısı control
            using(connect = new SqlConnection(sqlcon))
            {
                try
                {
                    connect.Open();
                    return true;
                }
                catch(SqlException exp)
                {
                    System.Windows.Forms.MessageBox.Show(exp.Message);
                    return false;
                }
            }
        }

        public static DataGridView GridTumunuDoldur(DataGridView gridim , string table)
        {
            connect = new SqlConnection(sqlcon);    //sqle bağlandık
            data = new SqlDataAdapter("select * from " + table, connect);  //sql sorugusu gönderdik
            ds = new DataSet();
            connect.Open();
            data.Fill(ds, table);
            gridim.DataSource = ds.Tables[table];  // tabloyu atadik
            connect.Close();
            return gridim;
        }

        public static bool LoginKontrol(string TC , string password)
        {
            string query = $"select * from uyeler where TC = '{TC}' and sifre = '{VeriTabani.MD5Sifrele(password)}'";
            connect = new SqlConnection(sqlcon);
            cmd = new SqlCommand(query, connect);
            connect.Open();

            dr = cmd.ExecuteReader();

            // Eğer veri geldiyse 
            if (dr.Read())
            {
                //MessageBox.Show("Giriş Başarılı !!");
                connect.Close();
                return true;
            }
            else
            {
                //MessageBox.Show("Öğrenci Numarası Veya Şifre Hatalı");
                connect.Close();
                return false;
            }
            
        }

        public static string MD5Sifrele(string sifrelenecekMetin)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);  // şifreleme algoritmaları byte olarak alındıgı için bu işlemi yapıyoruz

            // dizinin hash değeri çıkartılıyor
            dizi = md5.ComputeHash(dizi);

            StringBuilder sb = new StringBuilder();

            foreach (byte item in dizi)
            {
                sb.Append(item.ToString("x2").ToLower());
            }

            return sb.ToString();
        }

        public static string SHASifrele(string sifrelenecekmetin)
        {
            SHA256 SHA256hash = SHA256.Create();
            byte[] dizi = SHA256hash.ComputeHash(Encoding.UTF8.GetBytes(sifrelenecekmetin));
            StringBuilder sb = new StringBuilder();

            foreach (byte item in dizi)
            {
                sb.Append(item.ToString("x2").ToLower());
            }
            return sb.ToString();
        }

        public static void KomutYolla(string sorgu)
        {
            connect = new SqlConnection(sqlcon);
            cmd = new SqlCommand();        
            connect.Open();
            cmd.Connection = connect;
            cmd.CommandText = sorgu;
            cmd.ExecuteNonQuery();
            connect.Close();
          
        }

        public static void KomutYollaParametreli(string sorgu , SqlCommand cmd)
        {
            connect = new SqlConnection(sqlcon);
            cmd = new SqlCommand();
            connect.Open();
            cmd.Connection = connect;
            cmd.CommandText = sorgu;
            cmd.ExecuteNonQuery();
            connect.Close();
        }

    }

}

