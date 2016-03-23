using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H10_Bookstore
{
    class DBbooks
    {
        public static DataTable GetTestData()
        {
            //Luodaan oikeanlainen datatable "leikkidatasta"
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("title",typeof(string));
            dt.Columns.Add("author", typeof(string));
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("year", typeof(int));
            //luodaan rivit
            dt.Rows.Add(11,"Pekka Lipposen seikkailut", "outsider","Suomi",1946);
            dt.Rows.Add(21, "Lucky Luke", "Rene Cosciny", "Belgia", 1946);
            return dt;
        }
        public static DataTable GetBooks(string connStr)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT id, name, author, country, year FROM books";
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable("Books");
                    da.Fill(dt);
                    return dt;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int UpdateBook(string connstr, int id, string title, string author, string country, int year)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    string sql = string.Format("UPDATE books SET title = @Nimi, author=@kirjailija, country='{1}',year={2} WHERE id={0}",
                        id,country,year);
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlParameter sp;
                    sp = new SqlParameter("Nimi", SqlDbType.NVarChar); ;
                    sp.Value = title;
                    cmd.Parameters.Add(sp);
                    sp = new SqlParameter("Kirjailija", SqlDbType.NVarChar);
                    sp.Value = author;
                    int lkm = cmd.ExecuteNonQuery();
                    return lkm;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
