using Microsoft.Ajax.Utilities;
using RadioWebConfig.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class AddVehicle : System.Web.UI.Page
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        string path = Settings.Default.ConfigsPath;
        public string stationNo = "";
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
            GetQueryString();
        }

        public void GetQueryString()
        {
            stationNo = Request.QueryString["name"];
        }
        protected void AddTruck_Click(object sender, EventArgs e)
        {
            try
            { 
                string station = Request.QueryString["name"];
                var oldTruck = Request.QueryString["Truck"];
                var truckNo =  Request["TruckNoTB"];

                bool exist = CheckIfTruckAlreadyExist(station, truckNo);

                if(exist == true)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "stationAlreadyExists", "alert('Stationsnummer finns redan');", true);

                }
                else
                {
                Stations.CopyTruckfolder(station, truckNo, oldTruck);
                var redirectLocation = Page.ResolveUrl("Default.aspx?name=" + station + "&Truck=" + truckNo);
                var title = "Fordon";
                var message = "Fordon tillagt";
                var scriptTemplate = "alert('{0}','{1}'); location.href='{2}'";
                var script = string.Format(scriptTemplate, message, title, redirectLocation);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "truckAdded", script, true);
                }
            }
           
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        private bool CheckIfTruckAlreadyExist(string station, string truckNo)
        {
            var path = Settings.Default.ConfigsPath + "\\" + station;
            var files = Directory.EnumerateDirectories(path);
            bool exist = false;

            foreach (var file in files)
            {
                if (file.Contains(truckNo))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
    }
    }
