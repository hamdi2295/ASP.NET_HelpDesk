using System;
using ASP_Helpdesk.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Net;


namespace ASP_Helpdesk.Controllers
{
    public class UsersController : Controller
    {
        HelpDeskEntities3 db = new HelpDeskEntities3();

        
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Create()
        {
            List<Province> ProvList = db.Provinces.ToList();
            ViewBag.ProvList = new SelectList(ProvList, "Id", "Name");
            List<Department> DepList = db.Departments.ToList();
            ViewBag.DepList = new SelectList(DepList, "Id", "Name");

            var roles = db.Roles.OrderBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = false
            }).ToArray();
            ViewBag.Roles_Id = roles;
            return View();
        }
        public JsonResult GetRegencyList(int Provinces_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Regency> KabList = db.Regencies.Where(x => x.Province.Id == Provinces_Id).ToList();
            return Json(KabList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDistrictList(int Regencies_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<District> KecList = db.Districts.Where(x => x.Regencies_Id == Regencies_Id).ToList();
            return Json(KecList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetVillageList(int Districts_Id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Village> VilList = db.Villages.Where(x => x.Districts_Id == Districts_Id).ToList();
            return Json(VilList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,UserName,Gender,Password,PhoneNumber,Address,PostNumber,Email,CreateDate,UpdateDate,DeleteDate,IsDelete,Departments_Id,Villages_Id,Roles_Id")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepList = new SelectList(db.Departments, "Id", "Name", user.Departments_Id);
            ViewBag.Roles_Id = new SelectList(db.Roles, "Id", "Name", user.Roles_Id);
            ViewBag.Villages_Id = new SelectList(db.Villages, "Id", "Name", user.Villages_Id);
            return View(user);
        }




        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            User user = db.Users.Find(id);
                 
            if (user == null)
            {
                return HttpNotFound();
            }
            var province = db.Provinces.OrderBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = false
            }).ToArray();
            foreach (var get in province )
            {
                if (get.Value.Equals(user.Village.District.Regency.Province.Id.ToString()))
                {
                    get.Selected = true;
                    break;
                }
            }

            var regency = db.Regencies.OrderBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = false
            }).ToArray();
            foreach (var get in regency)
            {
                if (get.Value.Equals(user.Village.District.Regency.Id.ToString()))
                {
                    get.Selected = true;
                    break;
                }
            }
            var district = db.Districts.OrderBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = false
            }).ToArray();
            foreach (var get in district)
            {
                if (get.Value.Equals(user.Village.District.Id.ToString()))
                {
                    get.Selected = true;
                    break;
                }
            }
            var village = db.Villages.OrderBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = false
            }).ToArray();
            foreach (var get in village)
            {
                if (get.Value.Equals(user.Villages_Id.ToString()))
                {
                    get.Selected = true;
                    break;
                }
            }

            var role = db.Roles.OrderBy(x => x.Name).Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString(),
                Selected = false
            }).ToArray();
            foreach (var get in role)
            {
                if (get.Value.Equals(user.Roles_Id.ToString()))
                {
                    get.Selected = true;
                    break;
                }
            }

            ViewBag.ProvList = province;
            ViewBag.RegList = regency;
            ViewBag.DisList = district;
            ViewBag.VilList = village;
            ViewBag.RoleList = role;

            List<Department> DepList = db.Departments.ToList();
            ViewBag.DepList = new SelectList(DepList, "Id", "Name");
         
            

            var toview = new ProvRegViewModel(user);

            return View(toview);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProvRegViewModel UVM)
        {
            if (ModelState.IsValid)
            {

                var adduser = db.Users.Where(x => x.Id.ToString() == UVM.Id.ToString()).SingleOrDefault();
                adduser.Id = Convert.ToInt32(UVM.Id);
                adduser.FirstName = adduser.FirstName;
                adduser.LastName = adduser.LastName;
                adduser.UserName = adduser.UserName;
                adduser.Gender = adduser.Gender;
                adduser.Password = adduser.Password;
                adduser.PhoneNumber = adduser.PhoneNumber;
                adduser.Address = adduser.Address;
                adduser.PostNumber = adduser.PostNumber;
                adduser.Email = adduser.Email;
                adduser.Departments_Id = adduser.Departments_Id;
                adduser.Villages_Id = adduser.Villages_Id;
                adduser.Roles_Id = adduser.Roles_Id;

                var getdep = db.Departments.Find(Convert.ToInt16(UVM.Departments_Id));
                var getvil = db.Villages.Find(Convert.ToInt16(UVM.Villages_Id));
                var getrol = db.Roles.Find(Convert.ToInt16(UVM.Roles_Id));
                if (getdep != null && getvil != null && getrol != null)
                {
                    adduser.Department = getdep;
                    adduser.Village = getvil;
                    adduser.Role = getrol;
                    //db.Entry(majorVM).State = EntityState.Modified;
                    db.SaveChanges();

                }
                var result = db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(UVM);
        }



        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Faculties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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