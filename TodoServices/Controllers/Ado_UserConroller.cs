using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Data.SqlClient;
using TodoServices.Models;

namespace TodoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ado_UserConroller : ControllerBase
    {
        [HttpGet]
        [Route("/ado_GetUserList")]
        public ActionResult Get()
        {
            SqlConnection con = null;
            con = new SqlConnection("data source=.; database=TODO; integrated security=SSPI");
            con.Open();
            SqlCommand cm = new SqlCommand("Select * from todoschema.userlist", con);
            SqlDataReader sdr = cm.ExecuteReader();
            List<UserList> ul = new List<UserList>();
            while (sdr.Read())
            {
                ul.Add(new UserList
                {
                    UserName = sdr["UserName"].ToString(),
                    UserId = (int)sdr["UserId"]
                });
            }
            con.Close();
            return StatusCode(200, ul);
        }

        [HttpPost]
        [Route("/ado_CreateUser")]
        public ActionResult Post(string Username)
        {
            string username = Username;
            SqlConnection con = null;
            con = new SqlConnection("data source=.; database=TODO; integrated security=SSPI");
            SqlCommand cm = new SqlCommand($"Insert into todoschema.userlist(UserName) values (@username)", con);

            cm.Parameters.AddWithValue("@username", username);
            try
            {
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete]
        [Route("/ado_DeleteUser")]
        public ActionResult Delete(int userid)
        {
            SqlConnection con = null;
            con = new SqlConnection("data source=.; database=TODO; integrated security=SSPI");
            SqlCommand cm = new SqlCommand("Delete from todoschema.userlist where userid = @userid", con);
            cm.Parameters.AddWithValue("@userid", userid);
            try
            {
                con.Open();
                cm.ExecuteNonQuery();
                con.Close();
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }

}