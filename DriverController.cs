using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApplication1.Controllers
{
    public class DriverController : ApiController
    {
        // GET api/Customers/GetCustomersByOwnerCode?ownerCode=owner&userId=user&groupId=group
        [HttpGet]
        [Route("api/getDriver")]
        public string GetCustomersByOwnerCode(string username, string passord)
        {
            IList<object> customers = new List<object>();
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=infolog.dyndns.org,14331;Database=DEV;User ID=infolog;Password=emQIQ4gNb7x4Q9l;Trusted_Connection = True;Integrated Security=false;";

            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand cmd = new SqlCommand("check_credentials", cnn);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            cmd.CommandType = CommandType.StoredProcedure;
            DataSet data = new DataSet();
            DataTable dtContact = new DataTable("Contact_Object");
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            cmd.Parameters.Add(new SqlParameter("username", username));
            cmd.Parameters.Add(new SqlParameter("password", passord));
            da.Fill(dtContact);
          int   userId = Convert.ToInt32(cmd.ExecuteScalar());

            SqlDataReader rdr = cmd.ExecuteReader();
         
            if (rdr.HasRows)
            {
                rdr.Read();
                string active = rdr["active"].ToString();
             
                if (active=="true"  || active=="True")
                  return "active";
                else return "notactive";
            }
            else
            {
              return  "invalid";
            }
        }
    }

}