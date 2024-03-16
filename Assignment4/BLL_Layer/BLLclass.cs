using DAL_Layer;
using DAL_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Layer
{
    public class BLLclass
    {
        UserDAL userDAL = new UserDAL();

        public void AddUser(User user)
        {
            userDAL.AddUser(user);
        }

        public List<User> GetAllUsers()
        {
            List<User> userAll = userDAL.GetUsers();
            return userAll;
        }

        public List<User> GetUsers(string UserN)
        {
            var userList = userDAL.GetUserInfo(UserN);
            return userList;   
        }

    }
}
