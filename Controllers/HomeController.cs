using MaxsPetCare.Models;
using MaxsPetCare.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxsPetCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeUtil HomeUtil = new HomeUtil();

        private string UserAddress()
        {
            AccountUtil accountUtil = new AccountUtil();
            if (Session["UserID"] != null)
            {
                return accountUtil.GetUserByID((int)Session["UserID"]).Address;
            }
            else
            {
                return null;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactUs(ContactUs contactUs)
        {
            contactUs.DateTime = DateTime.Now;
            contactUs.Seen = 0;
            if (HomeUtil.AddContactUs(contactUs))
            {
                Session["Flash_Success"] = "Message received!<br>Thank you for contacting us";
            }
            else
            {
                Session["Flash_Error"] = "Message sent failed!<br>Please try again later";
            }            
            return RedirectToAction("ContactUs");
        }

        
        public ActionResult Consulting()
        {
            ViewBag.Address = UserAddress();
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Consulting(Consulting consulting)
        {
            consulting.UserID = Convert.ToInt32(Session["UserID"]);
            consulting.Status = 0;
            consulting.Seen = 0;
            consulting.DateTime = DateTime.Now;

            if (HomeUtil.AddConsulting(consulting))
            {
                Session["Flash_Success"] = "Consulting added successfully!";
                return RedirectToAction("MyConsulting");
            }
            else
            {
                Session["Flash_Error"] = "Consulting add failed!<br>Please try again later";
                return View();
            }
        }

        [UserAuthorize]
        public ActionResult MyConsulting()
        {
            return View(HomeUtil.AllConsultingsByUser((int) Session["UserID"]));
        }

        public ActionResult Training()
        {
            ViewBag.Address = UserAddress();
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Training(Training training)
        {
            training.UserID = Convert.ToInt32(Session["UserID"]);
            training.Seen = 0;
            training.DateTime = DateTime.Now;
            if (HomeUtil.AddTraining(training))
            {
                Session["Flash_Success"] = "Pet Training added successfully!";
                return RedirectToAction("MyTraining");
            }
            else
            {
                Session["Flash_Error"] = "Pet Training add failed!<br>Please try again later";
                return View();
            }
        }

        [UserAuthorize]
        public ActionResult MyTraining()
        {
            return View();
        }
        
        public ActionResult Boarding()
        {
            ViewBag.Address = UserAddress();
            return View();
        }

        [UserAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Boarding(Boarding boarding)
        {
            boarding.UserID = Convert.ToInt32(Session["UserID"]);
            boarding.Seen = 0;
            boarding.DateTime = DateTime.Now;
            if (HomeUtil.AddBoarding(boarding))
            {
                Session["Flash_Success"] = "Boarding added successfully!";
                return RedirectToAction("MyBoarding");
            }
            else
            {
                Session["Flash_Error"] = "Boarding add failed!<br>Please try again later";
                return View();
            }
        }

        [UserAuthorize]
        public ActionResult MyBoarding()
        {
            return View(HomeUtil.AllBoardingsByUser((int)Session["UserID"]));
        }

        [UserAuthorize]
        public ActionResult MyAccount()
        {
            AccountUtil accountUtil = new AccountUtil();
            return View(accountUtil.GetUserByID((int)Session["UserID"]));
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session["Flash_Success"] = "You have been logged out successfully!";
            return RedirectToAction("Index", "Home");
        }

    }
}