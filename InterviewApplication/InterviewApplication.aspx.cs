using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CIS325_Master_Project.InterviewApplication
{
    public partial class InterviewApplication : System.Web.UI.Page
    {
        public DataTable userData = new DataTable();

        protected string connectionString;

        protected SqlConnection connection;

        protected string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            SubmitButton.Visible = true;
            UpdateButton.Visible = false;
            DeleteButton.Visible = false;

            //validate the page query string
            if (Page.ClientQueryString.Length > 0 && (!Page.ClientQueryString.Contains("ID") || !Page.ClientQueryString.Contains("Action")))
            {
                Response.Redirect("InterviewMain.aspx");
            }

            if (Page.ClientQueryString.Contains("ID"))
            {
                id = Request.QueryString["ID"]; 
                connectionString = ConfigurationManager.ConnectionStrings["Interview"].ConnectionString;
                connection = new SqlConnection(connectionString);

                GetData(Request.QueryString["ID"]);

                if(Request.QueryString["Action"] == "Edit")
                {
                    UpdateButton.Visible = true;
                }

                if (Request.QueryString["Action"] == "Delete")
                {
                    SubmitButton.Visible = false;
                    DeleteButton.Visible = true;
                    DeleteMsg.Visible = true;
                    MainFormSection.Visible = false;
                    DeleteMsg.Text = "Are you sure you wish to delete the Interview for " + userData.Rows[0]["FirstName"] + " " + userData.Rows[0]["LastName"] + "?"; 
                }

                if (Request.QueryString["Action"] == "View" || Request.QueryString["Action"] == "Edit")
                {
                    SubmitButton.Visible = false;

                    DataRow row = userData.Rows[0];
                    IName.Text = row["IName"].ToString();
                    IPosition.Text = row["IPosition"].ToString();
                    IDate.Text = Convert.ToDateTime(row["IDate"]).ToString("yyyy-MM-dd");
                    LastName.Text = row["LastName"].ToString();
                    FirstName.Text = row["FirstName"].ToString();
                    BusinessKnowledge.Text = row["BusinessKnowledge"].ToString();
                    IComments.Text = row["Comments"].ToString();

                    //listing out every communication possible from the checkbox list
                    foreach (ListItem ability in Communication.Items)
                    {
                        if (ability.Text == row["Communication"].ToString())
                        {
                            ability.Selected = true;
                        }
                    }

                    //listing out every communication possible from the checkbox list
                    if (row["Skills"].ToString().Length > 0)
                    {
                        string[] currentSkills = row["Skills"].ToString().Split(',');
                        foreach (ListItem skill in Skills.Items)
                        {
                            if (currentSkills.Contains(skill.Text))
                            {
                                skill.Selected = true;
                            }
                        }
                    }

                    if (Request.QueryString["Action"] == "View")
                    {
                        IName.ReadOnly = true;
                        IPosition.ReadOnly = true;
                        IDate.ReadOnly = true;
                        LastName.ReadOnly = true;
                        FirstName.ReadOnly = true;
                        BusinessKnowledge.ReadOnly = true;
                        IComments.ReadOnly = true;

                        foreach (ListItem item in Communication.Items)
                        {
                            item.Enabled = false;
                        }
                        foreach (ListItem item in Skills.Items)
                        {
                            item.Enabled = false;
                        }
                    }
                }
            }
        }
        protected void GetData(string id)
        {
            using (connection)
            {
                connection.Open();

                string selectData = "Select * FROM Interview WHERE AppID = @ID";

                using (var cmd = new SqlCommand(selectData, connection))
                {
                    cmd.Parameters.AddWithValue("ID", Convert.ToInt32(id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    userData.Load(reader);
                }

                connection.Close();
            }
        } 

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            // Gather the data and insert into the database
            Page.Validate();   //force validation for database input (best practices)
            if (Page.IsValid)
            {
                connectionString = ConfigurationManager.ConnectionStrings["Interview"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        //construct SQL statements
                        string sqlQuery = "INSERT INTO Interview" +
                            "(IName, IPosition, IDate, LastName, FirstName, Communication, Skills, BusinessKnowledge, Comments) VALUES" +
                            "(@IName, @IPosition, @IDate, @LastName, @FirstName, @Communication, @Skills, @BusKnow, @Comments)";

                        using (var cmdSQL = new SqlCommand(sqlQuery, connection))
                        {
                            cmdSQL.Parameters.AddWithValue("IName", Request.Form["IName"].ToString());
                            cmdSQL.Parameters.AddWithValue("IPosition", Request.Form["IPosition"].ToString());
                            cmdSQL.Parameters.AddWithValue("IDate", Request.Form["IDate"].ToString());
                            cmdSQL.Parameters.AddWithValue("LastName", Request.Form["LastName"].ToString());
                            cmdSQL.Parameters.AddWithValue("FirstName", Request.Form["FirstName"].ToString());
                            cmdSQL.Parameters.AddWithValue("Communication", Request.Form["Communication"].ToString());
                            cmdSQL.Parameters.AddWithValue("Skills", FormatSkills());
                            cmdSQL.Parameters.AddWithValue("BusKnow", Request.Form["BusinessKnowledge"].ToString());
                            cmdSQL.Parameters.AddWithValue("Comments", Request.Form["IComments"].ToString());

                            cmdSQL.ExecuteNonQuery();
                        }

                        //DON'T FORGET TO CLOSE CONN
                        connection.Close();

                        ResultMsg.Text = "Your application is submitted successfully! Please <a href=\"InterviewMain.aspx\">click here</a> to view the results!";
                        InterviewForm.Visible = false;

                    }
                    catch (SqlException exception)
                    {//if there is a run time error
                        ErrorMsg.Text = exception.Message + " | " + exception.Number;
                        throw;
                    }
                }                
            }
        }

        protected string FormatSkills()
        {
            string skills = "";

            foreach (ListItem skill in Skills.Items )
            {
                if (skill.Selected)
                {
                    skills += skill.Text + ","; 
                }

            }
                return skills.Substring (0, skills.Length - 1);
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            // Gather the data and insert into the database
            Page.Validate();   //force validation for database input (best practices)
            if (Page.IsValid)
            {
                connectionString = ConfigurationManager.ConnectionStrings["Interview"].ConnectionString;
            
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        //construct SQL statements
                        string sqlQuery = "UPDATE Interview SET " +
                        "IName = @IName," +
                        "IPosition = @IPosition," +
                        "IDate = @IDate," +
                        "LastName = @LastName," +
                        "FirstName = @FirstName," +
                        "Communication = @Communication," +
                        "Skills = @Skills," +
                        "BusinessKnowledge = @BusKnow," +
                        "Comments = @Comments " +
                        "WHERE AppID = @ID";

                        using (var cmdSQL = new SqlCommand(sqlQuery, connection))
                        {
                            cmdSQL.Parameters.AddWithValue("ID", id);
                            cmdSQL.Parameters.AddWithValue("IName", Request.Form["IName"].ToString());
                            cmdSQL.Parameters.AddWithValue("IPosition", Request.Form["IPosition"].ToString());
                            cmdSQL.Parameters.AddWithValue("IDate", Request.Form["IDate"].ToString());
                            cmdSQL.Parameters.AddWithValue("LastName", Request.Form["LastName"].ToString());
                            cmdSQL.Parameters.AddWithValue("FirstName", Request.Form["FirstName"].ToString());
                            cmdSQL.Parameters.AddWithValue("Communication", Request.Form["Communication"].ToString());
                            cmdSQL.Parameters.AddWithValue("Skills", FormatSkills());
                            cmdSQL.Parameters.AddWithValue("BusKnow", Request.Form["BusinessKnowledge"].ToString());
                            cmdSQL.Parameters.AddWithValue("Comments", Request.Form["IComments"].ToString());

                            cmdSQL.ExecuteNonQuery();
                        }


                        //DON'T FORGET TO CLOSE CONN
                        connection.Close();



                        ResultMsg.Text = "Your application is updated successfully! Please <a href=\"InterviewMain.aspx\">click here</a> to view the results!";
                        InterviewForm.Visible = false;

                    }
                    catch (SqlException exception)
                    {//if there is a run time error
                        ErrorMsg.Text = exception.Message + " | " + exception.Number;
                        throw;
                    }
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("InterviewMain.aspx");
        }

        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["Interview"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //construct SQL statements
                    string sqlQuery = "DELETE FROM Interview WHERE AppID = @ID";

                    using (var cmdSQL = new SqlCommand(sqlQuery, connection))
                    {
                        cmdSQL.Parameters.AddWithValue("ID", id);
                        cmdSQL.ExecuteNonQuery();
                    }


                    //DON'T FORGET TO CLOSE CONN
                    connection.Close();

                    ResultMsg.Text = "Your application was deleted successfully! Please <a href=\"InterviewMain.aspx\">click here</a> to return to the Main page!";
                    InterviewForm.Visible = false;
                }
                catch (SqlException exception)
                {//if there is a run time error
                    ErrorMsg.Text = exception.Message + " | " + exception.Number;
                    throw;
                }
            }
        }
    }
}