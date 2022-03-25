using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace AplikacijaZaPoslovneKnjige
{
    class Baza
    {
        static DataSet _rsReport;
        public static DataSet rsReport
        {
            get
            {
                return _rsReport;
            }
            set
            {
                _rsReport = value;
            }
        }
        public System.Data.DataSet DajPodatke(string Query, string ImeTabele)
        {
            SqlConnection Conn = new SqlConnection(GetConnString());
            try
            {

                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                Conn.Open();
                da = new SqlDataAdapter(Query, Conn);
                da.Fill(ds, ImeTabele);

                return ds;
            }
            catch (Exception Exc)
            {
                DataSet dk = new DataSet();
                System.Windows.MessageBox.Show(Exc.Message, "Greska u generisanju dataset-a");
                return dk;
            }
            finally
            {
                Conn.Dispose();
                Conn.Close();
            }
        }

        public string GetConnString()  // metoda u kojoj se nalazi connection string
        {
            return @"Data Source=DESKTOP-UR8376F ;Initial Catalog=GlavnaKnjiga ;User ID= sa";  //;Password = 12345
        }


    }
}
