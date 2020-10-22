using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class AddStation : System.Web.UI.Page
    {
        string connection = "";


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
            var stationNo = Request["stationNoTB"];
            Stations.AddNewStation(stationNo);

            var redirectLocation = Page.ResolveUrl("ShowTrucks.aspx?name=" + stationNo);
            var title = "Station";
            var message = "Station tillagd";
            var scriptTemplate = "alert('{0}','{1}'); location.href='{2}'";
            var script = string.Format(scriptTemplate, message, title, redirectLocation);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "stationAdded", script, true);
        }
    }
}