using ST1.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ST1.Controllers
{
    public class HomeController : Controller
    {
        public List<Users> usersList = new List<Users>();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginView()
        {
            return View("Login");
        }
        [HttpPost]
        public ActionResult Login(Users model)
        {
            Users user = new Users();
            try
            {
                using (testEntities db = new testEntities())
                {
                    usersList = db.Users.ToList();
                }
            }
            catch (Exception)
            {
                ViewBag.NotValidUser = $"User does not exists.";
            }
            user = usersList.Where(x => x.UserName == model.UserName && x.Password == model.Password).First();
            if (user != null)
                return View("LoginSuccess");
            return View("Login");
        }
        public ActionResult Waiting()
        {
            return View("Waiting");
        }
        public ActionResult Warning()
        {
            return View("Warning");
        }
        public ActionResult Register()
        {
            return View("Register");
        }
        [HttpPost]

        public ActionResult RegisterAction(Users model)
        {
            Users user = new Users();
            model.Status = 0;
            try
            {
                using (testEntities db = new testEntities())
                {
                    usersList = db.Users.ToList();
                }
            }
            catch (Exception e)
            {
                ViewBag.NotValidUser = $"{e.Message}";
            }
            if (usersList.Where(x => x.UserName == model.UserName || x.Email == model.Email).Count() == 0)
            {
                try
                {
                    using (testEntities db = new testEntities())
                    {
                        model.Status = 0;
                        db.Users.Add(model);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    ViewBag.NotValidUser = $"Something gone wrong. Try again later.";
                }
                return RedirectToAction(nameof(Waiting));
            }
            ViewBag.NotValidUser = $"User already exists.";
            return View("Register");
        }
    }
}