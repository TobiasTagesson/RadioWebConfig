using Microsoft.Ajax.Utilities;
using RadioWebConfig.Properties;
using System;
using System.Collections.Generic;
using System.Data;
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
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static string path { get; } = Settings.Default.ConfigsPath;
        public static string stationNr;
        public static string newStationFolder;
        public static string newTruckFolder;
        public static string defaultFileName = "";
        public static string sourceFile = "";
        public static string destFile = "";
        public static string sourceFolder = "";

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

            GetDirectories();
            AdminHidden.Value = user.Role.ToString();
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
                        lbl.Text += $"<div class='stationdiv'><a class='stations' href='ShowTrucks.aspx?name={Path.GetFileName(dir)}' >" + Path.GetFileName(dir) + $"</a><a onclick='DeleteStation(\"{Path.GetFileName(dir)}\");' <i id='trashcan' class='fa fa-trash' title='Radera station' /></a></div>";
                    }
                }
                // TODO Är du kund visas dina stationer (som är samma som username) Hur blir det om man har fler än en station?
                else
                {
                     foreach (string dir in dirs.Where(x => x.Equals(path + "\\" + user.UserName)))
                     {
                         lbl.Text += $"<div class='stationdiv'><a class='stations' href='ShowTrucks.aspx?name={Path.GetFileName(dir)}' >" + Path.GetFileName(dir) + $"</a><a onclick='DeleteStation(\"{Path.GetFileName(dir)}\");' <i id='trashcan' class='fa fa-trash' title='Radera station' /></a></div>";
                     }
                }
                if (dirs.Length <= 0)
                {
                    lbl.Text = "Hittade inga filer";
                }
            }
            catch (Exception ex)
            {
                lbl.Text = "Något blev fel";
                _logger.Error(ex.ToString());
            }
        }

        public static string AddNewStation(string val)
        {
            HttpContext.Current.Session["value"] = val; 

            stationNr = val;
            newStationFolder = path + "\\" + stationNr;
            Directory.CreateDirectory(newStationFolder);

            return stationNr;
        }
      
        public static void CopyTruckfolder(string stationFolder, string truckFolder, string oldTruckNo)
        {
            string dest = Settings.Default.ConfigsPath + "\\" + stationFolder + "\\" + truckFolder;

            if (oldTruckNo == "createNew")
            {
                sourceFile = Settings.Default.ConfigDefaultTemplate;
            }
            else
            {
            sourceFolder = Settings.Default.ConfigsPath + "\\" + stationFolder + "\\" + oldTruckNo;
            defaultFileName = oldTruckNo + ".txt";
            sourceFile = Path.Combine(sourceFolder, defaultFileName);
            }
            Directory.CreateDirectory(dest);
            var defaultFileNameNew = truckFolder + ".txt";
            destFile = Path.Combine(dest, defaultFileNameNew);
            File.Copy(sourceFile, destFile, true);
        }

        [WebMethod]
        [ScriptMethod]
        public static string DeleteStation(string val)
        {
            string stationNr = val;
            string stationPath = Settings.Default.ConfigsPath + "\\" + stationNr;
            Directory.Delete(stationPath, true);
            // Respons?

            return stationNr;
        }

    }



}