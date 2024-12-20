using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReportGenerator.Class
{
    class CMySQL
    {
        private MySqlConnection m_conn;
        private DataTable m_data;
        private MySqlDataAdapter m_da;
        private MySqlCommand m_cb;
        private String m_cadC;

        public CMySQL()
        {
            m_cadC = String.Format("server={0};user id={1}; password={2}; database=jpacking; pooling=false",
                            "192.168.3.13", "root", "S3m4dm1n2017!");
            m_cb = new MySqlCommand();
        }

        public bool connect()
        {
            bool result = false;
            try
            {
                m_conn = new MySqlConnection(m_cadC);
                m_conn.Open();
                result = true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error connecting to the server: " + ex.Message);
            }
            return result;
        }

        public bool executeSPWhitoutP(String nameProc)
        {
            bool result = false;
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                connect();
                m_cb.Connection = m_conn;

                m_cb.CommandText = nameProc;
                m_cb.CommandType = CommandType.StoredProcedure;

                m_cb.ExecuteNonQuery();

                //Console.WriteLine("Employee number: " + cmd.Parameters["@empno"].Value);
                //Console.WriteLine("Birthday: " + cmd.Parameters["@bday"].Value);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine("Error " + ex.Number + " has occurred: " + ex.Message);
            }
            m_conn.Close();
            Console.WriteLine("Done.");
            return result;
        }
    }
}
