using DiplomaDataModel.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomaDataModel.Controllers
{
    public class HomeController : Controller
    {
        private DiplomaContext db = new DiplomaContext();

        public ActionResult Index()
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            var sId = db.Choices.ToList();

            if (user != null)
            {
                foreach (var id in sId)
                {
                    if (id.StudentId == user.UserName)
                    {

                        ViewBag.Message = "You've Successfully Registered";
                        return View();
                    }
                }
            }

            return View();
        }
    }
}