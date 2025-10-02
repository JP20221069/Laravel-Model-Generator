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

        public List<ConstraintInfo> getConstraintInfo()
        {
            List<ConstraintInfo> ret = new List<ConstraintInfo>();
            MySqlConnection connection = new MySqlConnection(connectionstring);

            try
            {
                connection.Open();
                string query = @"SELECT i.TABLE_SCHEMA, i.TABLE_NAME, k.COLUMN_NAME, k.REFERENCED_TABLE_NAME, k.REFERENCED_COLUMN_NAME,i.CONSTRAINT_TYPE, i.CONSTRAINT_NAME
                                 FROM information_schema.TABLE_CONSTRAINTS i
                                 LEFT JOIN information_schema.KEY_COLUMN_USAGE k ON i.CONSTRAINT_NAME = k.CONSTRAINT_NAME
                                 WHERE i.CONSTRAINT_TYPE = 'FOREIGN KEY' AND i.CONSTRAINT_SCHEMA = '"+dbname+"';";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    ret.Add(new ConstraintInfo(dr["TABLE_NAME"].ToString(), dr["COLUMN_NAME"].ToString(), dr["REFERENCED_TABLE_NAME"].ToString(), dr["REFERENCED_COLUMN_NAME"].ToString(),ConstraintType.FOREIGN_KEY));
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
