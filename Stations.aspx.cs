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
        //public static string defaultFileName = "mall.txt";
        public static string defaultFileName = "";
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
            //if (user.Role != 1)
            //{
            //    Response.Redirect("~/Default.aspx");
            //    return;
            //}
            SqlConnection();

            GetDirectories();
            AdminHidden.Value = user.Role.ToString();
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
            var user = Session["myUser"] as LoggedInUser;
            string path = Settings.Default.ConfigsPath;
          
            try
            {
                // Rensar listningen så de nya inte skrivs ut under de gamla vid ny page_load
                lbl.Text = "";
                string[] dirs = Directory.GetDirectories(path);

                // Är du admin visas alla stationer
                if(user.Role == 1)
                {
                    foreach (string dir in dirs)
                    {
                        lbl.Text += $"<div class='stationdiv'><a class='stations' href='ShowTrucks.aspx?name={Path.GetFileName(dir)}' >" + Path.GetFileName(dir) + "</a></div>";

                    }
                }
                // TODO Är du kund visas dina stationer (som är samma som username) Hur blir det om man har fler än en station?
                else
                    {

                    foreach (string dir in dirs.Where(x => x.Equals(path + "\\" + user.UserName)))
                    {
                        lbl.Text += $"<div class='stationdiv'><a class='stations' href='ShowTrucks.aspx?name={Path.GetFileName(dir)}' >" + Path.GetFileName(dir) + "</a></div>";

                    }
                }
                if (dirs.Length <= 0)
                {
                    lbl.Text = "Hittade inga filer";
                }

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
            // TODO Den här metoden är kaos. Funkar ibland och ibland inte. Ingen krasch men ingen mapp skapas om jag inte breakpointar mig igenom den. Verkar funka vid Thread.Sleep-addering
            //Validering av tecken
            //if (val.IsNullOrWhiteSpace() || val.IndexOfAny(Path.GetInvalidPathChars()) >= 0) // TODO vad ska hända här?
                HttpContext.Current.Session["value"] = val; 

            stationNr = val;
            newStationFolder = path + "\\" + stationNr;
            Thread.Sleep(500); // Lägga till tid så metoden fungerar..? Verkar funka nu
            Directory.CreateDirectory(newStationFolder);

            return stationNr;
        }
      
        public static void CopyTruckfolder(string stationFolder, string truckFolder, string oldTruckNo)
        {
            // TODO namn behöver tweakas beroende på path man läser ifrån
            // vid skapa ny måste stationfolder heta "mall" annars heter den det som skickas in i metoden
            string dest = Settings.Default.ConfigsPath + "\\" + stationFolder + "\\" + truckFolder;

            // TODO för knasig funktion nedan?
            if (oldTruckNo == "mall")
            {
                sourceFolder = Settings.Default.ConfigsPath + "\\" + "mall";
            }
            else
            {
            sourceFolder = Settings.Default.ConfigsPath + "\\" + stationFolder + "\\" + oldTruckNo;
            }
            Directory.CreateDirectory(dest);
            defaultFileName = oldTruckNo + ".txt";
            sourceFile = Path.Combine(sourceFolder, defaultFileName);
            var defaultFileNameNew = truckFolder + ".txt";
            destFile = Path.Combine(dest, defaultFileNameNew);
            File.Copy(sourceFile, destFile, true);
        }

    }

}