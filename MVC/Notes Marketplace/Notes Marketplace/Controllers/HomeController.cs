using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Notes_Marketplace.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult signUp()
        {
            return View();
        }
        public ActionResult forgot_password()
        {
            return View();
        }
        public ActionResult faq()
        {
            return View();
        }
    }
}