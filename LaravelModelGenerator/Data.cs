using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelModelGenerator
{
    public class DataManager
    {
        string connectionstring = "Server=localhost;Database=spomenici;Uid=root;Pwd=;";
        string dbname;
        public string ConnectionString { get { return this.connectionstring; } set { this.connectionstring = value; } }
        public string DBName { get { return this.dbname; } set { this.dbname = value; } }
        public DataManager(string dbname,string username,string password,bool use_pass)
        {
            this.dbname = dbname;
            this.connectionstring = "Server=localhost;Database=" + dbname + ";Uid=" + username + ";";
            if (use_pass)
            {
                this.connectionstring += "Pwd=" + password + ";";
            }
        }
        public List<string> getColumns(string table_name)
        {
            List<string> ret = new List<string>();
            MySqlConnection connection = new MySqlConnection(connectionstring);

            try
            {
                connection.Open();
                string query = "SELECT `COLUMN_NAME` FROM `INFORMATION_SCHEMA`.`COLUMNS` WHERE `TABLE_SCHEMA`='"+DBName+"' AND `TABLE_NAME`='"+table_name+"';";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    ret.Add(dr.GetString(0));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return ret;
        }
    }
}
