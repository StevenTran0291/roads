//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Roads.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblMach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblMach()
        {
            this.tblTransactions = new HashSet<tblTransaction>();
        }
    
        public int Mach_No { get; set; }
        public string Mach_Desc { get; set; }
        public Nullable<double> Lease_Rate { get; set; }
        public Nullable<int> Owner_Num { get; set; }
        public bool Active { get; set; }
    
        public virtual tblOwner tblOwner { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblTransaction> tblTransactions { get; set; }
    }
}
