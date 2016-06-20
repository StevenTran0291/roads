using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Roads.Controllers
{
    public class MachineController : Controller
    {
        // GET: Machine
        Models.RoadsEntities1 obj = new Models.RoadsEntities1();
        Models.ViewModel obj2 = new Models.ViewModel();

        public ActionResult Index()
        {
            obj2.listMach = obj.tblMaches.ToList();

            var owner = obj.tblOwners.ToList();
            ViewBag.owners = new SelectList(owner, "Owner_Num", "Owner_Name");

            return View(obj2);
        }

        [HttpPost]
        public ActionResult Insert(Models.tblMach machine)
        {
            obj.tblMaches.Add(machine);
            obj.SaveChanges();
            
            return RedirectToAction("Index");
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Models.ViewModel machine)
        {
            Models.tblMach old = new Models.tblMach();

            old.Mach_No = machine.Mach_No;
            old.Mach_Desc = machine.Mach_Desc;
            old.Lease_Rate = machine.Lease_Rate;
            var name = obj.tblOwners.Where(m => m.Owner_Name == machine.Owner_Name).FirstOrDefault();
            old.Owner_Num = name.Owner_Num;
            old.Active = machine.Active;

            obj.Entry(old).State = EntityState.Modified;
            obj.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Index2() //// gridview get
        {
            //Models.RoadViewModel rvm = new Models.RoadViewModel();
            //Models.RoadsEntities obj = new Models.RoadsEntities();
            obj2.listRoad = obj.tblRoads.ToList();

            List<Models.tblType> items = obj.tblTypes.ToList();
            ViewBag.sel = new SelectList(items, "Type_Id", "Type_Desc");

            return View(obj2);
        }

        [HttpPost]
        public ActionResult Index3(Models.tblRoad r)
        {   ////insert
            //Models.RoadViewModel rvm = new Models.RoadViewModel();
            //Models.RoadsEntities obj = new Models.RoadsEntities();
            //Models.tblRoad tblr = new Models.tblRoad();
            //tblr.BIA_No =Convert.ToInt32(r.BIA_No);
            //tblr.Road_Name = r.Road_Name;
            //tblr.Type_Id = r.Type_Id;
            //tblr.Miles = r.Miles;

            obj.tblRoads.Add(r);
            obj.SaveChanges();
            return RedirectToAction("Index2");
        }

        [HttpPost]
        public ActionResult Edit2(Models.ViewModel r)
        {
            //Models.RoadsEntities obj = new Models.RoadsEntities();
            Models.tblRoad tblr = new Models.tblRoad();

            tblr.BIA_No = Convert.ToInt32(r.BIA_No);
            tblr.Road_Name = r.Road_Name;
            tblr.Type_Id = r.Type_Id;
            tblr.Miles = r.Miles;

            obj.Entry(tblr).State = EntityState.Modified;
            obj.SaveChanges();
            return RedirectToAction("Index2");
        }

    }
}