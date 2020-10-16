using Microsoft.Ajax.Utilities;
using RadioWebConfig.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class Stations : System.Web.UI.Page
    {



        string connection = "";
        public static string path { get; } = Settings.Default.ConfigsPath;
        public static string stationNr;
        public static string newStationFolder;
        public static string newTruckFolder;
        public static string defaultFileName = "mall.txt";
        public static string sourceFile = "";
        public static string destFile = "";
        public static string sourceFolder = "";

        //public static string stationNr;
        //public static string newStationFolder;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["myUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            var user = Session["myUser"] as LoggedInUser;
            if (user.Role != 1)
            {
                Response.Redirect("~/Default.aspx");
                return;
            }

            SqlConnection();

            GetDirectories();
            
        }

        protected string SqlConnection()
        {
            SqlConnection conn = new SqlConnection(
            new SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "WebAppUsers"

            }.ConnectionString
            );

            connection = conn.ConnectionString;

            return connection;
        }
        private void GetDirectories()
        {
            string path = Settings.Default.ConfigsPath;
            //DataTable dt = new DataTable();
            // dt.Columns.Add("Folder", typeof(string));
            try
            {
                // Rensa listningen så de nya skrivs ut under de gamla vid ny page_load
                lbl.Text = "";
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    lbl.Text += $"<div class='stationdiv'><a class='stations' href='ShowTrucks.aspx?name={Path.GetFileName(dir)}' >" + Path.GetFileName(dir) + "</a></div>";
                    //lbl.Text += $"<div><a href='#'" + Path.GetFileName(dir) + "</a></div>";

                }
                if (dirs.Length <= 0)
                {
                    lbl.Text = "Hittade inga filer";
                }

                //  rpt.DataSource = dt; //your repeater 
                // rpt.DataBind(); //your repeater 
            }
            catch (Exception e)
            {
                lbl.Text = "Något blev fel";
            }
        }


        [WebMethod]
        [ScriptMethod]
        public static string AddNewStation(string val)
        {
            // Lägg till invalid chars
            // TODO Den här metoden är kaos. Funkar ibland och ibland inte. Ingen krasch men ingen mapp skapas om jag inte breakpointar mig igenom den. Verkar funka vid Thread.Sleep-addering
            HttpContext.Current.Session["value"] = val;
            stationNr = val;
            newStationFolder = path + "\\" + stationNr;
            Thread.Sleep(500); // Lägga till tid så metoden fungerar..? Verkar funka nu
            Directory.CreateDirectory(newStationFolder);

            //return HttpContext.Current.Session["value"].ToString();
            return stationNr;
        }
        public static void CopyTruckfolder(string stationFolder)
        {
            // TODO namn behöver tweakas beroende på path man läser ifrån
            sourceFolder = Settings.Default.ConfigsPath + "\\" + "mall";
            Directory.CreateDirectory(stationFolder);
            sourceFile = Path.Combine(sourceFolder, defaultFileName);
            destFile = Path.Combine(stationFolder, defaultFileName);
            File.Copy(sourceFile, destFile, true);
        }
        public static void CopyTruckfolder(string stationFolder, string truckFolder)
        {
            // TODO namn behöver tweakas beroende på path man läser ifrån
            sourceFolder = Settings.Default.ConfigsPath + "\\" + "mall";
            string dest = stationFolder + "\\" + truckFolder;
            Directory.CreateDirectory(dest);
            sourceFile = Path.Combine(sourceFolder, defaultFileName);
            var defaultFileNameNew = truckFolder + ".txt";
            destFile = Path.Combine(dest, defaultFileNameNew);
            File.Copy(sourceFile, destFile, true);
            //return sourceFile;
        }

    }

}