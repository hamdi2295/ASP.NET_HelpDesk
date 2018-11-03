using System;
using ASP_Helpdesk.ViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASP_Helpdesk.Controllers
{
    public class SubCategoriesController : Controller
    {
        // GET: SubCategories
        HelpDeskEntities3 db = new HelpDeskEntities3();
       
        public ActionResult Index()
        {
            return View(db.SubCategories.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.Categories_Id = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreateDate,UpdateDate,DeleteDate,IsDelete,Categories_Id")] SubCategory subcategory)
        {
            if (ModelState.IsValid)
            {
                db.SubCategories.Add(subcategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Categories_Id = new SelectList(db.Categories, "Id", "Name", subcategory.Categories_Id);
            return View(subcategory);
        }





        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcategory = db.SubCategories.Find(id);
            if (subcategory == null)
            {
                return HttpNotFound();
            }
            var categories = db.Categories.OrderBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = false
            }).ToArray();
            foreach (var get in categories)
            {
                if (get.Value.Equals(subcategory.Category.Id.ToString()))
                {
                    get.Selected = true;
                    break;
                }
            }
            ViewBag.Categories_Id = categories;
            return View(subcategory);
        }

        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(subcatVM scVM)
        {
            if (ModelState.IsValid)
            {
                
                var addsub = db.SubCategories.Include("Category").Where(x => x.Id.ToString() == scVM.Id.ToString()).SingleOrDefault();
                addsub.Id = Convert.ToInt32(scVM.Id);
                addsub.Name = scVM.Name;
                


                var getcat = db.Categories.Find(Convert.ToInt16(scVM.Categories_Id ));
                if (getcat != null)
                {
                    addsub.Category= getcat;
                   
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(scVM);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategory subcat = db.SubCategories.Find(id);
            if (subcat == null)
            {
                return HttpNotFound();
            }
            return View(subcat);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubCategory subcat = db.SubCategories.Find(id);
            db.SubCategories.Remove(subcat);
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
    }
}