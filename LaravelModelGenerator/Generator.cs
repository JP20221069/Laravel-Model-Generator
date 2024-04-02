using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LaravelModelGenerator
{
    public class Generator
    {
        string model_template;
        string collection_template;
        string controller_template;
        string resource_template;

        string model_name;
        string table_name;

        DataManager datamanager;

        public string ModelName { get { return this.model_name; } set { this.model_name = value; }}
        public string TableName { get { return this.table_name; } set { this.table_name = value; } }

        public DataManager DataManager { get { return this.datamanager; } set { this.datamanager = value; } }
        public Generator(string model_name, string table_name, DataManager data_manager)
        {
            StreamReader myreader = new StreamReader("templates/model_template.txt");
            this.model_template = myreader.ReadToEnd();
            myreader = new StreamReader("templates/collection_template.txt");
            this.collection_template = myreader.ReadToEnd();
            myreader = new StreamReader("templates/controller_template.txt");
            this.controller_template = myreader.ReadToEnd();
            myreader = new StreamReader("templates/resource_template.txt");
            this.resource_template = myreader.ReadToEnd();
            this.model_name = model_name;
            this.table_name = table_name;
            this.datamanager = data_manager;
            if(!Directory.Exists("output"))
            {
                Directory.CreateDirectory("output");
            }

        }

        public void GenerateModel()
        {
            string temp = model_template;
            temp=Regex.Replace(temp, "<TABLE_NAME>", table_name);
            temp=Regex.Replace(temp, "<MODEL_NAME>", model_name);
            List<string> cols = DataManager.getColumns(table_name);
            string column_names = "";
            foreach(string name in cols)
            {
                if (name != cols.Last())
                {
                    column_names += "'"+ name + "',";
                }
                else
                {
                    column_names += "'"+name+"'";
                }
            }
            temp=Regex.Replace(temp, "<COLUMN_NAMES>", column_names);
            //File.Create("output/" + model_name + ".php");
            StreamWriter mywriter = new StreamWriter("output/" + model_name + ".php");
            try
            {
                mywriter.Write(temp);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                mywriter.Close();
            }
            
        }

        public void GenerateResource()
        {
            string temp = resource_template;
            string controller_name = model_name + "Controller";
            string resource_name = model_name + "Resource";
            temp=Regex.Replace(temp, "<MODEL_NAME>", model_name);
            temp=Regex.Replace(temp, "<CONTROLLER_NAME>", controller_name);
            temp=Regex.Replace(temp, "<RESOURCE_NAME>", resource_name);
            List<string> cols = DataManager.getColumns(table_name);
            string column_assg = "";
            foreach (string name in cols)
            {
                if (name != cols.Last())
                {
                    column_assg += "\t\t\t'"+name+"'=>$this->resource->"+name + ",\r\n";
                }
                else
                {
                    column_assg += "\t\t\t'" + name + "'=>$this->resource->" + name+"\r\n";
                }
            }
            temp = Regex.Replace(temp, "<COLUMN_ASSIGNMENTS>", column_assg);
            //File.Create("output/" + resource_name + ".php");
            StreamWriter mywriter = new StreamWriter("output/" + resource_name + ".php");
            try
            {
                mywriter.Write(temp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mywriter.Close();
            }
        }

        public void GenerateController()
        {
            string temp = controller_template;
            string controller_name = model_name + "Controller";
            string collection_name = model_name + "Collection";
            temp=Regex.Replace(temp, "<MODEL_NAME>", model_name);
            temp=Regex.Replace(temp, "<CONTROLLER_NAME>", controller_name);
            temp=Regex.Replace(temp, "<COLLECTION_NAME>", collection_name);
            //File.Create("output/" + controller_name + ".php");
            StreamWriter mywriter = new StreamWriter("output/" + controller_name + ".php");
            try
            {
                mywriter.Write(temp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mywriter.Close();
            }
        }

        public void GenerateCollection()
        {
            string temp = collection_template;
            string collection_name = model_name + "Collection";
            temp=Regex.Replace(temp, "<COLLECTION_NAME>", collection_name);
            //File.Create("output/" + collection_name + ".php");
            StreamWriter mywriter = new StreamWriter("output/" + collection_name + ".php");
            try
            {
                mywriter.Write(temp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                mywriter.Close();
            }
        }
    }
}
