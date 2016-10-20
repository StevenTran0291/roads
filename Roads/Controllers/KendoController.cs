﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Roads.Models;

namespace Roads.Controllers
{
    public class KendoController : Controller
    {
        private RoadsEntities1 db = new RoadsEntities1();

        public ActionResult Kendo()
        {
            return View();
        }

        public ActionResult tblTransactions_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblTransaction> tbltransactions = db.tblTransactions;
            DataSourceResult result = tbltransactions.ToDataSourceResult(request, tblTransaction => new {
                AutoNumber = tblTransaction.AutoNumber,
                Trans_Date = tblTransaction.Trans_Date,
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
                    AutoNumber = tblTransaction.AutoNumber,
                    Trans_Date = tblTransaction.Trans_Date,
                    Emp_No = tblTransaction.Emp_No,
                    Mach_No = tblTransaction.Mach_No,
                    BIA_No = tblTransaction.BIA_No,
                    Activity_Code = tblTransaction.Activity_Code,
                    Hours = tblTransaction.Hours,
                    Lease_Chg = tblTransaction.Lease_Chg,
                };

                db.tblTransactions.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { tblTransaction }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult tblEmp_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblEmp> tblEmps = db.tblEmps;
            DataSourceResult result = tblEmps.ToDataSourceResult(request, tblEmp => new {
                Emp_Name = tblEmp.Emp_Name,
                Emp_No = tblEmp.Emp_no
            });

            return Json(result);
        }

        public ActionResult tblMach_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblMach> tblMachs = db.tblMaches;
            DataSourceResult result = tblMachs.ToDataSourceResult(request, tblMach => new {
                Mach_Desc = tblMach.Mach_Desc,
                Mach_No = tblMach.Mach_No
            });

            return Json(result);
        }

        public ActionResult tblRoad_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblRoad> tblRoads = db.tblRoads;
            DataSourceResult result = tblRoads.ToDataSourceResult(request, tblRoad => new {
                Road_Name = tblRoad.Road_Name,
                BIA_No = tblRoad.BIA_No
            });

            return Json(result);
        }

        public ActionResult tblAct_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<tblAct> tblActs = db.tblActs;
            DataSourceResult result = tblActs.ToDataSourceResult(request, tblAct => new {
                Activity_Desc = tblAct.Activity_Desc,
                Activity_Code = tblAct.Activity_Code
            });

            return Json(result);
        }
    }
}
