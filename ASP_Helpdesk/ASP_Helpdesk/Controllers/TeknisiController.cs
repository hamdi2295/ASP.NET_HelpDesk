using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_Helpdesk.Controllers
{
    public class TeknisiController : Controller
    {
        HelpDeskEntities3 db = new HelpDeskEntities3();
        // GET: Teknisi
        public ActionResult Index()        
        {
            var tek = Session["Id"];
            return View(db.Tickets.Where(x => x.users_Id.ToString() == tek.ToString()).ToList());
        }
    }
}