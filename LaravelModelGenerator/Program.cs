using MyProg;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaravelModelGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Laravel Model Generator V1.0.");
            Console.WriteLine("<c> 2024-2030 JP Programi");
            IniFile myini = new IniFile("settings.ini");
            string username = "N/A", password = "N/A", db_name = "N/A";
            bool data_set = false, data_saved = false;
            bool use_pass = false;
            if (myini.KeyExists("sDBName"))
            {
                db_name = myini.Read("sDBName");
                data_set = true;
            }
            if (myini.KeyExists("sDBUsername"))
            {
                username = myini.Read("sDBUsername");
                data_set = true;
            }
            if (myini.KeyExists("sDBPassword"))
            {
                password = myini.Read("sDBPassword");
                data_set = true;
            }
            if(myini.KeyExists("bUsePassword"))
            {
                use_pass = bool.Parse(myini.Read("bUsePassword"));
            }
            while (true)
            {
                Console.Write(">");
                string comm = Console.ReadLine();
                comm = comm.Trim();
                string[] commx = comm.Split(' ');
                //string[] commx2 = commx[0].Split('/');



                if (commx[0].ToUpper()=="SET")
                {
                    if (commx.Length < 2)
                    {
                        Console.WriteLine("ERROR: NOT ENOUGH ARGUMENTS.");
                        continue;
                    }

                    string[] commx2 = commx[1].Split('/');
                    if(commx2.Length<2)
                    {
                        Console.WriteLine("ERROR: NOT ENOUGH ARGUMENTS.");
                        continue;
                    }
                    if (commx2[0].ToUpper() == "DATABASE" || commx2[0].ToUpper() == "DB")
                    {
                        if (commx2[1].ToUpper() == "NAME")
                        {
                            if (commx.Length < 3)
                            {
                                Console.WriteLine("ERROR: NOT ENOUGH ARGUMENTS.");
                                continue;
                            }
                            db_name = commx[2];
                            Console.WriteLine("Database name set.");
                        }
                        else if (commx2[1].ToUpper()=="USERNAME" || commx2[1].ToUpper()=="USER")
                        {
                            if (commx.Length < 3)
                            {
                                Console.WriteLine("ERROR: NOT ENOUGH ARGUMENTS.");
                                continue;
                            }
                            username = commx[2];
                            Console.WriteLine("Database username set.");
                        }
                        else if (commx2[1].ToUpper()=="PASSWORD" || commx2[1].ToUpper()=="PASS")
                        {
                            if (commx.Length < 3)
                            {
                                Console.WriteLine("ERROR: NOT ENOUGH ARGUMENTS.");
                                continue;
                            }
                            password = commx[2];
                            Console.WriteLine("Database password set.");
                        }
                        else if (commx2[1].ToUpper() == "USEPASSWORD" || commx2[1].ToUpper() == "USEPASS")
                        {
                            if (commx.Length < 3)
                            {
                                Console.WriteLine("ERROR: NOT ENOUGH ARGUMENTS.");
                                continue;
                            }
                            if (commx[2].ToUpper()!="TRUE" && commx[2].ToUpper()!="TRUE" && commx[2]!="1" && commx[2]!="0")
                            {
                                Console.WriteLine("ERROR: ONLY BOOLEAN VALUES ALLOWED.");
                            }
                            use_pass = bool.Parse(commx[2]);
                            Console.WriteLine("Use password set.");
                        }
                        else if (commx2[1].ToUpper()=="INQUIRE")
                        {
                            if (db_name != "N/A" && username != "N/A" && password != "N/A")
                            {
                                data_set = true;
                            }
                            if (data_set==false)
                            {
                                Console.WriteLine("<!> WARNING: DATA NOT SET <!>");
                            }
                            else if(data_set==true && data_saved==true)
                            {
                                Console.WriteLine("<!> WARNING: DATA NOT SAVED <!>");
                            }
                            Console.WriteLine("Database name: "+db_name);
                            Console.WriteLine("Username: " + username);
                            Console.WriteLine("Password: " + password);
                            Console.WriteLine("Use password: " + use_pass.ToString());
                        }
                        else if (commx2[1].ToUpper()=="SAVE")
                        {
                            if (data_set == true)
                            {
                                myini.Write("sDBUsername", username);

                                myini.Write("sDBPassword", password);

                                myini.Write("sDBName", db_name);

                                myini.Write("bUsePassword", use_pass.ToString());

                                Console.WriteLine("Data saved successfully.");
                            }
                            else
                            {
                                Console.WriteLine("ERROR: Data not set.");
                                continue;
                            }
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Command not recognized.");
                            continue;
                        }
                    }
                    else if (commx2[0].ToUpper()=="TERMINAL")
                    {
                        if (commx2[1].ToUpper()=="CLEAR" || commx2[1].ToUpper() == "CLS")
                        {
                            Console.Clear();
                            Console.WriteLine("Laravel Model Generator V1.0.");
                            Console.WriteLine("<c> 2024-2030 JP Programi");
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Command not recognized.");
                            continue;
                        }
                    }
                }
                else if (commx[0].ToUpper()=="GENERATE")
                {
                    Console.WriteLine("Input model data:");
                    Console.Write("Name >");
                    string model_name = Console.ReadLine();
                    Console.Write ("Table >");
                    string table_name = Console.ReadLine();
                    string uns = "";
                    int count = 1, current = 0;
                    Console.Write("Generate collection? (Y/N) >");
                    uns = Console.ReadLine();
                    bool gencontroller = false, gencollection = false, genresource=false;
                    if(uns.ToUpper()=="Y")
                    {
                        gencollection = true;
                        count++;
                    }
                    Console.Write("Generate controller? (Y/N) >");
                    uns = Console.ReadLine();
                    if (uns.ToUpper() == "Y")
                    {
                        gencontroller = true;
                        count++;
                    }
                    Console.Write("Generate resource? (Y/N) >");
                    uns = Console.ReadLine();
                    if (uns.ToUpper() == "Y")
                    {
                        genresource = true;
                        count++;
                    }
                    Console.WriteLine("Setting up...");
                    try
                    {
                        DataManager dm = new DataManager(db_name, username, password,use_pass);
                        Generator g = new Generator(model_name, table_name, dm);
                        Console.WriteLine("0 OK.");
                        Console.WriteLine("Generating model...");
                        g.GenerateModel();
                        Console.WriteLine("DONE.");
                        if (gencollection == true)
                        {
                            Console.WriteLine("Generating collection...");
                            g.GenerateCollection();
                            Console.WriteLine("DONE.");
                            current++;
                        }
                        if (gencontroller == true)
                        {
                            Console.WriteLine("Generating controller...");
                            g.GenerateController();
                            Console.WriteLine("DONE.");
                            current++;
                        }
                        if (genresource == true)
                        {
                            Console.WriteLine("Generating resource...");
                            g.GenerateResource();
                            Console.WriteLine("DONE.");
                            current++;
                        }
                        Console.WriteLine(current + " OK.");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("ERROR: " + ex.Message);
                        Console.WriteLine(current + " ERROR.");
                    }
                }
                else if (commx[0].ToUpper()=="EXIT")
                {
                    break;
                }
                else if (commx[0].ToUpper()=="HELP" || commx[0].ToUpper()=="LIST")
                {

                }
                else
                {
                    Console.WriteLine("Unrecognized command '" + comm + "'.");
                }
            }
        }
    }
}
