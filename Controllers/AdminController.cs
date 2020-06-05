using MaxsPetCare.DAL;
using MaxsPetCare.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace MaxsPetCare.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly AdminUtil AdminUtil = new AdminUtil();
        private readonly AccountUtil AccountUtil = new AccountUtil();

        public AdminController()
        {
            General general = new General();
            int ContactUs = general.CountByArgs("contact_us", "seen = 0");
            int Consulting = general.CountByArgs("consulting", "seen = 0");
            int Training = general.CountByArgs("training", "seen = 0");
            int Boarding = general.CountByArgs("boarding", "seen = 0");

            if (ContactUs > 0)
            {
                ViewBag.PendingContactUS = $"<span class=\"badge badge-secondary float-right\">{ContactUs} New</span>";
            }
            
            if (Consulting > 0)
            {
                ViewBag.PendingConsulting = $"<span class=\"badge badge-secondary float-right\">{Consulting} New</span>";
            }
            
            if (Training > 0)
            {
                ViewBag.PendingTraining = $"<span class=\"badge badge-secondary float-right\">{Training} New</span>";
            }
            
            if (Boarding > 0)
            {
                ViewBag.PendingBoarding = $"<span class=\"badge badge-secondary float-right\">{Boarding} New</span>";
            }
            
        }

        public ActionResult Index()
        {
            General general = new General();

            ViewBag.AllContactUs = general.Count("contact_us");
            ViewBag.AllConsulting = general.Count("consulting");
            ViewBag.AllTraining = general.Count("training");
            ViewBag.AllBoarding = general.Count("boarding");

            return View();
        }
        
        public ActionResult ContactUs()
        {
            List<ContactUs> list = AdminUtil.AllContactUs();
            AdminUtil.UpdateStatusToSeen("contact_us");
            return View(list);
        }
        
        public ActionResult Consulting()
        {
            List<Consulting> list = AdminUtil.AllConsulting();
            AdminUtil.UpdateStatusToSeen("consulting");
            return View(list);
        }
        
        public ActionResult ConsultingStatus(int ID, int Status)
        {
            if (AdminUtil.UpdateConsulting(ID, Status))
            {
                Session["Flash_Success"] = "Status updated successfully!";
            }
            else
            {
                Session["Flash_Error"] = "Status update failed!<br>Please try again later";
            }
            return RedirectToAction("Consulting");
        }

        public ActionResult Training()
        {
            List<Training> list = AdminUtil.AllTrainings();
            AdminUtil.UpdateStatusToSeen("training");
            return View(list);
        }
        
        public ActionResult TrainingPackages()
        {
            return View(AdminUtil.AllPackages());
        }
        
        public ActionResult AddTrainingPackages()
        {
            ViewBag.Title = "Add Training Packages";
            TrainingPackages training = new TrainingPackages();
            return View("_PackageForm", training);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrainingPackages(TrainingPackages trainingPackages)
        {
            if (AdminUtil.AddPackage(trainingPackages))
            {
                Session["Flash_Success"] = "Package added successfully!";
            }
            else
            {
                Session["Flash_Error"] = "Package add failed!<br>Please try again later";
            }
            return RedirectToAction("TrainingPackages");
        }
        
        public ActionResult EditTrainingPackages(int ID)
        {
            ViewBag.Title = "Update Training Packages";
            return View("_PackageForm", AdminUtil.GetPackageByID(ID));
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrainingPackages(TrainingPackages trainingPackages)
        {
            if (AdminUtil.UpdatePackage(trainingPackages))
            {
                Session["Flash_Success"] = "Package updated successfully!";
            }
            else
            {
                Session["Flash_Error"] = "Package updated failed!<br>Please try again later";
            }
            return RedirectToAction("TrainingPackages");
        }
        
        public ActionResult Boarding()
        {
            List<Boarding> list = AdminUtil.AllBoardings();
            AdminUtil.UpdateStatusToSeen("boarding");
            return View(list);
        }
        
        public ActionResult Users()
        {
            return View(AccountUtil.AllUsers());
        }
        
        public ActionResult AddUsers()
        {
            ViewBag.Title = "Add User";
            return View("_UserForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUsers(Users users)
        {
            users.DateTime = DateTime.Now;
            if (AccountUtil.AddUser(users))
            {
                Session["Flash_Success"] = "User added successfully!";
            }
            else
            {
                Session["Flash_Error"] = "User add failed!<br>Please try again later";
            }
            return RedirectToAction("Users");
        }
        
        public ActionResult EditUsers(int ID)
        {
            ViewBag.Title = "Update User";
            ViewBag.HidePass = true;
            return View("_UserForm", AccountUtil.GetUserByID(ID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUsers(Users users)
        {
            if (AccountUtil.UpdateUser(users))
            {
                Session["Flash_Success"] = "User updated successfully!";
            }
            else
            {
                Session["Flash_Error"] = "User update failed!<br>Please try again later";
            }
            return RedirectToAction("Users");
        }
        
        public ActionResult Admins()
        {
            return View(AccountUtil.AllAdmins((int)Session["AdminID"]));
        }

        public ActionResult AddAdmin()
        {
            ViewBag.Title = "Add Admin";
            return View("_AdminForm");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAdmin(Admin admin)
        {
            if (AccountUtil.AddAdmin(admin))
            {
                Session["Flash_Success"] = "Admin added successfully!";
            }
            else
            {
                Session["Flash_Error"] = "Admin add failed!<br>Please try again later";
            }
            return RedirectToAction("Admins");
        }

        public ActionResult EditAdmin(int ID)
        {
            ViewBag.Title = "Update Admin";
            ViewBag.HidePass = true;
            return View("_AdminForm", AccountUtil.GetAdminByID(ID));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdmin(Admin admin)
        {
            if (AccountUtil.UpdateAdmin(admin))
            {
                Session["Flash_Success"] = "Admin updated successfully!";
            }
            else
            {
                Session["Flash_Error"] = "Admin update failed!<br>Please try again later";
            }
            return RedirectToAction("Admins");
        }

        public ActionResult MyAccount()
        {
            return View(AccountUtil.GetAdminByID((int)Session["AdminID"]));
        }
        
        public ActionResult Gallery()
        {
            return View(AdminUtil.AllImages());
        }

        public ActionResult AddGalleryImage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGalleryImage(Gallery gallery)
        {
            string ImgUrl = string.Empty;
            try
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + gallery.Image.FileName;
                if (gallery.Image != null)
                {
                    string path = Server.MapPath("/Images/Gallery/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    gallery.Image.SaveAs(path + uniqueFileName);
                    ImgUrl = uniqueFileName;
                }
            }
            catch
            { }

            gallery.ImgURL = ImgUrl;

            if (AdminUtil.AddImage(gallery))
            {
                Session["Flash_Success"] = "Image added successfully!";
            }
            else
            {
                Session["Flash_Error"] = "Image add failed!<br>Please try again later";
            }
            return RedirectToAction("Gallery");
        }
        
        public ActionResult DeleteGalleryImg(int ID)
        {
            AdminUtil.DeleteImage(ID);
            Session["Flash_Success"] = "Image deleted successfully!";
            return RedirectToAction("Gallery");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyAccount(Admin admin)
        {
            admin.ID = (int)Session["AdminID"];
            if (AccountUtil.UpdateAdmin(admin))
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

            Admin admin = AccountUtil.GetAdminByID(Convert.ToInt32(Session["AdminID"]));

            if (admin.Password == OldPassword)
            {
                if (AccountUtil.UpdateAdminPassword(NewPassword, admin.ID))
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
            return RedirectToAction("AdminLogin", "Account");
        }

    }
}