using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RadioWebConfig
{
    public partial class Admin : System.Web.UI.Page
    {
        string connection = "";
        //string userName = "";
        //string passw = "";
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["myUser"] == null)
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

        [WebMethod]
        protected void AddUser_Click(object sender, EventArgs e)
        {
           
            var pw = Request.Form["pwInputName"].ToString();

            string role = "";
            using (SqlConnection conn = new SqlConnection(connection))
            {
                //userName = userInput.Value;
                //passw = pwInput.Value;

                // TODO: Lägg till funktionalitet Confirm Password-fältet

                // Radiobuttons
                if(Request.Form["selectuser"] != null)
                {
                    role = Request.Form["selectuser"].ToString();
                }

                // TODO: Byt namn på roller(kund, kundadmin, admin..osv?) och se till att de får tillgång till det de ska.

                bool exists = false;
                conn.Open();
                
                // Check if username already exists
                using (SqlCommand cmd = new SqlCommand("select count(*) from [Users] where username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("username", userInput.Value);
                    exists = (int)cmd.ExecuteScalar() > 0;
                }

                // if exists, show a error message
                if (exists)
                {
                    userNameExists.Visible = true;
                    //Response.Write("Användarnamnet finns redan");
                }
                else
                {

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [Users] values (@username, @password, @role)", conn))
                    {
                        cmd.Parameters.AddWithValue("username", userInput.Value);
                        cmd.Parameters.AddWithValue("password", pw);
                        cmd.Parameters.AddWithValue("role", role);


                        cmd.ExecuteNonQuery();
                    }
                }
                conn.Close();

            }
        }
        [WebMethod]
        protected void DeleteUser_Click(object sender, EventArgs e)
        {
            //TODO: Checka inputvariabler för whitespace osv för sql injection
            //TODO: Se till att deletemetoden inte körs förrän popup-fönstret besvarats.
            //TODO: Men hur skickar man ett returvärde från javascript till CS?
            using (SqlConnection conn = new SqlConnection(connection))
            {
                 var commandtext = "select count(*) from[Users] where username = @username";

                bool exists = false;
                conn.Open();

                // Check if username already exists
                using (SqlCommand cmd = new SqlCommand(commandtext, conn))
                {
                    // TODO deleteUerInput måste ha runat server för att komma åt variabeln här, men kan inte ha det i JS-filen för då blir den null. Hmm
                    cmd.Parameters.AddWithValue("username", deleteUserInput.Value);
                    exists = (int)cmd.ExecuteScalar() > 0;
                }

                // if exists, show a error message
                if (!exists)
                {
                    errorMessage2.Visible = true;
    
                }
                else
                {
                    string confirmValue = Request.Form["confirm_value"];
                    if (confirmValue == "Ja")
                    {
                        var commandtextDelete = "DELETE FROM [Users] WHERE username = @username";

                        using (SqlCommand cmd = new SqlCommand(commandtextDelete, conn))
                        {
                            cmd.Parameters.AddWithValue("username", deleteUserInput.Value);

                            cmd.ExecuteNonQuery();
                        }

                        conn.Close();
                         this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Användare raderad!')", true);
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Användaren raderades ej!')", true);
                    }



                   // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "showalert", "DeleteUserMessageBox('" + deleteUserInput.Value + "');", true);
                    
                  
                }
                

            }


        }
        //public void OnConfirm(object sender, EventArgs e)
        //{
        //    string confirmValue = Request.Form["confirm_value"];
        //    if (confirmValue == "Ja")
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked YES!')", true);
        //    }
        //    else
        //    {
        //        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
        //    }
        //}
    }
}