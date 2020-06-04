using MaxsPetCare.DAL;
using MaxsPetCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxsPetCare.Controllers
{
    [IsAuthorized]
    public class AccountController : Controller
    {
        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(Admin admin)
        {
            AccountUtil account = new AccountUtil();
            if (account.AdminLogin(admin.Email, admin.Password))
            {
                Session["Flash_Success"] = "Login Success";
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                Session["Error"] = "Incorrect email or password";
                return View();
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users users)
        {
            AccountUtil account = new AccountUtil();
            if (account.UserLogin(users.Email, users.Password))
            {
                Session["Flash_Success"] = "Login Success";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["Error"] = "Incorrect email or password";
                return View();
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users users)
        {
            users.DateTime = DateTime.Now;
            AccountUtil account = new AccountUtil();
            if (account.AddUser(users))
            {
                Session["Success"] = "Account successfully created!<br>Kindly login";
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Session["Flash_Error"] = "An error occured<br>Kindly try again later";
                return View();
            }
        }

    }
}