using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Roads.Models;

namespace Roads.Controllers
{
    [OutputCache(Duration = 100)]
    public class MachineController : Controller
    {
        // GET: Machine
        Models.RoadsEntities1 obj = new Models.RoadsEntities1();
        Models.ViewModel obj2 = new Models.ViewModel();
        Models.TransVM tvm = new Models.TransVM();

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

            old.Mach_No = (int)machine.Mach_No;
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

        public ActionResult Trans()
        {
            List<SelectListItem> list1 = new List<SelectListItem>();
            List<SelectListItem> list2 = new List<SelectListItem>();
            List<SelectListItem> list3 = new List<SelectListItem>();
            List<SelectListItem> list4 = new List<SelectListItem>();

            foreach (Models.tblEmp a in obj.tblEmps)
            {
                list1.Add(new SelectListItem()
                {
                    Text = a.Emp_Name,
                    Value = a.Emp_no.ToString()
                });
            }

            foreach(Models.tblMach a in obj.tblMaches)
            {
                list2.Add(new SelectListItem()
                {
                    Text = a.Mach_Desc,
                    Value = a.Mach_No.ToString()
                });
            }

            foreach(Models.tblRoad a in obj.tblRoads)
            {
                list3.Add(new SelectListItem()
                {
                    Text = a.Road_Name,
                    Value = a.BIA_No.ToString()
                });
            }

            foreach(Models.tblAct a in obj.tblActs)
            {
                list4.Add(new SelectListItem()
                {
                    Text = a.Activity_Desc,
                    Value = a.Activity_Code.ToString()
                });
            }

            tvm.Emp_List = list1;
            tvm.Mach_List = list2;
            tvm.BIA_List = list3;
            tvm.Activity_List = list4;

            return View(tvm);
        }

        public ActionResult Create_Trans(Models.tblTransaction trans)
        {
            obj.tblTransactions.Add(trans);
            obj.SaveChanges();

            return RedirectToAction("Trans");
        }


        public ActionResult Kendo()
        {
            return View();
        }

        public ActionResult tblTransactions_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblTransaction> tbltransactions = obj.tblTransactions;
            DataSourceResult result = tbltransactions.ToDataSourceResult(request, tblTransaction => new {
                //AutoNumber = tblTransaction.AutoNumber,
                //Trans_Date = tblTransaction.Trans_Date,
                Emp_No = tblTransaction.Emp_No,
                Mach_No = tblTransaction.Mach_No,
                BIA_No = tblTransaction.BIA_No,
                Activity_Code = tblTransaction.Activity_Code,
                Hours = tblTransaction.Hours,
                Lease_Chg = tblTransaction.Lease_Chg,
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult tblTransactions_Update([DataSourceRequest]DataSourceRequest request, tblTransaction tblTransaction)
        {
            if (ModelState.IsValid)
            {
                var entity = new tblTransaction
                {
                    //AutoNumber = tblTransaction.AutoNumber,
                    //Trans_Date = tblTransaction.Trans_Date,
                    Emp_No = tblTransaction.Emp_No,
                    Mach_No = tblTransaction.Mach_No,
                    BIA_No = tblTransaction.BIA_No,
                    Activity_Code = tblTransaction.Activity_Code,
                    Hours = tblTransaction.Hours,
                    Lease_Chg = tblTransaction.Lease_Chg,
                };

                obj.tblTransactions.Attach(entity);
                obj.Entry(entity).State = EntityState.Modified;
                obj.SaveChanges();
            }

            return Json(new[] { tblTransaction }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            obj.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult tblEmp_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblEmp> tblEmps = obj.tblEmps;
            DataSourceResult result = tblEmps.ToDataSourceResult(request, tblEmp => new {
                Emp_Name = tblEmp.Emp_Name,
                Emp_No = tblEmp.Emp_no
            });

            return Json(result);
        }

        public ActionResult tblMach_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblMach> tblMachs = obj.tblMaches;
            DataSourceResult result = tblMachs.ToDataSourceResult(request, tblMach => new {
                Mach_Desc = tblMach.Mach_Desc,
                Mach_No = tblMach.Mach_No
            });

            return Json(result);
        }

        public ActionResult tblRoad_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblRoad> tblRoads = obj.tblRoads;
            DataSourceResult result = tblRoads.ToDataSourceResult(request, tblRoad => new {
                Road_Name = tblRoad.Road_Name,
                BIA_No = tblRoad.BIA_No
            });

            return Json(result);
        }

        public ActionResult tblAct_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblAct> tblActs = obj.tblActs;
            DataSourceResult result = tblActs.ToDataSourceResult(request, tblAct => new {
                Activity_Desc = tblAct.Activity_Desc,
                Activity_Code = tblAct.Activity_Code
            });

            return Json(result);
        }
    }
}