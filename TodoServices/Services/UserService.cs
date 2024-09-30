using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TodoServices.Interfaces;
using TodoServices.Models;

namespace TodoServices.Services
{
    public class UserService : IUserService
    {
        public TodoContext _todocontext;
        public UserService(TodoContext context)
        {
            _todocontext = context;
        }

        public List<UserList> GetUserList()
        {
            var userlist = _todocontext.UserLists.ToList();
            return userlist;
        }

        public string CreateUser(string username)
        {
            UserList userlist = new UserList();
            userlist.UserName = username;
            _todocontext.Add(userlist);
            _todocontext.SaveChanges();
            string msg = "User Created";
            return msg;
        }

        public string DeleteUser(int id)
        {
            var user = _todocontext.UserLists.Where(a => a.UserId == id).FirstOrDefault();
            if (user != null)
            {
                _todocontext.UserLists.Remove(user);
                _todocontext.SaveChanges();
                string msg = "User Deleted";
                return msg;
            }
            else
            {
                string msg = "User not Found";
                return msg;
            }
        }
    }
}
