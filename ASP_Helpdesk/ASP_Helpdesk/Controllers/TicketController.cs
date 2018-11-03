using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASP_Helpdesk.Controllers
{
    public class TicketController : Controller
    {
        // GET: Ticket
        HelpDeskEntities3 db = new HelpDeskEntities3();
        public ActionResult Index()
        {
            return View(db.Tickets.ToList());
        }
        public ActionResult IndexWaiting()
        {
            return View(db.Tickets.Where(x => x.Status == "On Waiting").ToList());
        }
        public ActionResult IndexProgress()
        {
            return View(db.Tickets.Where(x => x.Status == "On Progress").ToList());
        }
        public ActionResult IndexHold()
        {
            return View(db.Tickets.Where(x => x.Status == "On Hold").ToList());
        }
        public ActionResult IndexFinish()
        {
            return View(db.Tickets.Where(x => x.Status == "Close").ToList());
        }
        public ActionResult IndexTeknisi()
        {
            var t = Session["Email"].ToString();
            var tek = db.Tickets.Where(x => x.Technician == t).ToList();
           
            return View(tek);
        }
        public ActionResult IndexClient()
        {
            var t = Session["Id"].ToString();
            var client = db.Tickets.Where(x => x.users_Id.ToString() == t).ToList();
            return View(client);
        }

        public JsonResult GetSubList(int Categories_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<SubCategory> SubList = db.SubCategories.Where(x => x.Categories_Id == Categories_Id).ToList();
            return Json(SubList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            List<User> ClientList = db.Users.Where(x => x.Roles_Id== 3).ToList();
            ViewBag.ClientList = new SelectList(ClientList, "Id", "FirstName");

            List<User> TechList = db.Users.Where(x => x.Roles_Id == 2).ToList();
            ViewBag.TechList = new SelectList(TechList, "Email", "Email");

            List<Category> CatList = db.Categories.ToList();
            ViewBag.CatList = new SelectList(CatList, "Id", "Name");

            List<DueDate> TypeList = db.DueDates.ToList();
            ViewBag.TypeList = new SelectList(TypeList, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ViewModel.TiketViewModel TVM)
        {
           

            if (ModelState.IsValid && TVM.DueDates_Id == 3)
            {
                var addtiket = new Ticket();

                
                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;
                addtiket.OnProgressDate = DateTime.Now;
                addtiket.DueDate = addtiket.OnProgressDate.AddDays(5);
                addtiket.Status = "On Progress";
                addtiket.DueDates_Id = TVM.DueDates_Id;
                addtiket.SubCategories_Id = Convert.ToInt32(TVM.Sub_Categories_Id);
                addtiket.users_Id = Convert.ToInt32(TVM.users_Id);
                try
                {
                    db.Tickets.Add(addtiket);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Console.Write(ex.StackTrace);
                    Console.Write(ex.InnerException);
                }

                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid && TVM.DueDates_Id == 4)
            {
                var addtiket = new Ticket();


                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;
                addtiket.OnProgressDate = DateTime.Now;
                addtiket.DueDate = addtiket.OnProgressDate.AddDays(7);
                addtiket.Status = "On Progress";
                addtiket.DueDates_Id = TVM.DueDates_Id;
                addtiket.SubCategories_Id = Convert.ToInt32(TVM.Sub_Categories_Id);
                addtiket.users_Id = Convert.ToInt32(TVM.users_Id);
                try
                {
                    db.Tickets.Add(addtiket);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Console.Write(ex.StackTrace);
                    Console.Write(ex.InnerException);
                }

                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid && TVM.DueDates_Id == 5)
            {
                var addtiket = new Ticket();


                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;
                addtiket.OnProgressDate = DateTime.Now;
                addtiket.DueDate = addtiket.OnProgressDate.AddDays(15);
                addtiket.Status = "On Progress";
                addtiket.DueDates_Id = TVM.DueDates_Id;
                addtiket.SubCategories_Id = Convert.ToInt32(TVM.Sub_Categories_Id);
                addtiket.users_Id = Convert.ToInt32(TVM.users_Id);
                try
                {
                    db.Tickets.Add(addtiket);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                    Console.Write(ex.StackTrace);
                    Console.Write(ex.InnerException);
                }

                return RedirectToAction("Index");
            }

            return View(TVM);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket tiket = db.Tickets.Find(id);
            if (tiket == null)
            {
                return HttpNotFound();
            }

            

            var toview = new ViewModel.TiketViewModel(tiket);
            return View(toview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ViewModel.TiketViewModel TVM)
            {
            if (ModelState.IsValid && TVM.Status == "On Waiting")
            {

                var addtiket = db.Tickets.Where(x => x.Id.ToString() == TVM.Id.ToString()).SingleOrDefault();

                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;

                addtiket.OnWaitingDate = DateTime.Now;
                addtiket.Status = TVM.Status;

                var getsub = db.SubCategories.Find(Convert.ToInt16(TVM.Sub_Categories_Id));

                if (getsub != null)
                {
                    addtiket.SubCategory = getsub;
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid && TVM.Status == "On Hold")
            {

                var addtiket = db.Tickets.Where(x => x.Id.ToString() == TVM.Id.ToString()).SingleOrDefault();

                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;
                
                addtiket.OnHoldingDate = DateTime.Now;
                addtiket.Status = TVM.Status;

                var getsub = db.SubCategories.Find(Convert.ToInt16(TVM.Sub_Categories_Id));

                if (getsub != null)
                {
                    addtiket.SubCategory = getsub;
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("Index");
            }
            else if (ModelState.IsValid && TVM.Status == "Close")
            {

                var addtiket = db.Tickets.Where(x => x.Id.ToString() == TVM.Id.ToString()).SingleOrDefault();

                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;
                addtiket.ResolvedDate = DateTime.Now;
                addtiket.Status = TVM.Status;

                var getsub = db.SubCategories.Find(Convert.ToInt16(TVM.Sub_Categories_Id));

                if (getsub != null)
                {
                    addtiket.SubCategory = getsub;
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(TVM);
        }






        public ActionResult EditTek(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket tiket = db.Tickets.Find(id);
            if (tiket == null)
            {
                return HttpNotFound();
            }



            var toview = new ViewModel.TiketViewModel(tiket);
            return View(toview);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTek(ViewModel.TiketViewModel TVM)
        {
            if (ModelState.IsValid && TVM.Status == "On Waiting")
            {

                var addtiket = db.Tickets.Where(x => x.Id.ToString() == TVM.Id.ToString()).SingleOrDefault();

                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;

                addtiket.OnWaitingDate = DateTime.Now;
                addtiket.Status = TVM.Status;

                var getsub = db.SubCategories.Find(Convert.ToInt16(TVM.Sub_Categories_Id));

                if (getsub != null)
                {
                    addtiket.SubCategory = getsub;
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("IndexTeknisi");
            }
            else if (ModelState.IsValid && TVM.Status == "On Hold")
            {

                var addtiket = db.Tickets.Where(x => x.Id.ToString() == TVM.Id.ToString()).SingleOrDefault();

                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;

                addtiket.OnHoldingDate = DateTime.Now;
                addtiket.Status = TVM.Status;

                var getsub = db.SubCategories.Find(Convert.ToInt16(TVM.Sub_Categories_Id));

                if (getsub != null)
                {
                    addtiket.SubCategory = getsub;
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("IndexTeknisi");
            }
            else if (ModelState.IsValid && TVM.Status == "Close")
            {

                var addtiket = db.Tickets.Where(x => x.Id.ToString() == TVM.Id.ToString()).SingleOrDefault();

                addtiket.Description = TVM.Description;
                addtiket.Technician = TVM.Techinician;
                addtiket.AdminBy = TVM.AdminBy;
                addtiket.ResolvedDate = DateTime.Now;
                addtiket.Status = TVM.Status;

                var getsub = db.SubCategories.Find(Convert.ToInt16(TVM.Sub_Categories_Id));

                if (getsub != null)
                {
                    addtiket.SubCategory = getsub;
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("IndexTeknisi");
            }

            return View(TVM);
        }







        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket tiket = db.Tickets.Find(id);
            if (tiket == null)
            {
                return HttpNotFound();
            }
            return View(tiket);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket tiket = db.Tickets.Find(id);
            db.Tickets.Remove(tiket);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket tiket = db.Tickets.Find(id);
            if (tiket == null)
            {
                return HttpNotFound();
            }
            return View(tiket);
        }

    }

    
}