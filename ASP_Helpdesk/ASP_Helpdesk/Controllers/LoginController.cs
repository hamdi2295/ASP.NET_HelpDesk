using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_Helpdesk.Controllers
{
    
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _Adminn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Autherize(ASP_Helpdesk.User userModel)
        {
            using (HelpDeskEntities3 db = new HelpDeskEntities3())
            {
                var userDetails = db.Users.Where(x => x.Email == userModel.Email && x.Password == userModel.Password).FirstOrDefault();
                
                if (userDetails == null)
                {
                    return View("Index", userModel);
                } 
                else if (userDetails.Roles_Id == 1)
                {
                    Session["Id"] = userDetails.Id;
                    Session["Email"] = userDetails.Email;
                    Session["Role"] = userDetails.Roles_Id;

                    var id = Session["Role"];

                    return RedirectToAction("Index", "Home");
                }
                else if (userDetails.Roles_Id == 2)
                {
                    Session["Id"] = userDetails.Id;
                    Session["Email"] = userDetails.Email;
                    Session["Role"] = userDetails.Roles_Id;
                    return RedirectToAction("Index2", "Home");
                }
                else if (userDetails.Roles_Id == 3)
                {
                    Session["Id"] = userDetails.Id;
                    Session["Email"] = userDetails.Email;
                    Session["Role"] = userDetails.Roles_Id;
                    return RedirectToAction("Index2", "Home");
                }
            }
            return View();
        }
        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}