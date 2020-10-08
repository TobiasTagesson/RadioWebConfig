using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{

    public partial class LogIn : System.Web.UI.Page
    {
        string connection = "";
        public static string uid { get; set; } = "";
        string pass = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SQLConnection();
        }
        protected string SQLConnection()
        {
            SqlConnection conn = new SqlConnection(
            new SqlConnectionStringBuilder()
            {
                DataSource = "(localdb)\\MSSQLLocalDB",
                InitialCatalog = "WebAppUsers"
                //Authentication = SqlAuthenticationMethod.ActiveDirectoryIntegrated
                //UserID = "test",
                //Password = "test"
            }.ConnectionString
            );

            connection = conn.ConnectionString;

            return connection;
        }



        [WebMethod]
        //[ScriptMethod]
        protected void ValidateUser_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                uid = userInput.Value;
                pass = pwInput.Value;

                
                var commandtext = "SELECT * FROM dbo.Users WHERE username = @user AND password = @pw";

                SqlCommand sc = new SqlCommand(commandtext, con);

                sc.Parameters.AddWithValue("@user", userInput.Value);
                sc.Parameters.AddWithValue("@pw", pwInput.Value);

                con.Open();

                LoggedInUser user = new LoggedInUser();

                SqlDataReader reader = sc.ExecuteReader();

                if (reader.Read())
                {
                    user.UserName = reader.GetString(1);
                    user.Role = reader.GetInt32(3);
                
                    //create a cookie
                    HttpCookie myCookie = new HttpCookie("myCookie");

                    //Add key-values in the cookie
                    myCookie.Values.Add("userid", uid.ToString());
                    // add session
                    Session.Add("myUser", user);
                    //set cookie expiry date-time. Made it to last for next 12 hours.
                    myCookie.Expires = DateTime.Now.AddHours(12);
                    Session.Timeout = 720;

                    //Most important, write the cookie to client.
                    Response.Cookies.Add(myCookie);
                    Response.Redirect("Default.aspx");
                   // Response.Redirect("Stations.aspx");
                }
                else
                {
                    errorMessage.Visible = true;
                    
                }
                con.Close();
                
            }
            
        }

        
    } 
}