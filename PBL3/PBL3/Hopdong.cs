//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PBL3
{
    using System;
    using System.Collections.Generic;
    
    public partial class Hopdong
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hopdong()
        {
            this.lichtaps = new HashSet<lichtap>();
        }
    
        public int id { get; set; }
        public Nullable<int> idKH { get; set; }
        public Nullable<int> idgoitap { get; set; }
        public Nullable<int> idstatus { get; set; }
        public Nullable<System.DateTime> ngayki { get; set; }
        public Nullable<System.DateTime> ngayhethan { get; set; }
        public Nullable<int> idpt { get; set; }
    
        public virtual GoiTap GoiTap { get; set; }
        public virtual TTKH TTKH { get; set; }
        public virtual PT PT { get; set; }
        public virtual Status Status { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lichtap> lichtaps { get; set; }
    }
}
