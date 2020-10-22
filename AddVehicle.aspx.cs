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
        string connection = "";
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
            SqlConnection();
            GetQueryString();
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
             //TODO checka förbjudna tecken. Vad händer om det är förbjudna tecken? Min jQuery-validering borde räcka
                if (truckNo.IsNullOrWhiteSpace() || truckNo.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                {

                }
                    //var stationPath = Settings.Default.ConfigsPath + "\\" + station;

                    Stations.CopyTruckfolder(station, truckNo, oldTruck);

                var redirectLocation = Page.ResolveUrl("Default.aspx?name=" + station + "&Truck=" + truckNo);
                var title = "Fordon";
                var message = "Fordon tillagt";
                var scriptTemplate = "alert('{0}','{1}'); location.href='{2}'";
                var script = string.Format(scriptTemplate, message, title, redirectLocation);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "truckAdded", script, true);

            }
           
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

            

        }
    }
