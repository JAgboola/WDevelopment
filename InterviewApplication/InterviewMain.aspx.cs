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
    public partial class WebForm1 : System.Web.UI.Page
    {
        public DataTable userData = new DataTable();

        protected string connectionString;

        protected SqlConnection connection;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = ConfigurationManager.ConnectionStrings["Interview"].ConnectionString;
            connection = new SqlConnection(connectionString);

            string where = "";
            if (IsPostBack) 
            {
                if (Request.Form["filter"] == "Skills")
                {
                    where += " WHERE Skills LIKE @skill";
                }
                else
                {
                    where += " WHERE FirstName = @firstName AND LastName = @lastName";
                }
            }
            

            userData = GetData(where);
        }

        protected DataTable GetData(string whereParams)
        {
            DataTable dt = new DataTable();


            using (connection)
            {
                connection.Open();

                string selectData = "Select * FROM Interview ";

                if (whereParams != "")
                {
                    selectData += whereParams;
                }

                using (var cmd = new SqlCommand(selectData, connection))
                {
                    if (Request.Form["filter"] == "Skills")
                    {
                        cmd.Parameters.AddWithValue("skill", "%" + Request.Form["search"] + "%");
                    }
                    if (Request.Form["filter"] == "Name")
                    {
                        cmd.Parameters.AddWithValue("firstName",  Request.Form["search"].Split(',')[1].Trim());
                        cmd.Parameters.AddWithValue("lastName", Request.Form["search"].Split(',')[0].Trim());
                    }
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                }

                connection.Close();        
            }

            return dt;
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            
        }
    }
}