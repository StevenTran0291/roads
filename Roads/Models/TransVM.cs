using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roads.Models
{
    public class TransVM
    {
        public int AutoNumber { get; set; }
        public Nullable<System.DateTime> Trans_Date { get; set; }
        public Nullable<int> Emp_No { get; set; }
        public Nullable<int> Mach_No { get; set; }
        public Nullable<int> BIA_No { get; set; }
        public Nullable<int> Activity_Code { get; set; }
        public Nullable<double> Hours { get; set; }
        public Nullable<double> Lease_Chg { get; set; }

        public List<SelectListItem> Emp_List { get; set; }
        public List<SelectListItem> Mach_List { get; set; }
        public List<SelectListItem> BIA_List { get; set; }
        public List<SelectListItem> Activity_List { get; set; }
    }
}