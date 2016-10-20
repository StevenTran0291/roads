using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Roads.Models
{
    public class ViewModel
    {
        public Nullable<int> Mach_No { get; set; }
        public string Mach_Desc { get; set; }
        public Nullable<double> Lease_Rate { get; set; }
        public Nullable<int> Owner_Num { get; set; }
        public bool Active { get; set; }
        public string Owner_Name { get; set; }

        public List<tblMach> listMach { get; set; }

        public Nullable<int> BIA_No { get; set; }
        public string Road_Name { get; set; }
        public Nullable<int> Type_Id { get; set; }
        public Nullable<double> Miles { get; set; }

        public List<tblRoad> listRoad { get; set; }
    }
}