using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(loginTable u)
        {
            if (ModelState.IsValid)
            {
                using (LoginDatabaseEntities dc = new LoginDatabaseEntities())
                {
                    var v = dc.loginTables.Where(a => a.userName.Equals(u.userName) && a.password.Equals(u.password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserId"] = v.userId.ToString();
                        Session["LogedUserName"] = v.userName.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }
    }
}
