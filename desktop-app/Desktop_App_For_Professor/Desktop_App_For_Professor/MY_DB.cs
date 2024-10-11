using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;//Mysql
namespace Desktop_App_For_Professor
{
    /*
     *in this class, connect application to Mysql database
     */

    internal class MY_DB
    {
        /*
         * gxk220025 10/7/2024
         * connect login form to mysql database
         * 
         */
        public static string server_name = "localhost";
        public static string database_name = "myDB";
        public static string server_username = "root";//default without password
        public static string server_password = "";//no password for root
        public static int port_num = 3306;

        static string connect_info = "server=localhost;uid=root;pwd=kotori1430;database=mydb";//@"Server=localhost;Database=mydb;User ID=root;Password=;SslMode=None;";//= "datasource=" + server_name + ";" + "port=" + port_num + ";" + "username=" + server_username + ";" + "password=" + server_password + ";" + "database=" + database_name;

        //the connection
        private MySqlConnection con = new MySqlConnection(connect_info); //con to connect

        //create a function to get the connection
        public MySqlConnection getConnection
        {
            get
            {
                return con;
            }
        }


        //create a function to open the connection
        public void openConnection()
        {
            if(con.State == ConnectionState.Closed) //libarary using System.data if not, use System.Data.Conn~
            {
                con.Open();
            }
        }

        //create a function to close the connection
        public void closeConnection()
        {
            if (con.State == ConnectionState.Open) //libarary using System.data if not, use System.Data.Conn~
            {
                con.Close();
            }
        }
    }
}
