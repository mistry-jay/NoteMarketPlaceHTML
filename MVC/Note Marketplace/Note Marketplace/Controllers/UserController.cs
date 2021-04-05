using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Note_Marketplace.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [AllowAnonymous]
        public ActionResult FAQ()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
    }
}