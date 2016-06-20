using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roads.Controllers
{
    public class RoadsController : Controller
    {
        // GET: Roads
        public ActionResult Index(string username, string password)
        {

            Models.RoadsEntities1 obj = new Models.RoadsEntities1();

            Models.tblLogin User = obj.tblLogins.Where(m => m.LoginID == username && m.Password == password).FirstOrDefault();
            if(User != null)
            {
                Response.Redirect("~/Home/Index");
            }

            return View();
        }
    }
}