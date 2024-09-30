using Microsoft.AspNetCore.Mvc;
using TodoServices.Models;

namespace TodoServices.Interfaces
{
    public interface IUserService
    {
        public List<UserList> GetUserList();
        public string CreateUser(string username);
        public string DeleteUser(int id);
    }
}
