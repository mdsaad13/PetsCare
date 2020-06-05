using MaxsPetCare.Models;
using MaxsPetCare.DAL;
using System;
using System.Collections.Generic;
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
            AdminUtil adminUtil = new AdminUtil();
            return View(adminUtil.AllImages());
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
            }
            else
            {
                Session["Flash_Error"] = "Consulting add failed!<br>Please try again later";
            }
            return RedirectToAction("MyConsulting");
        }

        [UserAuthorize]
        public ActionResult MyConsulting()
        {
            return View(HomeUtil.AllConsultingsByUser((int) Session["UserID"]));
        }

        public ActionResult DeleteConsulting(int ID)
        {
            HomeUtil.DeleteConsulting(ID, (int)Session["UserID"]);
            Session["Flash_Success"] = "Consulting deleted successfully!";
            return RedirectToAction("MyConsulting");
        }

        public ActionResult Training()
        {
            ViewBag.Address = UserAddress();
            ViewBag.PackagesList = HomeUtil.ParticularPackages(1);
            return View();
        }

        [HttpPost]
        public string GetPackages(FormCollection formCollection)
        {
            int Type =  Convert.ToInt32(formCollection["PetType"]);
            List<TrainingPackages> list = HomeUtil.ParticularPackages(Type);
            string Data = "";
            foreach (var item in list)
            {
                Data += $"<option value=\"{item.ID}\">{item.Name}</option>";
            }
            return Data;
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
            }
            else
            {
                Session["Flash_Error"] = "Pet Training add failed!<br>Please try again later";
            }
            return RedirectToAction("MyTraining");
        }

        [UserAuthorize]
        public ActionResult MyTraining()
        {
            return View(HomeUtil.AllTrainingsByUser((int)Session["UserID"]));
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

        [UserAuthorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyAccount(Users users)
        {
            users.ID = (int)Session["UserID"];
            AccountUtil accountUtil = new AccountUtil();
            if (accountUtil.UpdateUser(users))
            {
                Session["Flash_Success"] = "Profile updated successfully!";
            }
            else
            {
                Session["Flash_Error"] = "Profile update failed!<br>Please try again later";
            }
            return RedirectToAction("MyAccount");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(FormCollection formCollection)
        {
            string OldPassword = Convert.ToString(formCollection["OldPassword"]);
            string NewPassword = Convert.ToString(formCollection["NewPassword"]);
            AccountUtil accountUtil = new AccountUtil();
            Users user = accountUtil.GetUserByID(Convert.ToInt32(Session["UserID"]));

            if (user.Password == OldPassword)
            {
                if (accountUtil.UpdateUserPassword(NewPassword, user.ID))
                {
                    Session["Flash_Success"] = "Password updated successfully!";
                }
                else
                {
                    Session["Flash_Error"] = "Password update failed!<br>Please try again later";
                }
            }
            else
            {
                Session["Flash_Error"] = "Incorrect Password";
            }

            return RedirectToAction("MyAccount");
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