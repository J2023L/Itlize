using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Registration.Models;
using Registration.DAL;

namespace Registration.Controllers
{
    public class RegistrationController : Controller
    {
        UserDAL userDAL = new UserDAL();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View("Register");
        }


        [HttpPost]
        public ActionResult RegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                userDAL.AddUser(user);
                return RedirectToAction("UserList");
            }
            return View("Register", user);
        }

        [HttpGet]
        public ActionResult UserList()
        {
            List<User> user = userDAL.GetUsers();
            return View(user);
        }

        public ActionResult GetUserInfo(string UserN)
        {
            var userList = userDAL.GetUserInfo(UserN);
            return View("UserList", userList);
        }
    }
}