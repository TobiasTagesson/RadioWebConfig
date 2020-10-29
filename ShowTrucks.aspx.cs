using Microsoft.Ajax.Utilities;
using RadioWebConfig.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class ShowTrucks : System.Web.UI.Page
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

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
           // SqlConnection();

            GetTrucks();
        }

        public string station;
        private void GetTrucks()
        {
            string path = Settings.Default.ConfigsPath;
            station = Request.QueryString["name"];
            var truckPath = path + "\\" + station;
            string[] dirs = Directory.GetDirectories(truckPath);

            //dubbelkolla att det är en giltig kod
            if(truckPath.IsNullOrWhiteSpace() || truckPath.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                Response.Redirect("Default.aspx");
            }
            Session.Add("TruckPath", truckPath);    

            try
            {
                foreach (string dir in dirs)
                {
                    lbl.Text += $"<div class='truckdiv'>{Path.GetFileName(dir)}<a class='trucks' href='Default.aspx?name={station}&Truck={Path.GetFileName(dir)}'>Redigera</a><a class='trucks' id='saveAs' href='AddVehicle?name={station}&Truck={Path.GetFileName(dir)}' >Skapa Kopia</a><a onclick='DeleteTruck(\"{station}\", \"{Path.GetFileName(dir)}\");' <i id='trashcanTruck' class='fa fa-trash' title='Radera fordon'/></a></div>"; ;
                }
                if (dirs.Length <= 0)
                {
                    lbl.Text = "Inga filer att visa";
                }

            }
            catch (Exception ex)
            {
                lbl.Text = "Något blev fel";
                _logger.Error(ex.ToString());
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static string DeleteTruck(string station, string truck)
        {
            string stationNo = station;
            string truckNo = truck;
            string truckPath = Settings.Default.ConfigsPath + "\\" + stationNo + "\\" + truckNo;
            Directory.Delete(truckPath, true);
            // Respons?

            return truckNo;
        }
    }


}