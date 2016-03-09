using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; //ADO:n perusluokat
using System.Data.SqlClient; //SQL serveriä varten

namespace H8_Sql
{
    class Program
    {
        static void Main(string[] args)
        {
            //Steps
            // 1. Luodaan yhteys
            // 2. Tehdään SQL kysely
            // 3. Käsitellään "tulos" tässä DataReader-olio

            try
            {
                //1.
                string connStr = GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    //avataan yhteys
                    conn.Open();
                    //2., siitä luodaan Command-tyyppinen olio
                    string sql = "SELECT asioid, lastname, firstname FROM Presences";
                    //string sql = "SELECT asioid, lastname, firstname FROM Presences WHERE asioid = 'Eurybus'";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    //3.
                    SqlDataReader rdr = cmd.ExecuteReader();
                    //käydään reader-olio läpi, forward-only
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Console.WriteLine("Läsnäolosi {0} {1} {2}", rdr.GetString(0), 
                                rdr.GetString(1), rdr.GetString(2));
                        }
                        Console.WriteLine("Tiedot haettu onnistuneesti");
                    }
                    //Suljetaan tarvittavat
                    rdr.Close();
                    conn.Close();
                    Console.WriteLine("Tietokanta yhteys suljettu onnistuneesti");
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        private static string GetConnectionString()
        {
            //luetaan Connection string App.configista
            return H8_Sql.Properties.Settings.Default.Tietokanta;
        }
    }
}
