using RadioWebConfig.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class AddStation : System.Web.UI.Page
    {
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
        }

        protected void AddStation_Click(object sender, EventArgs e)
        {
            var stationNo = Request["stationNoTB"];
            bool exist = CheckIfStationAlreadyExist(stationNo);

            if (exist == true)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "stationAlreadyExists", "alert('Stationsnummer finns redan');", true);
            }
            else 
            { 
            Stations.AddNewStation(stationNo);
            var redirectLocation = Page.ResolveUrl("ShowTrucks.aspx?name=" + stationNo);
            var title = "Station";
            var message = "Station tillagd";
            var scriptTemplate = "alert('{0}','{1}'); location.href='{2}'";
            var script = string.Format(scriptTemplate, message, title, redirectLocation);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "stationAdded", script, true);
            }
        }

        private bool CheckIfStationAlreadyExist(string stationNo)
        {
            var path = Settings.Default.ConfigsPath;
            var files = Directory.EnumerateDirectories(path);
            bool exist = false;

            foreach (var file in files)
            {
                if (file.Contains(stationNo))
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }
    }
}