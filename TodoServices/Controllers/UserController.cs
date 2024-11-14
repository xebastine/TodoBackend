using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoServices.Interfaces;
using TodoServices.Models;

namespace TodoServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userservice;
        public UserController(IUserService userservice)
        {
            _userservice = userservice;
        }

        [Authorize]
        [HttpGet]
        [Route("getuserlist")]
        public ActionResult Get()
        {
            try
            {
                var userlist = _userservice.GetUserList();
                if (userlist != null)
                {
                    return StatusCode(200, userlist);
                }
                else
                    return StatusCode(404,"No user exists");
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("createuser")]
        public ActionResult Post(string username)
        {
            try
            {
                string msg = _userservice.CreateUser(username);
                return StatusCode(200, msg);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Route("deleteuser")]
        public ActionResult Delete(int id)
        {
            try
            {
                string msg = _userservice.DeleteUser(id);
                if (msg == "User Deleted")
                {
                    return StatusCode(200, msg);
                }
                else
                    return StatusCode(404, msg);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

    }
}
