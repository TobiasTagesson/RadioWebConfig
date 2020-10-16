﻿using Microsoft.Ajax.Utilities;
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
    public partial class Admin2 : System.Web.UI.Page
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

        [WebMethod]
        protected void AddUser_Click(object sender, EventArgs e)
        {
            // passwordinput från client
            var pw = string.Empty;

            // Kolla otillåtna tecken
            try
            {
                inputEmptyField.Visible = false;
                userNameExists.Visible = false;
                userNameExists.Visible = false;
                pw = Request.Form["pwInputName"].ToString();
            }
            catch
            {
                forbiddenChars.Visible = true;
                inputEmptyField.Visible = false;
                userNameExists.Visible = false;
                return;
            }
            userInput.Value = userInput.Value.Trim();
            
            if (userInput.Value.Contains("\"") || userInput.Value.Contains("\'") || pw.Contains("\"") || pw.Contains("\'"))
            {
                forbiddenChars.Visible = true;
            }
            else if (userInput.Value.IsNullOrWhiteSpace() || pw.IsNullOrWhiteSpace())
            {
                inputEmptyField.Visible = true;
            }
            else
            {
                string role = "";
                using (SqlConnection conn = new SqlConnection(connection))
                {

                    // Radiobuttons
                    if (Request.Form["selectuser"] != null)
                    {
                        role = Request.Form["selectuser"].ToString();
                    }

                    // TODO: Se till att olika roller får tillgång till det de ska.

                    bool exists = false;
                    conn.Open();

                    // Check if username already exists
                    using (SqlCommand cmd = new SqlCommand("select count(*) from [Users] where username = @username", conn))
                    {
                        cmd.Parameters.AddWithValue("username", userInput.Value);
                        exists = (int)cmd.ExecuteScalar() > 0;
                    }

                    // if exists, show an error message
                    if (exists)
                    {
                        userNameExists.Visible = true;
                        userAdded.Visible = false;
                        inputEmptyField.Visible = false;
                        forbiddenChars.Visible = false;

                    }
                    else
                    {
                        userNameExists.Visible = false;
                        forbiddenChars.Visible = false;
                        inputEmptyField.Visible = false;

                        using (SqlCommand cmd = new SqlCommand("INSERT INTO [Users] values (@username, @password, @role)", conn))
                        {
                            cmd.Parameters.AddWithValue("username", userInput.Value);
                            cmd.Parameters.AddWithValue("password", pw);
                            cmd.Parameters.AddWithValue("role", role);

                            userAdded.Visible = true;
                            userNameExists.Visible = false;
                            forbiddenChars.Visible = false;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
        }

        [WebMethod]
        protected void DeleteUser_Click(object sender, EventArgs e)
        {
            //TODO: Checka inputvariabler för whitespace osv och för sql injection
            using (SqlConnection conn = new SqlConnection(connection))
            {
                var commandtext = "select count(*) from[Users] where username = @username";

                bool exists = false;
                conn.Open();

                // Check if username already exists
                using (SqlCommand cmd = new SqlCommand(commandtext, conn))
                {
                    cmd.Parameters.AddWithValue("username", deleteUserInput.Value);
                    exists = (int)cmd.ExecuteScalar() > 0;
                }

                // if exists, show an error message
                if (!exists)
                {
                    errorMessage2.Visible = true;

                }
                else
                {
                    errorMessage2.Visible = false;
                    // värde från client-popup
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
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Användare " + deleteUserInput.Value + " raderad!')", true);

                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Användaren raderades ej!')", true);
                        /* TODO (vid tabs) Efter att man klickat ok på "vill du ta bort användare" på en användare som inte finns i databasen så laddas sidan
                         * om och man kommer till "fel" tab och ser således inte meddelandet:
                         * "Hittar ingen användare med det namnet" Hur stannar man kvar på samma tab efter popup? Skita i tabbar? */
                    }
                }
            }
        }
    }
}