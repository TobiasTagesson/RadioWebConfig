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
    public partial class ShowTrucks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            GetTrucks();
            //dubbelkolla att det är en giltig kod
            // om station innehåller något av: System.IO.Path.GetInvalidPathChars
            //redirect till default.aspx

        }

        private void GetTrucks()
        {
            string path = Settings.Default.ConfigsPath;
            var station = Request.QueryString["name"];
            var truckPath = path + "\\" + station;
            string[] dirs = Directory.GetDirectories(truckPath);

            //DataTable dt = new DataTable();
            // dt.Columns.Add("Folder", typeof(string));
            try
            {
                foreach (string dir in dirs)
                {
                    lbl.Text += $"<div>{Path.GetFileName(dir)}<a href='Default.aspx?Station={station}&Truck={Path.GetFileName(dir)}'> Redigera </a><a href=''> Skapa Kopia </a></div>";
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
    }
}