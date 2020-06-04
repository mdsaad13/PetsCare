using MaxsPetCare.DAL;
using MaxsPetCare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaxsPetCare.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        private readonly AdminUtil AdminUtil = new AdminUtil();
        private readonly AccountUtil AccountUtil = new AccountUtil();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ContactUs()
        {
            return View(AdminUtil.AllContactUs());
        }
        
        public ActionResult Consulting()
        {
            return View(AdminUtil.AllConsulting());
        }
        
        public ActionResult Training()
        {
            return View();
        }
        
        public ActionResult TrainingPackages()
        {
            return View(AdminUtil.AllPackages());
        }
        
        public ActionResult AddTrainingPackages()
        {
            ViewBag.Title = "Add Training Packages";
            return View("_PackageForm");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrainingPackages(TrainingPackages trainingPackages)
        {
            if (AdminUtil.AddPackage(trainingPackages))
            {
                Session["Flash_Success"] = "Package added successfully!";
                return RedirectToAction("TrainingPackages");
            }
            else
            {
                Session["Flash_Error"] = "Package add failed!<br>Please try again later";
                return View();
            }
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
                return RedirectToAction("TrainingPackages");
            }
            else
            {
                Session["Flash_Error"] = "Package updated failed!<br>Please try again later";
                return View();
            }
        }
        
        public ActionResult Boarding()
        {
            return View(AdminUtil.AllBoardings());
        }
        
        public ActionResult Users()
        {
            return View(AccountUtil.AllUsers());
        }
        
        public ActionResult Admins()
        {
            return View(AccountUtil.AllAdmins((int)Session["AdminID"]));
            //return View(AccountUtil.AllAdmins(1));
        }

    }
}