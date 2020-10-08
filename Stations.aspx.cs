using RadioWebConfig.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class Stations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GetDirectories();
        }
        private void GetDirectories()
        {
            string path = Settings.Default.ConfigsPath;
            
            //DataTable dt = new DataTable();
           // dt.Columns.Add("Folder", typeof(string));
            try
            {
                string[] dirs = Directory.GetDirectories(path);
                foreach (string dir in dirs)
                {
                    lbl.Text += $"<div><a href='ShowTrucks.aspx?name={Path.GetFileName(dir)}'>" + Path.GetFileName(dir) + "</a></div>";
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
    }
}