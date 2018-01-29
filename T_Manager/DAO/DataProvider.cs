using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace T_Manager.DAO
{
    public class DataProvider
    {
        private static string conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        private static DataProvider instance;

        public static DataProvider Instance
        {
            get
            {
                if (instance == null) instance = new DataProvider();
                return DataProvider.instance;
            }

            private set
            {
                DataProvider.instance = value;
            }
        }
        private DataProvider() { }

        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                if (parameter != null)
                {
                    string[] listParam = query.Split(' ');
                    int i = 0;
                    foreach (var item in listParam)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cnn.Close();
            }
            return dt;
        }
        public int ExcuteNonQuery(string query, object[] parameter = null)
        {

            int dt = 0;
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                if (parameter != null)
                {
                    string[] listParam = query.Split(' ');
                    int i = 0;
                    foreach (var item in listParam)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }


                try
                {
                    dt = cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {

                }


                cnn.Close();
            }
            return dt;
        }
        public int ExcuteScalar(string query, object[] parameter = null)
        {
            int dt = 0;
            using (SqlConnection cnn = new SqlConnection(conn))
            {
                cnn.Open();
                SqlCommand cmd = new SqlCommand(query, cnn);
                if (parameter != null)
                {
                    string[] listParam = query.Split(' ');
                    int i = 0;
                    foreach (var item in listParam)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                dt = (int)cmd.ExecuteScalar();
                cnn.Close();
            }
            return dt;

        }
    }
}
