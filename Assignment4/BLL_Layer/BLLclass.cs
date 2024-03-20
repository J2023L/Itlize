using BLL_Layer.Models;
using DAL_Layer;
using DAL_Layer.Models;
using System.Collections.Generic;

namespace BLL_Layer
{
    public class BLLclass
    {
        UserDB userDB = new UserDB();

        public void AddUser(UserBLL user)
        {
            UserDAL userDAL = ConvertToDAL(user);
            userDB.AddUser(userDAL);
        }

        public List<UserBLL> GetAllUsers()
        {
            List<UserDAL> userDALs = userDB.GetUsers();
            List<UserBLL> userBLLs = ConvertToBLL(userDALs);
            return userBLLs;
        }

        public List<UserBLL> GetUsers(string UserN)
        {
            var userDALs = userDB.GetUserInfo(UserN);
            List<UserBLL> userBLLs = ConvertToBLL(userDALs);
            return userBLLs;
        }

        private List<UserBLL> ConvertToBLL(List<UserDAL> users)
        {
            List<UserBLL> userBLLs = new List<UserBLL>();
            foreach (var user in users)
            {
                userBLLs.Add(new UserBLL
                {
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    Password = user.Password,
                    ConfirmPW = user.ConfirmPW
                });
            }
            return userBLLs;
        }

        private UserDAL ConvertToDAL(UserBLL user)
        {
            return new UserDAL
            {
                UserName = user.UserName,
                EmailAddress = user.EmailAddress,
                Password = user.Password,
                ConfirmPW = user.ConfirmPW
            };
        }
    }
}
