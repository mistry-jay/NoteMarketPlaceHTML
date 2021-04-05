using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Web.Security;
using Note_Marketplace.DBModels;

namespace Note_Marketplace.Controllers
{
    public class AccountController : Controller
    {
        NoteMarketplaceEntities db = new NoteMarketplaceEntities();
        // GET: Account
        private NoteMarketplaceEntities _Context;
        public AccountController()
        {
            _Context = new NoteMarketplaceEntities();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User userDbModel)
        {
            var connectionDB = new NoteMarketplaceEntities();
            string email = userDbModel.EmailID;
            if(IsValidEmail(email))
            {
                if(IsValidPassword(userDbModel.Password))
                {
                    var result = connectionDB.Users.Where(db => db.EmailID == email).FirstOrDefault();
                    if(result == null)
                    {
                        User obj = new User();
                        obj.FirstName = userDbModel.FirstName;
                        obj.LastName = userDbModel.LastName;
                        obj.EmailID = userDbModel.EmailID;
                        obj.Password = userDbModel.Password;
                        obj.IsEmailVerified = false;
                        obj.IsActive = true;
                        obj.ModifiedBy = null;
                        obj.ModifiedDate = null;
                        obj.CreatedDate = DateTime.Now;
                        obj.CreatedBy = null;
                        obj.RoleID = 103;

                        if (ModelState.IsValid)
                        {
                            db.Users.Add(obj);
                            try
                            {
                                // Your code...
                                // Could also be before try if you know the exception occurs in SaveChanges

                                db.SaveChanges();
                                ModelState.Clear();
                                FormsAuthentication.SetAuthCookie(userDbModel.EmailID, true);
                                return RedirectToAction("FAQ", "User");
                            }
                            catch (DbEntityValidationException e)
                            {
                                foreach (var eve in e.EntityValidationErrors)
                                {
                                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                                    foreach (var ve in eve.ValidationErrors)
                                    {
                                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                            ve.PropertyName, ve.ErrorMessage);
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        ViewBag.NotValidEmail = "Email is already exists";
                    }
                }
                else
                {
                    ViewBag.NotValidPassword = "Password and Re-enter password must be same";
                }
            }
            else
            {
                ViewBag.NotValidEmail = "Email is not valid";
            }
            return View("SignUp");
        }
        public static bool IsValidPassword(string pswd)
        {
            if (pswd != "")
            {
                return true;
            }
            return false;
        }
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

            public ActionResult Index()
        {
            return View();
        }
    }
}