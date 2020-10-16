using RadioWebConfig.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            // TODO inloggningsvalidering

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



        protected void AddTruck_Click(object sender, EventArgs e)
        {
            try
            { 
            string station = Request.QueryString["name"];
            var truckNo =  Request["TruckNoTB"];
            var stationPath = Settings.Default.ConfigsPath + "\\" + station;
            Stations.CopyTruckfolder(stationPath, truckNo);

                var redirectLocation = Page.ResolveUrl("ShowTrucks.aspx?name=" + station);
                var title = "Fordon";
                var message = "Fordon tillagt";
                var scriptTemplate = "alert('{0}','{1}'); location.href='{2}'";
                var script = string.Format(scriptTemplate, message, title, redirectLocation);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "stationAdded", script, true);

            }
           
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

            

        }
    }
