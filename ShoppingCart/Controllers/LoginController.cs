using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Models;
using ShoppingCart.DB;
using System.Web.Helpers;

namespace ShoppingCart.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string username, string password)
        {
            if (username == null || password==null)
                return View();

            User user = UserData.GetUserName(username,password);
            if (user.UserName == null || user.UserName=="")
            {
                TempData["Data"] = "Password is incorrect";
                return View();
            }
            if(user.UserName!=null && user.LastName == null)
            {
                TempData["Data"] = user.UserName;
                return View();
            }
            string sessionId = SessionData.CreateSession(user.Id);
            Session["user"] = sessionId;
            Session["id"] = user.Id;
            ViewData["user"] = user;

            return RedirectToAction("Index", "Product"); // will change to HC's page : return RedirectToAction("ViewProduct","Product", new {sessionId});           
        }

        public ActionResult Logout()
        {
            string session =(string) Session["user"];
            SessionData.RemoveSession(session);
            SessionData.RemoveSession(Session["user"] as string);
            SessionData.RemoveSession(Session["count"] as string);
            SessionData.RemoveSession(Session["addlist"] as string);
            Session["user"] = null;
            return RedirectToAction("Login", "Login");
        }
    }
}