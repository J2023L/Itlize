using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL_Layer;
using DAL_Layer.Models;

namespace Registration.Controllers
{
    public class RegistrationController : Controller
    {
        BLLclass Bll = new BLLclass();
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
                Bll.AddUser(user);
                return RedirectToAction("UserList");
            }
            return View("Register", user);
        }

        [HttpGet]
        public ActionResult UserList() 
        {
            List<User> user = Bll.GetAllUsers();
            return View(user);
        }

        public ActionResult GetUserInfo(string UserN)
        {
            var userList = Bll.GetUsers(UserN);
            return View("UserList", userList);
        }
    }
}